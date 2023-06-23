using AutoMapper;
using PaymentApplyProject.Core.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Infrastructure.Mapping
{
    public class AutoMapper : ICustomMapper
    {
        private readonly IMapper _mapper;

        public AutoMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T Map<T>(object entity) => _mapper.Map<T>(entity);
    }
}
