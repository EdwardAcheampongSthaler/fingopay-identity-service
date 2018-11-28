using System.Collections.Generic;

namespace STH.BiometricIdentityService.FvtClient.Model
{
    public class GetVersionResult
    {
        public IEnumerable<string> Data { get; set; }
        public bool Success { get; set; }
    }
}