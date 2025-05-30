using LMS.Application.DTOs.Admin.Sales;
using MediatR;

namespace LMS.Application.Features.Admin.Sales.Queries.ExpensesReport
{
    public record ExpensesReportQuery(
        DateTime From,
        DateTime To) : IRequest<ICollection<PaymentDetailsDto>>;
}
