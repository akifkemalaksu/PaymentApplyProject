using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Persistence.Context
{
    public class PaymentContext : DbContext, IPaymentContext
    {
        private IDbContextTransaction _transaction;

        public PaymentContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Banka> Bankalar { get; set; }
        public DbSet<BankaHesabi> BankaHesaplari { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Durum> Durumlar { get; set; }
        public DbSet<ParaCekmeDurum> ParaCekmeDurumlar { get; set; }
        public DbSet<ParaYatirmaDurum> ParaYatirmaDurumlar { get; set; }
        public DbSet<Firma> Firmalar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<ParaCekme> ParaCekmeler { get; set; }
        public DbSet<ParaYatirma> ParaYatirmalar { get; set; }
        public DbSet<Yetki> Yetkiler { get; set; }
        public DbSet<KullaniciYetki> KullaniciYetkiler { get; set; }
        public DbSet<KullaniciFirma> KullaniciFirmalar { get; set; }

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
                entity.EkleyenKullaniciId = 0;
                entity.DuzenleyenKullaniciId = 0;
                entity.EklemeTarihi = DateTime.UtcNow;
                entity.GuncellemeTarihi = DateTime.UtcNow;
                entity.SilindiMi = false;
            });
        }

        private void UpdateEventListener()
        {
            var updated = ChangeTracker.Entries<IBaseEntityWithoutId>().Where(x => x.State == EntityState.Modified).Select(x => x.Entity).ToList();
            if (!updated.Any()) return;

            Parallel.ForEach(updated, entity =>
            {
                entity.DuzenleyenKullaniciId = 0;
                entity.GuncellemeTarihi = DateTime.UtcNow;
            });
        }

        private void DeleteEventListener()
        {
            var deleted = ChangeTracker.Entries<IBaseEntityWithoutId>().Where(x => x.State == EntityState.Deleted).ToList();
            if (!deleted.Any()) return;

            Parallel.ForEach(deleted, entity =>
            {
                entity.State = EntityState.Modified;
                entity.Entity.DuzenleyenKullaniciId = 0;
                entity.Entity.GuncellemeTarihi = DateTime.UtcNow;
                entity.Entity.SilindiMi = true;
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
                await SaveChangesAsync(cancellationToken);
                _transaction?.CommitAsync(cancellationToken);
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
                    _transaction.Dispose();
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
                    _transaction.Dispose();
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
