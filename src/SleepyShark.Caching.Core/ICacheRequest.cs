using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Core
{
    public interface ICacheRequest
    {
        string AppId { get; set; }
        string Key { get; set; }
    }
}
