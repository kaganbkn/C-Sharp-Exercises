
using System;

namespace DesignPatterns
{
    public class Singleton
    {
        private static Singleton _instance;
        public Guid Id { get;}
        private static object _lock; // for thread safe
        private Singleton(Guid id)
        {
            Id = id;
        }

        public static Singleton GetInstance()
        {
            lock (_lock)
            {
                return _instance ?? (_instance = new Singleton(Guid.NewGuid()));
            }
        }
        public static Singleton GetInstanceDoubleCheck() // avoid useless thread lock
        {
            if (_instance != null) return _instance;
            lock (_lock)
            {
                if (_instance == null) // its necessary!!
                    _instance = new Singleton(Guid.NewGuid());
            }
            return _instance;
        }
    }
}
