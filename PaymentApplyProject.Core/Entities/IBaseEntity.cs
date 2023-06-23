using System.ComponentModel.DataAnnotations;

namespace PaymentApplyProject.Core.Entities
{
    public interface IBaseEntity<T> : IBaseEntityWithoutId
        where T : notnull
    {
        public T Id { get; set; }
    }

}
