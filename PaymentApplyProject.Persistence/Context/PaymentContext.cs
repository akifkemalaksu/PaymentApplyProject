using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Domain.Entities.Bases;

namespace PaymentApplyProject.Persistence.Context
{
    public class PaymentContext : DbContext, IPaymentContext
    {
        private IDbContextTransaction? _transaction;
        private readonly int _userId;

        public PaymentContext(DbContextOptions options, IAuthenticatedUserService authenticatedUserService) : base(options)
        {
            _userId = authenticatedUserService.GetUserId();
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<WithdrawStatus> WithdrawStatuses { get; set; }
        public DbSet<DepositStatus> DepositStatuses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Withdraw> Withdraws { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<DepositRequest> DepositRequests { get; set; }

        public override int SaveChanges()
        {
            AddEventListener();
            UpdateEventListener();
            DeleteEventListener();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddEventListener();
            UpdateEventListener();
            DeleteEventListener();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddEventListener()
        {
            var added = ChangeTracker.Entries<IBaseEntityWithoutId>().Where(x => x.State == EntityState.Added).Select(x => x.Entity).ToList();
            if (!added.Any()) return;

            Parallel.ForEach(added, entity =>
            {
                entity.AddedUserId = _userId;
                entity.EditedUserId = _userId;
                entity.AddDate = DateTime.Now;
                entity.EditDate = DateTime.Now;
                entity.Deleted = false;
            });
        }

        private void UpdateEventListener()
        {
            var updated = ChangeTracker.Entries<IBaseEntityWithoutId>().Where(x => x.State == EntityState.Modified).ToList();
            if (!updated.Any()) return;

            Parallel.ForEach(updated, entityEntry =>
            {
                entityEntry.Property(x => x.AddedUserId).IsModified = false;
                entityEntry.Property(x => x.AddDate).IsModified = false;
                entityEntry.Property(x => x.Deleted).IsModified = false;

                entityEntry.Entity.EditedUserId = _userId;
                entityEntry.Entity.EditDate = DateTime.Now;
            });
        }

        private void DeleteEventListener()
        {
            var deleted = ChangeTracker.Entries<IBaseEntityWithoutId>().Where(x => x.State == EntityState.Deleted).ToList();
            if (!deleted.Any()) return;

            Parallel.ForEach(deleted, entityEntry =>
            {
                entityEntry.State = EntityState.Modified;

                entityEntry.Property(x => x.AddedUserId).IsModified = false;
                entityEntry.Property(x => x.AddDate).IsModified = false;

                entityEntry.Entity.EditedUserId = _userId;
                entityEntry.Entity.EditDate = DateTime.Now;
                entityEntry.Entity.Deleted = true;
            });
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _transaction ??= await Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _transaction?.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (_transaction is not null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _transaction?.RollbackAsync(cancellationToken);
            }
            finally
            {
                if (_transaction is not null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RetryOnExceptionAsync(Func<Task> func)
        {
            await Database.CreateExecutionStrategy().ExecuteAsync(func);
        }
    }
}
