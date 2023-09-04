using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Services.InfrastructureServices
{
    public interface ICacheService
    {
        public T Get<T>(string key);
        public void Set<T>(string key, T value);
        public void Set<T>(string key, T value, DateTimeOffset expirationTime);
        public void Remove(string key);
    }
}
