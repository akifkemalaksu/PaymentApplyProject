﻿using Microsoft.Extensions.Caching.Memory;
using PaymentApplyProject.Application.Services.InfrastructureServices;

namespace PaymentApplyProject.Infrastructure.Services.InfrastructureServices
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
        public void Set<T>(string key, T value, TimeSpan timeSpan) => _memoryCache.Set(key, value, timeSpan);
    }
}
