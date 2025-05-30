using AutoMapper;
using LMS.Application.DTOs.Admin.Financial;
using LMS.Application.DTOs.Common;
using LMS.Application.Specifications.Sales;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Financial;
using MediatR;

namespace LMS.Application.Features.Admin.Sales.Queries.Revenues
{
    public class RevenuesQueryHandler : IRequestHandler<RevenuesQuery, PagedResult<FinancialDetailsDto>>
    {
        private readonly ISoftDeletableRepository<FinancialRevenue> _financialRepo;
        private readonly IMapper _mapper;

        public RevenuesQueryHandler(
            ISoftDeletableRepository<FinancialRevenue> financialRepo,
            IMapper mapper)
        {
            _financialRepo = financialRepo;
            _mapper = mapper;
        }

        public async Task<PagedResult<FinancialDetailsDto>> Handle(RevenuesQuery request, CancellationToken cancellationToken)
        {
            var totalCount = (await _financialRepo.GetAllAsync(new RevenuesSpecification(null, null))).count;
            
            var response = (await _financialRepo.GetAllAsync(new RevenuesSpecification(request.PageNumber, request.PageSize))).items;

            var financialItems = _mapper.Map<ICollection<FinancialDetailsDto>>(response);

            return new PagedResult<FinancialDetailsDto>(financialItems, totalCount, request.PageSize, request.PageNumber);
        }
    }
}