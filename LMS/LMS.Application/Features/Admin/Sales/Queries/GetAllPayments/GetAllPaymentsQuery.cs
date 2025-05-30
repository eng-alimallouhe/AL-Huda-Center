using LMS.Application.DTOs.Admin.Sales;
using LMS.Application.DTOs.Common;
using LMS.Domain.Entities.Financial;
using MediatR;

namespace LMS.Application.Features.Admin.Sales.Queries.GetAllPayments
{
    public record GetAllPaymentsQuery (
        int PageNumber, 
        int PageSize) : IRequest<PagedResult<PaymentDetailsDto>> ;
}
