using LMS.Application.Specifications.Admin;
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
            return (await _bookRepo.GetAllAsync(new ActiveBookSpecification(null))).Count();
        }
    }
}