using AutoMapper;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.DTOs.Stock;
using LMS.Application.Specifications.Stock.Products;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Stock.Products;
using MediatR;

namespace LMS.Application.Features.Admin.Reports.Queries.FullStockInventoryReport
{
    public class FullStockInventoryReportQueryHandler : IRequestHandler<FullStockInventoryReportQuery, ICollection<StockSnapshotDto>>
    {
        private readonly ISoftDeletableRepository<Product> _productRepo;
        private readonly IMapper _mapper;


        public FullStockInventoryReportQueryHandler(
            ISoftDeletableRepository<Product> productRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ICollection<StockSnapshotDto>> Handle(FullStockInventoryReportQuery request, CancellationToken cancellationToken)
        {
            var response = await _productRepo.GetAllAsync(new StockSnapshotSpecification(null, null));


            var mappedProducts = _mapper.Map<ICollection<StockSnapshotDto>>(
                response.items,
                opt =>
                {
                    opt.Items["lang"] = (int)request.Language;
                });

            return mappedProducts;
        }
    }
}
