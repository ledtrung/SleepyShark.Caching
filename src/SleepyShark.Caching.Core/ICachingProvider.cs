using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Core
{
    public interface ICachingProvider
    {
        string Name { get;  }
        void Set<T>(string key, T value, TimeSpan expiresIn);
        CacheValue<T> Get<T>(string key);
    }
}
