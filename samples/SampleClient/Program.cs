using Microsoft.Extensions.DependencyInjection;
using SleepyShark.Caching.Connector;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSleepySharkCaching(options =>
            {
                options.AppId = "sampleApp";
                options.ServerAddress = GetLocalIPAddress();
                options.ServerPort = 4485;
            });

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ISleepySharkCache cache = serviceProvider.GetService<ISleepySharkCache>();

            cache.Set("123", new TestClass() { Id = 1, Name = "Name #1", DoB = DateTime.Now });
            cache.Set("456", new TestClass() { Id = 1, Name = "Name #3", DoB = DateTime.Now.AddDays(-10) });
            TestClass result = cache.Get<TestClass>("456");
            Console.ReadLine();
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

    [Serializable]
    public class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DoB { get; set; }
    }
}
