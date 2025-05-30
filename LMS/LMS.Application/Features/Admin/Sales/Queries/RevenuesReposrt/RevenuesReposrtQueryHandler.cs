using AutoMapper;
using LMS.Application.DTOs.Admin.Financial;
using LMS.Application.Specifications.Sales;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Financial;
using MediatR;

namespace LMS.Application.Features.Admin.Sales.Queries.RevenuesReposrt
{
    public class RevenuesReposrtQueryHandler : IRequestHandler<RevenuesReposrtQuery, ICollection<FinancialDetailsDto>>
    {
        private readonly ISoftDeletableRepository<FinancialRevenue> _financialRepo;
        private readonly IMapper _mapper;

        public RevenuesReposrtQueryHandler(
            ISoftDeletableRepository<FinancialRevenue> financialRepo,
            IMapper mapper)
        {
            _financialRepo = financialRepo;
            _mapper = mapper;
        }

        public async Task<ICollection<FinancialDetailsDto>> Handle(RevenuesReposrtQuery request, CancellationToken cancellationToken)
        {
            var response = await _financialRepo.GetAllAsync(new RevenuesReportSpecification(request.From, request.To));

            var financialItems = _mapper.Map<ICollection<FinancialDetailsDto>>(response.items);

            return financialItems;
        }
    }
}
