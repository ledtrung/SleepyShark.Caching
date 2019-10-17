using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Core
{
    public interface IGetCacheRequest : ICacheRequest { }

    [Serializable]
    public class GetCacheRequest : IGetCacheRequest
    {
        public string AppId { get; set; }
        public string Key { get; set; }

        public GetCacheRequest(string appId, string key)
        {
            AppId = appId;
            Key = key;
        }
    }
}
