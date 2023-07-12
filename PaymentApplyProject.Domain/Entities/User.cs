using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{

    public class User : BaseEntity<int>
    {
        [StringLength(20)]
        public string Username { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserCompany> UserCompanies { get; set; }
    }

}
