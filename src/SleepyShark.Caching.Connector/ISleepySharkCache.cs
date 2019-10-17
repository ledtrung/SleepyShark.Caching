using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Connector
{
    public interface ISleepySharkCache
    {
        bool Set<T>(string key, T value);
        T Get<T>(string key);
    }
}
