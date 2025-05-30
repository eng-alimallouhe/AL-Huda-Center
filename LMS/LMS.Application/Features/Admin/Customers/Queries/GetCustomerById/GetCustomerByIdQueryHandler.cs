using AutoMapper;
using LMS.Application.DTOs.Users;
using LMS.Application.Specifications.Users;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDetailsDto?>
    {
        private readonly ISoftDeletableRepository<Customer> _customerRepo;
        private readonly IMapper _mapper;


        public GetCustomerByIdQueryHandler(
            ISoftDeletableRepository<Customer> customerRepo,
            IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }


        public async Task<CustomerDetailsDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepo.GetBySpecificationAsync(new SingleCustomerSpecification(request.userId));

            if (customer is null)
            {
                return null;
            }

            var result = _mapper.Map<CustomerDetailsDto>(customer);

            return result;
        }
    }
}
