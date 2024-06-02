using Mapster;
using PaymentApplyProject.Application.Mapping;

namespace PaymentApplyProject.Infrastructure.Mapping
{
    public class MapstersMapper : ICustomMapper
    {
        public T Map<T>(object entity) => entity.Adapt<T>();
    }
}
