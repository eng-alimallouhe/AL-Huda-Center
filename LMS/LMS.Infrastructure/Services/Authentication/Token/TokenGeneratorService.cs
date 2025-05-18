using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LMS.Application.Abstractions.Services.Authentication;
using LMS.Common.Enums;
using LMS.Common.Exceptions;
using LMS.Common.Results;
using LMS.Common.Settings;
using LMS.Domain.Abstractions;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LMS.Infrastructure.Services.Authentication.Token
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly TokenSettings _tokenSettings;
        private readonly ISoftDeletableRepository<User> _userRepo;
        private readonly ILogger<TokenGeneratorService> _logger;
        private readonly IBaseRepository<RefreshToken> _refreshRepo;
        private readonly IUnitOfWork _unitOfWork;

        public TokenGeneratorService(
            IOptions<TokenSettings> options,
            ISoftDeletableRepository<User> userRepo,
            ILogger<TokenGeneratorService> logger,
            IBaseRepository<RefreshToken> refreshRepo,
            IUnitOfWork unitOfWork)
        {
            _tokenSettings = options.Value;
            _userRepo = userRepo;
            _logger = logger;
            _refreshRepo = refreshRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> GenerateAccessTokenAsync(Guid userId)
        {
            var user = await _userRepo.GetBySpecificationAsync(new Specification<User>(
                criteria: user => user.UserId == userId,
                includes: [user => user.Role]
                ));

            if (user is null)
            {
                return Result<string>.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            // Create a security key from the configured secret key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));

            // Generate signing credentials using HMAC SHA256 algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            // Define user claims to be included in the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()), // User ID
                new Claim(JwtRegisteredClaimNames.Email, user.Email), // User email
                new Claim(ClaimTypes.Role, user.Role.RoleType), // User role
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique identifier for the token
            };


            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: _tokenSettings.Issure,
                audience: _tokenSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );


            // Serialize and return the token
            return Result<string>.Success(
                new JwtSecurityTokenHandler().WriteToken(token),
                ResponseStatus.TASK_COMPLETED);
        }

        public async Task<Result<string>> GenerateRefreshTokenAsync(Guid userId)
        {
            await _unitOfWork.BeginTransactionAsync();
            var user = await _userRepo.GetBySpecificationAsync(new Specification<User>(
                criteria: user => user.UserId == userId,
                includes: [user => user.Role, user => user.RefreshToken]
                ));

            // Ensure the user exists before generating a token
            if (user == null)
            {
                return Result<string>.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }


            // Create a 64-byte secure random token
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }


            // Create a new refresh token object
            var token = new RefreshToken
            {
                UserId = user.UserId,
                Token = Convert.ToBase64String(randomBytes), // Convert to Base64 string
                ExpiredAt = DateTime.UtcNow.AddDays(7), // Token valid for 7 days
                CreatedAt = DateTime.UtcNow
            };


            if (user.RefreshToken is not null)
            {
                try
                {
                    // Remove any previous refresh token for this user
                    await _refreshRepo.HardDeleteAsync(user.RefreshToken.RefreshTokenId);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Errorn while deleting the refresh token for the user: {user.UserName}, \nError message: {ex.Message}");
                    return Result<string>.Failure(ResponseStatus.DELETE_ERROR);
                }
            }


            try
            {
                // Store the new refresh token in the database
                await _refreshRepo.AddAsync(token);
            }
            catch (DatabaseException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError($"Errorn while adding the refresh token for the user: {user.UserName}, \n" +
                    $"Error message: {ex.Message}, \n" +
                    $"Error Code: {ex.SqlErrorCode}\n " +
                    $"Error Code : {ex.SqlErrorCode}\n" +
                    $"------------------------------------------------------------------------------------\n");
                return Result<string>.Failure(ResponseStatus.ADD_ERROR);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError($"Errorn while adding the refresh token for the user: {user.UserName}, \n" +
                    $"Error message: {ex.Message}" +
                    $"------------------------------------------------------------------------------------\n");
                return Result<string>.Failure(ResponseStatus.ADD_ERROR);
            }
            await _unitOfWork.CommitTransactionAsync();
            return Result<string>.Success(token.Token, ResponseStatus.AUTHENTICATION_SUCCESS);
        }
    }
}
