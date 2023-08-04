using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Domain.Entities
{
    public class DepositRequest : BaseEntity<int>
    {
        public string CallbackUrl { get; set; }
        public string SuccessUrl { get; set; }
        public string FailedUrl { get; set; }
        [StringLength(100)]
        public string CustomerId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(20)]
        public string MethodType { get; set; }
        [StringLength(100)]
        public string UniqueTransactionId { get; set; }
        [StringLength(64)]
        public string UniqueTransactionIdHash { get; set; }
    }
}
