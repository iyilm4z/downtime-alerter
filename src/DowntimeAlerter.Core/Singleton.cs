using System;
using System.Collections.Generic;

namespace DowntimeAlerter
{
    public abstract class SingletonBase
    {
        static SingletonBase()
        {
            AllSingletons = new Dictionary<Type, object>();
        }

        protected static IDictionary<Type, object> AllSingletons { get; }
    }

    public class Singleton<T> : SingletonBase
    {
        private static T _instance;

        public static T Instance
        {
            get => _instance;
            set
            {
                _instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }
}