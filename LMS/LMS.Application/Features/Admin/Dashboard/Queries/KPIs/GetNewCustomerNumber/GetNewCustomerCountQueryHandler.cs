using LMS.Application.Specifications.Users;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetNewCustomerNumber
{
    public class GetNewCustomerCountQueryHandler : IRequestHandler<GetNewCustomerCountQuery, int>
    {
        private readonly ISoftDeletableRepository<Customer> _customerRepo;

        public GetNewCustomerCountQueryHandler(
            ISoftDeletableRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }


        public async Task<int> Handle(GetNewCustomerCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _customerRepo.GetAllAsync(new NewCustomersCountSpecification(request.StartDate));
            return (response).count;
        }
    }
}
