using LMS.Application.Specifications.Sales;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Stock.Products;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetBooksCount
{
    public class GetBooksCountQueryHandler : IRequestHandler<GetBooksCountQuery, int>
    {
        private readonly ISoftDeletableRepository<Book> _bookRepo;
        public GetBooksCountQueryHandler(
            ISoftDeletableRepository<Book> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<int> Handle(GetBooksCountQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookRepo.GetAllAsync(new ActiveBookSpecification(null));
            
            return result.count;
        }
    }
}