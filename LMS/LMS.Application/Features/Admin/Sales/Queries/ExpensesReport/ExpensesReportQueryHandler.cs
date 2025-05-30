using AutoMapper;
using LMS.Application.DTOs.Admin.Sales;
using LMS.Application.Specifications.Sales;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Financial;
using MediatR;

namespace LMS.Application.Features.Admin.Sales.Queries.ExpensesReport
{
    public class ExpensesReportQueryHandler : IRequestHandler<ExpensesReportQuery, ICollection<PaymentDetailsDto>>
    {
        private readonly ISoftDeletableRepository<Payment> _paymentRepo;
        private readonly IMapper _mapper;


        public ExpensesReportQueryHandler(
            ISoftDeletableRepository<Payment> paymentRepo,
            IMapper mapper)
        {
            _paymentRepo = paymentRepo;
            _mapper = mapper;
        }

        public async Task<ICollection<PaymentDetailsDto>> Handle(ExpensesReportQuery request, CancellationToken cancellationToken)
        {
            var response = await _paymentRepo.GetAllAsync(new PaymentsReportSpecification(request.From, request.To));
            
            var payments = _mapper.Map<ICollection<PaymentDetailsDto>>(response.items);

            return payments;
        }
    }
}
