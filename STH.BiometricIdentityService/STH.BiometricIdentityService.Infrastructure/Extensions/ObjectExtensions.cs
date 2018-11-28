using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace STH.BiometricIdentityService.Infrastructure.Extensions
{
	public static class ObjectExtensions
    {
        private static readonly AppLogger Logger = AppLogger.Create(typeof(ObjectExtensions));

		public static string ToXML(this object obj)
		{
			var ns = new XmlSerializerNamespaces();
			ns.Add("", "");
			var stringwriter = new System.IO.StringWriter();
			var serializer = new XmlSerializer(obj.GetType());
			serializer.Serialize(stringwriter, obj,ns);
			return stringwriter.ToString();
		}

		public static string ToJson(this object obj)
		{
			return JsonConvert.SerializeObject(obj); 
		}

		public static T XmlDeserialize<T>(this string str)
		{
			var serializer = new XmlSerializer(typeof(T));
			var stringReader = new StringReader(str);
		    T obj = default(T);

            try
            {
                return obj = (T) serializer.Deserialize(stringReader);
            }
            catch (Exception exception)
            {
                Logger.ErrorException("Error deserializing string = " + str, exception);
                
            }
            finally
            {
               stringReader.Dispose(); 
            }
		    
			return obj;
		}
	}
}
