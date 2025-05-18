namespace LMS.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        
        Task CommitTransactionAsync();
        
        Task RollbackTransactionAsync();
    }
}
