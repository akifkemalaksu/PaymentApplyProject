using Mapster;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Infrastructure.Mapping.Mapster
{
    public class MapstersMapper : ICustomMapper
    {
        public T Map<T>(object entity) => entity.Adapt<T>();
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) => source.Adapt(destination);
        public IQueryable<T> QueryMap<T>(IQueryable query) => query.ProjectToType<T>();
    }
}
