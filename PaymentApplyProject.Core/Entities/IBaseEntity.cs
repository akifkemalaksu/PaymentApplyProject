namespace PaymentApplyProject.Core.Entities
{
    public interface IBaseEntity<T>
        where T : notnull
    {
        public  T Id { get; set; }
        public bool SilindiMi { get; set; }
        public int EkleyenKullaniciId { get; set; }
        public int DuzenleyenKullaniciId { get; set; }
        public DateTime EklemeTarihi { get; set; }
        public DateTime GuncellemeTarihi { get; set; }
    }

}
