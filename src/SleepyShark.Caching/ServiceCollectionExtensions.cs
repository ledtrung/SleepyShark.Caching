using SleepyShark.Caching.Core.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSleepySharkCaching(this IServiceCollection services, Action<SleepySharkCachingOptions> setupAction)
        {
            if (setupAction == null)
                throw new ArgumentNullException();

            var options = new SleepySharkCachingOptions();
            setupAction(options);
            foreach (var serviceExtension in options.Extensions)
            {
                serviceExtension.AddServices(services);
            }
            services.AddSingleton(options);

            return services;
        }
    }
}
