﻿using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Durum : BaseEntity<short>
    {
        public string Ad { get; set; }
        public string Aciklama { get; set; }
    }

    public class ParaCekmeDurum : Durum
    {
        public virtual ICollection<ParaCekme> ParaCekmeler { get; set; }
    }

    public class ParaYatirmaDurum : Durum
    {
        public virtual ICollection<ParaYatirma> ParaYatirmalar { get; set; }
    }
}
