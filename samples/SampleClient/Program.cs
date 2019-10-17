using SleepyShark.Caching.Connector;
using System;
using System.Threading;

namespace SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //AsynchronousClient.StartClient();
            string appId = "someApp";
            string ip = "172.22.62.11";
            int port = 4485;

            Client client = new Client(appId, ip, port);
            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    client.Set(appId, "123", new TestClass() { Id = 1, Name = "Name #1", DoB = DateTime.Now });
            //}).Start();
            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    client.Set(appId, "456", new TestClass() { Id = 1, Name = "Name #3", DoB = DateTime.Now.AddDays(-10) });
            //}).Start();

            client.Set(appId, "123", new TestClass() { Id = 1, Name = "Name #1", DoB = DateTime.Now });
            client.Set(appId, "456", new TestClass() { Id = 1, Name = "Name #3", DoB = DateTime.Now.AddDays(-10) });
            TestClass result = client.Get<TestClass>("456");
            Console.ReadLine();
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
