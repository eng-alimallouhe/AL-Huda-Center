using AutoMapper;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.DTOs.Common;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Stock;
using MediatR;

namespace LMS.Application.Features.Admin.Stock.Queries.GetStockHistory
{
    public class GetStockHistoryQueryHandler : IRequestHandler<GetStockHistoryQuery, PagedResult<InventoryLogDetailsDto>>
    {
        private readonly IBaseRepository<InventoryLog> _logRepo;
        private readonly IMapper _mapper;

        public GetStockHistoryQueryHandler(
            IBaseRepository<InventoryLog> logRepo,
            IMapper mapper)
        {
            _logRepo = logRepo;
            _mapper = mapper;
        }

        public async Task<PagedResult<InventoryLogDetailsDto>> Handle(GetStockHistoryQuery request, CancellationToken cancellationToken)
        {
            var response = await _logRepo.GetAllAsync(new Specification<InventoryLog>(
                includes: ["Product.Translations"],
                skip: request.PageNumber,
                take: request.PageSize
                )); ;

            var inventoryLogs = response.items;

            var logs = _mapper.Map<ICollection<InventoryLogDetailsDto>>(
                inventoryLogs,
                opt =>
                {
                    opt.Items["lang"] = (int)request.Language;
                });



            return new PagedResult<InventoryLogDetailsDto>(logs, response.count, request.PageSize, request.PageNumber);
        }
    }
}
