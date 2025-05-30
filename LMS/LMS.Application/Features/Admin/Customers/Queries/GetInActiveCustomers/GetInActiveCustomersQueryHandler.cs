using AutoMapper;
using LMS.Application.DTOs.Common;
using LMS.Application.DTOs.Users;
using LMS.Application.Specifications.Users;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Customers.Queries.GetInActiveCustomers
{
    public class GetInActiveCustomersQueryHandler : IRequestHandler<GetInActiveCustomersQuery, PagedResult<InActiveCustomersDto>>
    {
        private readonly ISoftDeletableRepository<Customer> _customerRepo;
        private readonly IMapper _mapper;

        public GetInActiveCustomersQueryHandler(
            ISoftDeletableRepository<Customer> customerRepo,
            IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;   
        }

        public async Task<PagedResult<InActiveCustomersDto>> Handle(GetInActiveCustomersQuery request, CancellationToken cancellationToken)
        {
            var response = await _customerRepo.GetAllAsync(new InActiveCustomersSpecification(request.From, request.PageNumber, request.PageSize));

            var inActiveCustomers = _mapper.Map<ICollection<InActiveCustomersDto>>(response.items);

            return new PagedResult<InActiveCustomersDto>
            {
                Items = inActiveCustomers,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = response.count
            };
        }
    }
}