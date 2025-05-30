using LMS.Application.DTOs.Admin.Financial;
using LMS.Application.DTOs.Common;
using MediatR;

namespace LMS.Application.Features.Admin.Sales.Queries.Revenues
{
    public record RevenuesQuery(
        int PageNumber = 0,
        int PageSize = 20) : IRequest<PagedResult<FinancialDetailsDto>>;
}