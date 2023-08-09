using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Domain.Entities
{
    public class InsertLog : BaseEntity<int>
    {
        [StringLength(36)]
        public string InsertedId { get; set; }
        [StringLength(128)]
        public string TableName { get; set; }
        public string Token { get; set; }
    }
}
