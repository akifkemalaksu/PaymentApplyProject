using System.ComponentModel.DataAnnotations;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public interface IBaseEntity<T> : IBaseEntityWithoutId
        where T : notnull
    {
        public T Id { get; set; }
    }

}
