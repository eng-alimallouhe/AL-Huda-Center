using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Stock.Queries.StockInformation
{
    public record StockInformationQuery(
        int Skip,
        int Take,
        Language Language): IRequest<ICollection<StockInfromationDto>>;
}
