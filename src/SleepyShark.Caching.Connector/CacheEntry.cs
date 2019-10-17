using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Connector
{
    public class CacheEntry<T>
    {
        public T Value { get; set; }
        public string Key { get; set; }

        public CacheEntry(string key, T value)
        {
            Key = key;
            Value = value;
        }
    }
}
