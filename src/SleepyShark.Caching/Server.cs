using SleepyShark.Caching.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace SleepyShark.Caching
{
    public class Server
    {
        ICachingProvider _cachingProvider;

        TcpListener server = null;
        public Server(ICachingProvider cachingProvider, string ip, int port)
        {
            _cachingProvider = cachingProvider;
            IPAddress localAddr = IPAddress.Parse(ip);
            server = new TcpListener(localAddr, port);
            server.Start();
            StartListener();
        }

        public void StartListener()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));
                    t.Start(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                server.Stop();
            }
        }

        public void HandleDeivce(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            Byte[] bytes = new Byte[2048];
            int i;

            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    //binaryFormatter.Binder = SleepySharkSerializationBinder.Default;
                    ICacheResponse response;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        ICacheRequest cacheRequest = (ICacheRequest)binaryFormatter.Deserialize(ms);
                        if (cacheRequest is ISetCacheRequest setCacheRequest)
                        {
                            _cachingProvider.Set(setCacheRequest.Key, setCacheRequest.Value, new TimeSpan(0, 0, setCacheRequest.ExpiresIn));
                            response = new SetCacheResponse(true);
                        }
                        else if (cacheRequest is IGetCacheRequest getCacheRequest)
                        {
                            response = _cachingProvider.Get(getCacheRequest.Key);
                        }
                        else
                        {
                            throw new InvalidOperationException("Weird action");
                        }
                    }

                    using (MemoryStream responseMs = new MemoryStream())
                    {
                        binaryFormatter.Serialize(responseMs, response);
                        byte[] outputBytes = responseMs.ToArray();
                        stream.Write(outputBytes, 0, outputBytes.Length);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }
    }
}