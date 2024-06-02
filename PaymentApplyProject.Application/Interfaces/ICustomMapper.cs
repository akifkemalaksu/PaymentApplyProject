namespace PaymentApplyProject.Application.Interfaces
{
    public interface ICustomMapper
    {
        public T Map<T>(object entity);
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
        public IQueryable<T> QueryMap<T>(IQueryable query);
    }
}
