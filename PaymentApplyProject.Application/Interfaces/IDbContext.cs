using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Interfaces
{
    public interface IDbContext
    {
        // todo: şimdilik sadece async gidelim

        //void BeginTransaction();
        //void CommitTransaction();
        //void RollbackTransaction();
        //void RetryOnException();

        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
        Task RetryOnExceptionAsync(Func<Task> func);
    }
}
