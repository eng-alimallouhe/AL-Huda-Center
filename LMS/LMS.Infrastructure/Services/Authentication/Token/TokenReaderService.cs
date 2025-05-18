using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LMS.Application.Abstractions.Services.Authentication;
using LMS.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LMS.Infrastructure.Services.Authentication.Token
{
    public class TokenReaderService : ITokenReaderService
    {
        private readonly TokenSettings _tokenSettings;
        public TokenReaderService(IOptions<TokenSettings> options)
        {
            _tokenSettings = options.Value;
        }

        public string? GetEmail(string accessToken)
        {
            var principal = GetPrincipal(accessToken);
            return principal?.FindFirst(ClaimTypes.Email)?.Value;
        }

        public Guid? GetUserId(string accessToken)
        {
            var principal = GetPrincipal(accessToken);
            var userIdString = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdString, out var userId) ? userId : null;
        }
         
        
        private ClaimsPrincipal? GetPrincipal(string accessToken)
        {
            string secretKey = _tokenSettings.SecretKey;

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(secretKey);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            try
            {
                var principal = tokenHandler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                Console.WriteLine("All Claims:");
                foreach (var claim in principal.Claims)
                {
                    Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                }


                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
