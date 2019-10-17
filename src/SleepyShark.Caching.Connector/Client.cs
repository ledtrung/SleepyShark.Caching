using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace SleepyShark.Caching.Connector
{
    public class Client
    {
        private readonly TcpClient _client;
        public Client(string server)
        {
            Int32 port = 4485;
            TcpClient client = new TcpClient(server, port);
        }

        public void Connect(String server, String message)
        {
            try
            {
                NetworkStream stream = _client.GetStream();
                int count = 0;
                while (count++ < 3)
                {
                    // Translate the Message into ASCII.
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    // Send the message to the connected TcpServer. 
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", message);
                    // Bytes Array to receive Server Response.
                    data = new Byte[256];
                    String response = String.Empty;
                    // Read the Tcp Server Response Bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", response);
                    Thread.Sleep(2000);
                }
                stream.Close();
                _client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            Console.Read();
        }

        public void Set<T>(string server, string key, T value)
        {
            try
            {
                NetworkStream stream = _client.GetStream();

                CacheEntry<T> cacheEntry = new CacheEntry<T>(key, value);

                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, cacheEntry);
                Console.WriteLine("Sent!");

                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }

        public T Get<T>(string key)
        { 
            NetworkStream stream = _client.GetStream();

        }
    }
}
