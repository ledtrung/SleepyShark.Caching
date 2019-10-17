using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SleepyShark.Caching.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.InMemory
{
    public class InMemoryCacheOptionExtensions : ISleepSharkOptionExtensions
    {
        private readonly string _name;

        public InMemoryCacheOptionExtensions(string name)
        {
            _name = name;
        }

        public void AddServices(IServiceCollection services)
        {
            services.TryAddSingleton<ICachingProviderFactory, DefaultCachingProviderFactory>();
            services.AddSingleton<ICachingProvider, InMemoryCachingProvider>(type => 
            {
                return new InMemoryCachingProvider(_name);
            });
        }
    }
}
