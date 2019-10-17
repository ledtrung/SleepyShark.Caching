using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.InMemory
{
    public class CacheEntry
    {
        public string Key { get; set; }
        public byte[] Value { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }

        public CacheEntry(string key, byte[] value, TimeSpan expiresIn)
        {
            Key = key;
            Value = value;
            ExpiresAt = DateTimeOffset.UtcNow.Add(expiresIn);
        }
    }
}
