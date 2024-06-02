namespace PaymentApplyProject.Domain.Entities
{
    public interface IBaseEntity<T> : IBaseEntityWithoutId
        where T : notnull
    {
        public T Id { get; set; }
    }

}
