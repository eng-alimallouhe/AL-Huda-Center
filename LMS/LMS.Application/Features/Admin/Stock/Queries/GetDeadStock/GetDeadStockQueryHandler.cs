using AutoMapper;
using LMS.Application.DTOs.Common;
using LMS.Application.DTOs.Stock;
using LMS.Application.Specifications.Stock.Products;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Stock.Products;
using MediatR;

namespace LMS.Application.Features.Admin.Stock.Queries.GetDeadStock
{
    public class GetDeadStockQueryHandler : IRequestHandler<GetDeadStockQuery, PagedResult<DeadStockDto>>
    {
        private readonly ISoftDeletableRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public GetDeadStockQueryHandler(
            ISoftDeletableRepository<Product> productRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }


        public async Task<PagedResult<DeadStockDto>> Handle(GetDeadStockQuery request, CancellationToken cancellationToken)
        {
            var response = await _productRepo.GetAllAsync(new DeadStockSpecification(request.From, request.PageNumber, request.PageSize));

            var deadProducts =  _mapper.Map<ICollection<DeadStockDto>>(
                response.items,
                opt =>
                {
                    opt.Items["lang"] = (int)request.Language;
                });

            return new PagedResult<DeadStockDto>
            {
                Items = deadProducts,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = response.count
            };
        }
    }
}