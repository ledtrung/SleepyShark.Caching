using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Core
{
    public interface ISetCacheRequest : ICacheRequest 
    {
        byte[] Value { get; set; }
        int ExpiresIn { get; set; }
    }

    [Serializable]
    public class SetCacheRequest : ISetCacheRequest
    {
        public string AppId { get; set; }
        public string Key { get; set; }
        public byte[] Value { get; set; }
        public int ExpiresIn { get; set; }

        public SetCacheRequest(string appId, string key, byte[] value, int expiresIn)
        {
            AppId = appId;
            Key = key;
            Value = value;
            ExpiresIn = expiresIn;
        }
    }
}
