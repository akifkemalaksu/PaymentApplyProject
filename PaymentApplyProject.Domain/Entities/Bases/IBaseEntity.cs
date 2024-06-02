namespace PaymentApplyProject.Domain.Entities.Bases
{
    public interface IBaseEntity<T> : IBaseEntityWithoutId
        where T : notnull
    {
        public T Id { get; set; }
    }

}
