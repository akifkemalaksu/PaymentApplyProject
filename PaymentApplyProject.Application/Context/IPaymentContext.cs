using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Context
{
    public interface IPaymentContext : IDbContext
    {
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

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
