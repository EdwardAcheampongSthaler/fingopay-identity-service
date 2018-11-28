using System.Web;
using System.Web.SessionState;
using STH.BiometricIdentityService.Infrastructure.Interfaces;

namespace STH.BiometricIdentityService.Infrastructure.Utilities.Caching
{
    public class Session : ISession
    {
        private static HttpSessionState CurrentSession
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Session;
                }
                return null;
            }
        }

        private bool HasSession
        {
            get { return CurrentSession != null; }
        }

        public object this[string key]
        {
            get { return Get<object>(key); }
            set { Set(key, value); }
        }

        public void Set<T>(string key, T value)
        {
            if(HasSession)
                CurrentSession.Add(key, value);
        }

        private string GetKey<T>()
        {
            return typeof (T).FullName;
        }

        public void Set<T>(T value)
        {
            Set(GetKey<T>(), value);
        }

        public T Get<T>(string key)
        {
            if(HasSession)
            return (T) (CurrentSession[key] ?? default(T));
            return default (T);
        }

        public T Get<T>()
        {
            return Get<T>(GetKey<T>());
        }

        public bool Contains<T>()
        {
           return Contains(GetKey<T>());
        }

        public bool Contains(string key)
        {
            if (HasSession)
            {
                return CurrentSession[key] != null;
            }
            return false;
        }

        public void Clear<T>()
        {
            Clear(GetKey<T>());
            
        }

        public void Clear(string key)
        {
            if (HasSession && Contains(key))
                CurrentSession[key] = null;
        }
    }

}