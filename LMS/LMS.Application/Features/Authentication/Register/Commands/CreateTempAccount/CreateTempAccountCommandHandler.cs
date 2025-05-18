using AutoMapper;
using LMS.Common.Enums;
using LMS.Common.Exceptions;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Financial.Levels;
using LMS.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LMS.Application.Features.Authentication.Register.Commands.CreateTempAccount
{
    public class CreateTempAccountCommandHandler : IRequestHandler<CreateTempAccountCommand, Result>
    {
        private readonly ISoftDeletableRepository<Customer> _customerRepo;
        private readonly ISoftDeletableRepository<Role> _roleRepo;
        private readonly ISoftDeletableRepository<LoyaltyLevel> _levelRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTempAccountCommandHandler> _logger;


        public CreateTempAccountCommandHandler(
            ISoftDeletableRepository<Customer> customerRepo,
            ISoftDeletableRepository<Role> roleRepo,
            ISoftDeletableRepository<LoyaltyLevel> levelRepo,
            IMapper mapper,
            ILogger<CreateTempAccountCommandHandler> logger)
        {
            _customerRepo = customerRepo;
            _roleRepo = roleRepo;
            _levelRepo = levelRepo;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<Result> Handle(CreateTempAccountCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _customerRepo.GetByExpressionAsync(
                user => user.Email.ToLowerInvariant().Trim() == request.Email.ToLowerInvariant().Trim() ||
                        user.UserName == request.UserName);

            if (oldUser is not null)
            {
                return Result.Failure(ResponseStatus.EXISTING_ACCOUNT);
            }

            if (request.Password.Length < 8)
            {
                return Result.Failure(ResponseStatus.WEAK_PASSWORD);
            }

            var newCustomer = _mapper.Map<Customer>(request);

            newCustomer.HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var customersRole = await _roleRepo.GetByExpressionAsync(
                role => role.RoleType.ToLowerInvariant() == "customer"
                );

            if (customersRole is null)
            {
                return Result.Failure(ResponseStatus.BACK_ERROR);
            }

            var firstLevel = await _levelRepo.GetByExpressionAsync(
                level => level.Translations.Any(t => t.LevelName.ToLowerInvariant() == "bronze")
                );


            if (firstLevel is null)
            {
                return Result.Failure(ResponseStatus.BACK_ERROR);
            }

            newCustomer.Level = firstLevel;
            newCustomer.Role = customersRole;
            newCustomer.LevelId = firstLevel.LevelId;
            newCustomer.RoleId = customersRole.RoleId;

            try
            {
                await _customerRepo.AddAsync(newCustomer);
                return Result.Success(ResponseStatus.TASK_COMPLETED);
            }

            catch (DatabaseException ex)
            {
                _logger.LogError($"Error while adding the new customer to database, Customer name: {request.UserName}," +
                    $"\nError Message: {ex.Message}, \n" +
                    $"Error Code: {ex.SqlErrorCode}\n" +
                    $"------------------------------------------------------------------------------------\n");
                return Result.Failure(ResponseStatus.ADD_ERROR);
            }
        }
    }
}
