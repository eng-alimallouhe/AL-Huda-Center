using LMS.Application.DTOs.Admin.Financial;
using MediatR;

namespace LMS.Application.Features.Admin.Sales.Queries.RevenuesReposrt
{
    public record RevenuesReposrtQuery(
        DateTime From,
        DateTime To) : IRequest<ICollection<FinancialDetailsDto>>;
}
