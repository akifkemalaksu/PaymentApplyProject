using Microsoft.Extensions.Caching.Memory;
using PaymentApplyProject.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Infrastructure.Services
{
    internal class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key) => _memoryCache.Get<T>(key);

        public void Remove(string key) => _memoryCache.Remove(key);

        public void Set<T>(string key, T value) => _memoryCache.Set(key, value);
        public void Set<T>(string key, T value, DateTimeOffset expirationTime) => _memoryCache.Set(key, value, expirationTime);
    }
}
