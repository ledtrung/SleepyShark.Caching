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

        public void Set(string key, byte[] value, TimeSpan expiresIn)
        {
            CacheEntry cacheEntry = new CacheEntry(key, value, expiresIn);
            _memory.AddOrUpdate(key, cacheEntry, (keyParam, cacheEntryParam) => cacheEntry);
        }

        public CacheValue Get(string key)
        {
            if (_memory.TryGetValue(key, out CacheEntry cacheEntry))
            {
                return new CacheValue(cacheEntry.Value, true);
            }
            else
                return new CacheValue(null, false);
        }
    }
}
