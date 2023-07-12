using System.ComponentModel.DataAnnotations;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class BaseEntity<T> : IBaseEntity<T>
        where T : notnull
    {
        [Key]
        public virtual T Id { get; set; }
        public virtual bool Delete { get; set; }
        public virtual int AddedUserId { get; set; }
        public virtual int EditedUserId { get; set; }
        public virtual DateTime AddDate { get; set; }
        public virtual DateTime EditDate { get; set; }
    }

}
