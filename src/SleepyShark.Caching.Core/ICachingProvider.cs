using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Core
{
    public interface ICachingProvider
    {
        string Name { get;  }
        void Set(string key, byte[] value, TimeSpan expiresIn);
        CacheValue Get(string key);
    }
}
