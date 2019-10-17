using SleepyShark.Caching.Connector;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSleepySharkCaching(this IServiceCollection services, Action<SleepySharkCacheConnectorOptions> setupAction)
        {
            if (setupAction == null)
                throw new ArgumentNullException();

            var options = new SleepySharkCacheConnectorOptions();
            setupAction(options);
            services.AddSingleton<ISleepySharkCache, SleepySharkCache>(type =>
            {
                return new SleepySharkCache(options.AppId, options.ServerAddress, options.ServerPort);
            });
            return services;
        }
    }
}
