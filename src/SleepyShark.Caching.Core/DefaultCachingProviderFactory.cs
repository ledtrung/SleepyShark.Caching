using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyShark.Caching.Core
{
    public class DefaultCachingProviderFactory : ICachingProviderFactory
    {
        private readonly IEnumerable<ICachingProvider> _cachingProviders;

        public DefaultCachingProviderFactory(IEnumerable<ICachingProvider> cachingProviders)
        {
            _cachingProviders = cachingProviders;
        }

        public ICachingProvider GetCachingProvider(string name)
        {
            return _cachingProviders.FirstOrDefault(c => c.Name.Equals(name));
        }
    }
}
