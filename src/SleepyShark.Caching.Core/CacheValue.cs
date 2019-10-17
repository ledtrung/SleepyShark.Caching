using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Core
{
    [Serializable]
    public class CacheValue : ICacheResponse
    {
        private bool _isSuccess = false;
        public bool IsSuccess { get => _isSuccess; }
        public byte[] Value { get; set; }

        public CacheValue(byte[] value, bool isSuccess)
        {
            Value = value;
            _isSuccess = isSuccess;
        }
    }
}
