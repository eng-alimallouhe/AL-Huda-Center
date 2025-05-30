using AutoMapper;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.DTOs.Common;
using LMS.Application.Specifications.Stock.Products;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Stock.Products;
using MediatR;

namespace LMS.Application.Features.Admin.Stock.Queries.GetStockSnapshot
{
    public class GetStockSnapshotQueryHandler : IRequestHandler<GetStockSnapshotQuery, PagedResult<StockInfromationDto>>
    {
        private readonly ISoftDeletableRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public GetStockSnapshotQueryHandler(
            ISoftDeletableRepository<Product> productRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<PagedResult<StockInfromationDto>> Handle(GetStockSnapshotQuery request, CancellationToken cancellationToken)
        {
            var response = await _productRepo.GetAllAsync(new StockSnapshotSpecification(request.PageNumber, request.PageSize));


            var mappedProducts = _mapper.Map<ICollection<StockInfromationDto>>(
                response.items,
                opt =>
                {
                    opt.Items["lang"] = (int)request.Language;
                });

            return new PagedResult<StockInfromationDto>(mappedProducts, response.count, request.PageSize, request.PageNumber);
        }
    }
}