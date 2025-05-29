using AutoMapper;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Stock;
using LMS.Domain.Entities.Stock.Products;
using MediatR;

namespace LMS.Application.Features.Admin.Stock.Queries.InvernotyLogs
{
    public class InventoryLogsQueryHandler : IRequestHandler<InventoryLogsQuery, ICollection<InventoryLogDetailsDto>>
    {
        private readonly IBaseRepository<InventoryLog> _logRepo;
        private readonly IMapper _mapper;

        public InventoryLogsQueryHandler(
            IBaseRepository<InventoryLog> logRepo,
            IMapper mapper)
        {
            _logRepo = logRepo;
            _mapper = mapper;
        }

        public async Task<ICollection<InventoryLogDetailsDto>> Handle(InventoryLogsQuery request, CancellationToken cancellationToken)
        {
            var inventoryLogs = await _logRepo.GetAllAsync(new Specification<InventoryLog>(
                includes: ["Product.Translations"],
                skip: (request.Skip - 1) * request.Take,
                take: request.Take
                ));;

            var logs = _mapper.Map<ICollection<InventoryLogDetailsDto>>(
                inventoryLogs,
                opt =>
                {
                    opt.Items["lang"] = (int)request.Language;
                });

            return logs;
        }
    }
}
