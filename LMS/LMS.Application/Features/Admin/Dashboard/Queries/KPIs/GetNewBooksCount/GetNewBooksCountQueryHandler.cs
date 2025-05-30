using LMS.Application.Specifications.Sales;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Stock.Products;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetNewBooksCount
{
    public class GetNewBooksCountQueryHandler : IRequestHandler<GetNewBooksCountQuery, int>
    {
        private readonly ISoftDeletableRepository<Book> _bookRepo;


        public GetNewBooksCountQueryHandler(
            ISoftDeletableRepository<Book> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<int> Handle(GetNewBooksCountQuery request, CancellationToken cancellationToken)
        {

            return (await _bookRepo.GetAllAsync(new ActiveBookSpecification(request.StartDate))).count;
        }
    }
}
