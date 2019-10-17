using Microsoft.Extensions.DependencyInjection;
using SleepyShark.Caching.Core;
using SleepyShark.Caching.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SleepyShark.Caching
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSleepySharkCaching(options =>
            {
                options.UserInMemoryCache("InMemoryCache");
            });

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var cachingProviderFactory = serviceProvider.GetService<ICachingProviderFactory>();
            ICachingProvider cachingProvider = cachingProviderFactory.GetCachingProvider("InMemoryCache");

            Thread t = new Thread(() =>
            {
                Server myserver = new Server(cachingProvider, GetLocalIPAddress(), ServerConfiguration.ListenerPort);
            });
            t.Start();
            Console.WriteLine("Server Started...!");
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
