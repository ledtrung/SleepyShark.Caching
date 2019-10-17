using SleepyShark.Caching.Core.Extensions;
using SleepyShark.Caching.InMemory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InMemoryCachingOptionExtensions
    {
        private const string _name = "DefaultInMemoryCache";

        public static SleepySharkCachingOptions UserInMemoryCache(this SleepySharkCachingOptions options, string name = _name)
        {
            options.RegisterExtension(new InMemoryCacheOptionExtensions(name));
            return options;
        }
    }
}
