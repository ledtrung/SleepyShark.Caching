using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SleepyShark.Caching.Core
{
    public class SleepySharkSerializationBinder : SerializationBinder
    {
        private static SerializationBinder defaultBinder = new BinaryFormatter().Binder;


        public override Type BindToType(string assemblyName, string typeName)
        {
            if (assemblyName.Equals("NA"))
                return Type.GetType(typeName);
            else
                return defaultBinder.BindToType(assemblyName, typeName);
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            // specify a neutral code for the assembly name to be recognized by the BindToType method.
            assemblyName = "SleepyShark.Caching.Core";
            typeName = serializedType.FullName;
        }

        private static object locker = new object();
        private static SleepySharkSerializationBinder _default = null;

        public static SleepySharkSerializationBinder Default
        {
            get
            {
                lock (locker)
                {
                    if (_default == null)
                        _default = new SleepySharkSerializationBinder();
                }
                return _default;
            }
        }
    }
}
