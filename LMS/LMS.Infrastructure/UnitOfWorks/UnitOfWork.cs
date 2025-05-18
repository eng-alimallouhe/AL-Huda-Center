using LMS.Domain.Abstractions;
using LMS.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace LMS.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LMSDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(LMSDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction is not null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is not null)
                await _transaction.RollbackAsync();
        }
    }

}
