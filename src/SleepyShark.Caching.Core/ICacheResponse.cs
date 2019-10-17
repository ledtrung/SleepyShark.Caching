using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Core
{
    public interface ICacheResponse
    {
        bool IsSuccess { get; }
    }
}
