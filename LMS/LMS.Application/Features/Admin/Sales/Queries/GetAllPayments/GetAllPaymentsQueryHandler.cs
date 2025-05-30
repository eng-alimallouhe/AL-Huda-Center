using AutoMapper;
using LMS.Application.DTOs.Admin.Sales;
using LMS.Application.DTOs.Common;
using LMS.Application.Specifications.Sales;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Financial;
using MediatR;

namespace LMS.Application.Features.Admin.Sales.Queries.GetAllPayments
{
    public class GetAllPaymentsQueryHandler
        : IRequestHandler<GetAllPaymentsQuery, PagedResult<PaymentDetailsDto>>
    {
        private readonly ISoftDeletableRepository<Payment> _paymentRepo;
        private readonly IMapper _mapper;

        public GetAllPaymentsQueryHandler(
            ISoftDeletableRepository<Payment> paymentRepo,
            IMapper mapper)
        {
            _paymentRepo = paymentRepo;
            _mapper = mapper;
        }

        public async Task<PagedResult<PaymentDetailsDto>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var response = await _paymentRepo.GetAllAsync(new PaymentSpecification(request.PageNumber, request.PageSize));

            var payments = _mapper.Map<ICollection<PaymentDetailsDto>>(response.items);

            return new PagedResult<PaymentDetailsDto> 
            { 
                Items = payments,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = response.count,
            };
        }
    }
}
