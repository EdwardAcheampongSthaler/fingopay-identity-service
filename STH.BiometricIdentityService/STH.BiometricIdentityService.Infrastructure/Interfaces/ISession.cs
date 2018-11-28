
//https://github.com/ServiceStack/ServiceStack/blob/master/src/ServiceStack.Interfaces/CacheAccess

namespace STH.BiometricIdentityService.Infrastructure.Interfaces
{
    public interface ISession
    {
        /// <summary>
        /// Store and Retrieve any object at key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// Set a typed value at key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set<T>(string key, T value);

        /// <summary>
        /// Set a typed value. The key is equal to the type name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        void Set<T>(T value);
        
        /// <summary>
        /// Get a typed value at key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>Cached value or null</returns>
        T Get<T>(string key);

        /// <summary>
        /// Get a typed value. The key is equal to the type name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Cached value or null</returns>
        T Get<T>();

        bool Contains<T>();
        bool Contains(string key);

        void Clear<T>();
        void Clear(string key);
    }
}