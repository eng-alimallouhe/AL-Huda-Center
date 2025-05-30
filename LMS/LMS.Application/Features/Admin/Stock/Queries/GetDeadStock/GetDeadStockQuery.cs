using LMS.Application.DTOs.Common;
using LMS.Application.DTOs.Stock;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Stock.Queries.GetDeadStock
{
    public record GetDeadStockQuery(
        Language Language,
        int PageNumber,
        int PageSize,
        DateTime From): IRequest<PagedResult<DeadStockDto>>;
}