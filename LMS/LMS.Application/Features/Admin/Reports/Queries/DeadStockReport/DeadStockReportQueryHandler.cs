using AutoMapper;
using LMS.Application.DTOs.Stock;
using LMS.Application.Specifications.Stock.Products;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Stock.Products;
using MediatR;

namespace LMS.Application.Features.Admin.Reports.Queries.DeadStockReport
{
    public class DeadStockReportQueryHandler : IRequestHandler<DeadStockReportQuery, ICollection<DeadStockDto>>
    {
        private readonly ISoftDeletableRepository<Product> _productRepo;
        private readonly IMapper _mapper;


        public DeadStockReportQueryHandler(
            ISoftDeletableRepository<Product> productRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ICollection<DeadStockDto>> Handle(DeadStockReportQuery request, CancellationToken cancellationToken)
        {
            var response = await _productRepo.GetAllAsync(new DeadStockSpecification(request.From, null, null));

            var deadProducts = _mapper.Map<ICollection<DeadStockDto>>(
                response.items,
                opt =>
                {
                    opt.Items["lang"] = (int)request.Language;
                });

            return deadProducts;
        }
    }
}