using AutoMapper;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Stock.Products;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.LowStockQuantity
{
    public class LowStockQuantityQueryHandler : IRequestHandler<LowStockQuantityQuery, ICollection<StockInfromationDto>>
    {
        private readonly ISoftDeletableRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public LowStockQuantityQueryHandler(
            ISoftDeletableRepository<Product> productRepo,
            IMapper mapper)
        {
            _mapper = mapper;
            _productRepo = productRepo;
        }

        public async Task<ICollection<StockInfromationDto>> Handle(LowStockQuantityQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepo.GetAllAsync(new Specification<Product>(
                criteria: product => product.ProductStock <= request.MaxQuantity,
                includes: ["Translations"],
                orderBy: product => product.ProductStock,
                tracking: false
                ));

            return _mapper.Map<ICollection<StockInfromationDto>>(
                products,
                opt =>
                {
                    opt.Items["lang"] = (int)request.Language;
                });
        }
    }
}