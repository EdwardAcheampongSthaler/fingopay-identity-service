using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using STH.BiometricIdentityService.Infrastructure.Interfaces;

namespace STH.BiometricIdentityService.Infrastructure.Utilities.Caching
{
    public class CacheClient : ICacheClient
    {
        private Cache Cache { 
            get { return HttpRuntime.Cache; } 
        }

        private string Prefix = "CacheClient-";

        private string GetKey(string key)
        {
            return Prefix + key;
        }

        public bool Remove(string key)
        {
            return Cache.Remove(GetKey(key)) != null;
        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                Remove(GetKey(key));
            }
        }

        public T Get<T>(string key)
        {
            object result = Cache.Get(GetKey(key));
            return (T) (result ?? default(T));
        }

        public long Increment(string key, uint amount)
        {
            var value = Get<long>(GetKey(key));
            value += amount;
            Set(GetKey(key), value);
            return value;
        }

        public long Decrement(string key, uint amount)
        {
            var value = Get<long>(GetKey(key));
            value -= amount;
            Set(GetKey(key), value);
            return value;
        }

        public void Set<T>(string key, T value)
        {
            Cache[GetKey(key)] = value;
        }
       

        public void Set<T>(string key, T value, DateTime expiresAt)
        {
            Cache.Add(GetKey(key), value, null, expiresAt, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        public void Set<T>(string key, T value, TimeSpan expiresIn)
        {
            Cache.Add(GetKey(key), value, null, Cache.NoAbsoluteExpiration, expiresIn, CacheItemPriority.Normal, null);
        }

        public void FlushAll()
        {
            IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var key = enumerator.Key as string;
                if (key != null && key.StartsWith(Prefix))
                {
                    Cache.Remove(key);
                }
            }
        }

        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            return keys.ToDictionary(x => x, Get<T>);
        }

        public void SetAll<T>(IDictionary<string, T> values)
        {
            foreach (var pair in values)
            {
                Set(pair.Key, pair.Value);
            }
        }
    }
}