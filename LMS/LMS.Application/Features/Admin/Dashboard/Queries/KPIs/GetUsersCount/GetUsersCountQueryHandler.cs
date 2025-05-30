using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.Specifications.Users;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetUsersCount
{
    public class GetUsersCountQueryHandler : IRequestHandler<GetUsersCountQuery, NumberOfUsersDto>
    {
        private readonly ISoftDeletableRepository<User> _userRepo;

        public GetUsersCountQueryHandler(
            ISoftDeletableRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<NumberOfUsersDto> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepo.GetAllAsync(new ActiveUsersSpecification());

            return new NumberOfUsersDto
            {
                NumberOfUsers = users.count,
                NumberOfCustomers = users.items.Where(user => user.Role.RoleType.ToLower() == "customer").Count(),
                NumberOfEmployees = users.items.Where(user => user.Role.RoleType.ToLower() == "employee").Count()
            };
        }
    }
}