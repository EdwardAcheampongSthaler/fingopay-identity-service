using Newtonsoft.Json.Serialization;

namespace STH.BiometricIdentityService.Infrastructure.Utilities.ApiConfiguration
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}