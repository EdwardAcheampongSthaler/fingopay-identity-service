using System;
using System.Collections.Generic;
using STH.BiometricIdentityService.Infrastructure.Interfaces;

namespace STH.BiometricIdentityService.Infrastructure
{
    public class NullCacheClient : ICacheClient
    {
        public static readonly NullCacheClient Instance = new NullCacheClient();

        private NullCacheClient()
        {
        }

        public bool Remove(string key)
        {
            return false;
        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            
        }

        public T Get<T>(string key)
        {
            return default(T);
        }

        public long Increment(string key, uint amount)
        {
            return 0;
        }

        public long Decrement(string key, uint amount)
        {
            return 0;
        }

        public void Set<T>(string key, T value)
        {
           
        }

        public void Set<T>(string key, T value, DateTime expiresAt)
        {
            
        }

        public void Set<T>(string key, T value, TimeSpan expiresIn)
        {
            
        }

        public void FlushAll()
        {
            
        }

        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            return new Dictionary<string, T>();
        }

        public void SetAll<T>(IDictionary<string, T> values)
        {
         
        }
    }
}
