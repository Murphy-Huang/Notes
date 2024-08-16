using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.Util
{
    public class SingleTonBase<T> where T : class
    {
        private static T _instance;
        private static readonly object _locker = new object();

        public static T instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}