using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SleepyShark.Caching.Core.Extensions
{
    public class SleepySharkCachingOptions
    {
        public SleepySharkCachingOptions()
        {
            Extensions = new List<ISleepSharkOptionExtensions>();
        }

        public IList<ISleepSharkOptionExtensions> Extensions { get; }

        public void RegisterExtension(ISleepSharkOptionExtensions extension)
        {
            if (extension == null)
                throw new ArgumentNullException();

            Extensions.Add(extension);
        }
    }
}
