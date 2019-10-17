using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Core
{
    [Serializable]
    public class CacheValue<T>
    {
        public T Value { get; set; }
        public bool HasValue { get; set; }

        public CacheValue(T value, bool hasValue)
        {
            Value = value;
            HasValue = hasValue;
        }
    }
}
