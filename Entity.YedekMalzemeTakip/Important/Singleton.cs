using System;
using System.Globalization;
using System.Reflection;

namespace Entity.YedekMalzemeTakip.Important
{
    public abstract class Singleton<T> where T : class
    {
        protected static readonly object SyncRoot = new Object();

        private static volatile T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            Type theType = typeof(T);
                            try
                            {
                                _instance = (T)theType.InvokeMember(theType.Name, BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic, null, null, null, CultureInfo.InvariantCulture);
                            }
                            catch (MissingMethodException ex)
                            {
                                throw new TypeLoadException(string.Format(CultureInfo.CurrentCulture, "The type '{0}' singleton tasarým örüntüsünde private constructor kullanilmali.", theType.FullName), ex);
                            }
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
