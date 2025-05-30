using AutoMapper;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Application.DTOs.Common;
using LMS.Application.DTOs.Users;
using LMS.Application.Specifications.Users;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, PagedResult<CustomersOverViewDto>>
    {
        private readonly ISoftDeletableRepository<Customer> _customerRepo;
        private readonly IMapper _mapper;


        public GetAllCustomersQueryHandler(
            ISoftDeletableRepository<Customer> customerRepo,
            IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }


        public async Task<PagedResult<CustomersOverViewDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var response = await _customerRepo.GetAllAsync(new ActiveCustomerSpecification(request.PageNumber, request.PageSize));

            var customers = _mapper.Map<ICollection<CustomersOverViewDto>>(response.items);

            return new PagedResult<CustomersOverViewDto>
            {
                Items = customers,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = response.count
            };
        }
    }
}

