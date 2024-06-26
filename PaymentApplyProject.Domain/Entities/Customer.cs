﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApplyProject.Domain.Entities
{
    public class Customer : BaseEntity<int>
    {
        public short CompanyId { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        [StringLength(100)]
        public string ExternalCustomerId { get; set; }
        public bool Active { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }

}
