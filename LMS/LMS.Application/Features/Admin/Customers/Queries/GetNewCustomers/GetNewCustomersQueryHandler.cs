using AutoMapper;
using LMS.Application.DTOs.Users;
using LMS.Application.Specifications.Users;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Customers.Queries.GetNewCustomers
{
    public class GetNewCustomersQueryHandler : IRequestHandler<GetNewCustomersQuery, ICollection<CustomersOverViewDto>>
    {
        private readonly ISoftDeletableRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public GetNewCustomersQueryHandler(
            ISoftDeletableRepository<Customer> customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;   
            _mapper = mapper;
        }

        public async Task<ICollection<CustomersOverViewDto>> Handle(GetNewCustomersQuery request, CancellationToken cancellationToken)
        {
            var response = await _customerRepository.GetAllAsync(new NewCustomersCountSpecification(request.StartDate));

            return _mapper.Map<ICollection<CustomersOverViewDto>>(response.items);
        }
    }
}