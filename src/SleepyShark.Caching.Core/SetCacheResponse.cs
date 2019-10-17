using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Core
{
    [Serializable]
    public class SetCacheResponse : ICacheResponse
    {
        private bool _isSuccess = false;
        public bool IsSuccess { get => _isSuccess; }

        public SetCacheResponse(bool isSuccess)
        {
            _isSuccess = isSuccess;
        }
    }
}
