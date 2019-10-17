using SleepyShark.Caching.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace SleepyShark.Caching.Connector
{
    public class SleepySharkCache : ISleepySharkCache
    {
        private readonly TcpClient _client;
        private readonly string _appId;
        public SleepySharkCache(string appId, string server, int port)
        {
            _appId = appId;
            _client = new TcpClient(server, port);
        }

        public bool Set<T>(string key, T value)
        {
            try
            {
                NetworkStream stream = _client.GetStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                SetCacheRequest cacheEntry;
                using (MemoryStream ms = new MemoryStream())
                {
                    binaryFormatter.Serialize(ms, value);
                    cacheEntry = new SetCacheRequest(_appId, key, ms.ToArray(), 300);
                }

                using (MemoryStream cacheEntryMs = new MemoryStream())
                {
                    binaryFormatter.Serialize(cacheEntryMs, cacheEntry);
                    byte[] bytes = cacheEntryMs.ToArray();
                    stream.Write(bytes, 0, bytes.Length);
                }

                Byte[] responseBytes = new Byte[2048];
                stream.Read(responseBytes, 0, responseBytes.Length);
                using (MemoryStream responseMs = new MemoryStream(responseBytes))
                {
                    ICacheResponse cacheResponse = (ICacheResponse)binaryFormatter.Deserialize(responseMs);
                    return cacheResponse.IsSuccess;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }

            return false;
        }

        public T Get<T>(string key)
        {
            try
            {
                NetworkStream stream = _client.GetStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                //binaryFormatter.Binder = SleepySharkSerializationBinder.Default;
                GetCacheRequest getCacheRequest = new GetCacheRequest(_appId, key);

                using (MemoryStream ms = new MemoryStream())
                {
                    binaryFormatter.Serialize(ms, getCacheRequest);
                    byte[] bytes = ms.ToArray();
                    stream.Write(bytes, 0, bytes.Length);
                }

                Byte[] responseBytes = new Byte[2048];
                stream.Read(responseBytes, 0, responseBytes.Length);
                ICacheResponse cacheResponse;
                using (MemoryStream responseMs = new MemoryStream(responseBytes))
                {
                    cacheResponse = (ICacheResponse)binaryFormatter.Deserialize(responseMs);
                }

                if (cacheResponse is CacheValue cacheValue && cacheValue.Value != null)
                {
                    using (MemoryStream resultMs = new MemoryStream(cacheValue.Value))
                    {
                        object rawObject = binaryFormatter.Deserialize(resultMs);
                        if (rawObject is T obj)
                            return obj;
                        else
                            return default(T);
                    }
                }
                else
                    return default(T);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }

            return default(T);
        }
    }
}
