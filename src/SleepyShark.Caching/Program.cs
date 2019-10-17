using SleepyShark.Caching.Core;
using SleepyShark.Caching.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SleepyShark.Caching
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<ICachingProvider> lst = new List<ICachingProvider>();
            //lst.Add(new InMemoryCachingProvider("m1") as ICachingProvider);
            //ICachingProviderFactory cachingProviderFactory = new DefaultCachingProviderFactory(lst.AsEnumerable());
            //ICachingProvider cachingProvider = cachingProviderFactory.GetCachingProvider("m1");

            //cachingProvider.Set("something", new SomeClass() { Name = "something#1", Value = 12345 }, new System.TimeSpan(0, 5, 0));
            //CacheValue<SomeClass> value = cachingProvider.Get<SomeClass>("something");

            //AsynchronousSocketListener.StartListening();

            Thread t = new Thread(delegate ()
            {
                // replace the IP with your system IP Address...
                Server myserver = new Server("192.168.100.44", 4485);
            });
            t.Start();

            Console.WriteLine("Server Started...!");

        }
    }

    public class SomeClass
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
