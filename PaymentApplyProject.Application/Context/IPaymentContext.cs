using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Context
{
    public interface IPaymentContext
    {
        DbSet<Banka> Bankalar { get; set; }
        DbSet<BankaHesabi> BankaHesaplari { get; set; }
        DbSet<Musteri> Musteriler { get; set; }
        DbSet<Durum> Durumlar { get; set; }
        DbSet<ParaCekmeDurum> ParaCekmeDurumlar { get; set; }
        DbSet<ParaYatirmaDurum> ParaYatirmaDurumlar { get; set; }
        DbSet<Firma> Firmalar { get; set; }
        DbSet<Kullanici> Kullanicilar { get; set; }
        DbSet<KullaniciYetki> KullaniciYetkiler { get; set; }
        DbSet<ParaCekme> ParaCekmeler { get; set; }
        DbSet<ParaYatirma> ParaYatirmalar { get; set; }
        DbSet<Yetki> Yetkiler { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
