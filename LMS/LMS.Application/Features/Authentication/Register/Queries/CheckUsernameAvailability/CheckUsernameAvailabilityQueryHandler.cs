using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Authentication.Register.Queries.CheckUsernameAvailability
{
    public class CheckUsernameAvailabilityQueryHandler : IRequestHandler<CheckUsernameAvailabilityQuery, Result>
    {
        private readonly ISoftDeletableRepository<User> _userRepo;

        public CheckUsernameAvailabilityQueryHandler(
            ISoftDeletableRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Result> Handle(CheckUsernameAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByExpressionAsync(user => user.UserName == request.UserName);

            if (user is null)
            {
                return Result.Success(ResponseStatus.ACCOUNT_NOT_FOUND);
            }
            return Result.Failure(ResponseStatus.EXISTING_ACCOUNT);
        }
    }
}
