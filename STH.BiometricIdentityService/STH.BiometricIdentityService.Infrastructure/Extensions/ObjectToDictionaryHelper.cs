using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace STH.BiometricIdentityService.Infrastructure.Extensions
{
    /// From
    /// https://gist.github.com/jarrettmeyer/798667
    ///
    public static class ObjectToDictionaryHelper
    {
        //public static IDictionary<string, object> ToDictionary(this object source)
        //{
        //    return source.ToDictionary<object>();
        //}
        public static IDictionary<string, string> ToDictionaryString(this object source)
        {
            return source.ToDictionary<string>();
        }
        public static IDictionary<string, string> ToDictionary<T>(this object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, string>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary(property, source, dictionary);

            return dictionary;
        }

        private static void AddPropertyToDictionary(PropertyDescriptor property, object source, Dictionary<string, string> dictionary)
        {
            object value = property.GetValue(source);
            //if (IsOfType<T>(value))
                dictionary.Add(property.Name, value != null ? value.ToString() : string.Empty);

        }                                           

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}
