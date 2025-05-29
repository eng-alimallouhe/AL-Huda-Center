using AutoMapper;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Stock.Products;
using MediatR;

namespace LMS.Application.Features.Admin.Stock.Queries.StockInformation
{
    public class StockInformationQueryHandler : IRequestHandler<StockInformationQuery, ICollection<StockInfromationDto>>
    {
        private readonly ISoftDeletableRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public StockInformationQueryHandler(
            ISoftDeletableRepository<Product> productRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ICollection<StockInfromationDto>> Handle(StockInformationQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepo.GetAllAsync(new Specification<Product>(
                includes: ["Translations"],
                skip: (request.Skip - 1) * request.Take,
                orderBy: product => product.ProductStock,
                take: request.Take,
                tracking: false
                ));


            return _mapper.Map<ICollection<StockInfromationDto>>(
                products,
                opt =>
                {
                    opt.Items["lang"] = (int) request.Language;
                });
        }
    }
}