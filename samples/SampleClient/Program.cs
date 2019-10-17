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

            Client client = new Client();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                client.Connect("192.168.100.44", "Hello I'm Device 1...");
            }).Start();
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                client.Connect("192.168.100.44", "Hello I'm Device 2...");
            }).Start();
            Console.ReadLine();
        }
    }
}
