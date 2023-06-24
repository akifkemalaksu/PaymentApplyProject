using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Mapping
{
    public interface ICustomMapper
    {
        public T Map<T>(object entity);
    }
}
