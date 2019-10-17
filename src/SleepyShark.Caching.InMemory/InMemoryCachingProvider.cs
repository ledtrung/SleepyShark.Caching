using SleepyShark.Caching.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.InMemory
{
    public class InMemoryCachingProvider : ICachingProvider
    {
        private string _name;
        public string Name => _name;
        private readonly ConcurrentDictionary<string, CacheEntry> _memory;

        public InMemoryCachingProvider(string name)
        {
            _name = name;
            _memory = new ConcurrentDictionary<string, CacheEntry>();
        }

        public void Set<T>(string key, T value, TimeSpan expiresIn)
        {
            CacheEntry cacheEntry = new CacheEntry(key, value, expiresIn);
            _memory.AddOrUpdate(key, cacheEntry, (keyParam, cacheEntryParam) => cacheEntry);
        }

        public CacheValue<T> Get<T>(string key)
        {
            if (_memory.TryGetValue(key, out CacheEntry cacheEntry) && cacheEntry.Value is T)
            {
                return new CacheValue<T>((T)cacheEntry.Value, true);
            }
            else
                return new CacheValue<T>(default(T), false);
        }
    }
}
