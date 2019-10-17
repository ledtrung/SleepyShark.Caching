using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Core
{
    public interface ICachingProviderFactory
    {
        ICachingProvider GetCachingProvider(string name);
    }
}
