using Mapster;
using PaymentApplyProject.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Infrastructure.Mapping
{
    public class MapstersMapper : ICustomMapper
    {
        public T Map<T>(object entity) => entity.Adapt<T>();
    }
}
