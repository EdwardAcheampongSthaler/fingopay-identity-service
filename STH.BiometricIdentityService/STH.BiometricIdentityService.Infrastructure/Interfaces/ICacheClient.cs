using System;
using System.Collections.Generic;

//https://github.com/ServiceStack/ServiceStack/blob/master/src/ServiceStack.Interfaces/CacheAccess

namespace STH.BiometricIdentityService.Infrastructure.Interfaces
{
    public interface ICacheClient
    {
        /// <summary>
        /// Removes the specified item from the cache.
        /// </summary>
        /// <param name="key">The identifier for the item to delete.</param>
        /// <returns>
        /// true if the item was successfully removed from the cache; false otherwise.
        /// </returns>
        bool Remove(string key);

        /// <summary>
        /// Removes the cache for all the keys provided.
        /// </summary>
        /// <param name="keys">The keys.</param>
        void RemoveAll(IEnumerable<string> keys);

        /// <summary>
        /// Retrieves the specified item from the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The identifier for the item to retrieve.</param>
        /// <returns>
        /// The retrieved item, or <value>null</value> if the key was not found.
        /// </returns>
        T Get<T>(string key);

        /// <summary>
        /// Increments the value of the specified key by the given amount. 
        /// The operation is atomic and happens on the server.
        /// A non existent value at key starts at 0
        /// </summary>
        /// <param name="key">The identifier for the item to increment.</param>
        /// <param name="amount">The amount by which the client wants to increase the item.</param>
        /// <returns>
        /// The new value of the item or -1 if not found.
        /// </returns>
        /// <remarks>The item must be inserted into the cache before it can be changed. The item must be inserted as a <see cref="T:System.String"/>. The operation only works with <see cref="System.UInt32"/> values, so -1 always indicates that the item was not found.</remarks>
        long Increment(string key, uint amount);

        /// <summary>
        /// Increments the value of the specified key by the given amount. 
        /// The operation is atomic and happens on the server.
        /// A non existent value at key starts at 0
        /// </summary>
        /// <param name="key">The identifier for the item to increment.</param>
        /// <param name="amount">The amount by which the client wants to decrease the item.</param>
        /// <returns>
        /// The new value of the item or -1 if not found.
        /// </returns>
        /// <remarks>The item must be inserted into the cache before it can be changed. The item must be inserted as a <see cref="T:System.String"/>. The operation only works with <see cref="System.UInt32"/> values, so -1 always indicates that the item was not found.</remarks>
        long Decrement(string key, uint amount);

        /// <summary>
        /// Sets an item into the cache at the cache key specified regardless if it already exists or not.
        /// </summary>
        void Set<T>(string key, T value);
        void Set<T>(string key, T value, DateTime expiresAt);
        void Set<T>(string key, T value, TimeSpan expiresIn);

        /// <summary>
        /// Invalidates all data on the cache.
        /// </summary>
        void FlushAll();

        /// Retrieves multiple items from the cache. 
        /// The default value of T is set for all keys that do not exist.
        /// </summary>
        /// <param name="keys">The list of identifiers for the items to retrieve.</param>
        /// <returns>
        /// a Dictionary holding all items indexed by their key.
        /// </returns>
        IDictionary<string, T> GetAll<T>(IEnumerable<string> keys);

        /// <summary>
        /// Sets multiple items to the cache. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        void SetAll<T>(IDictionary<string, T> values);
    }
}
