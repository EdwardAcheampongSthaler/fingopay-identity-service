using System;

namespace STH.BiometricIdentityService.FvtClient.Model
{
    public class BiometricFvtResult: BiometricResultBase
    {
        public Guid? Uuid { get; set; }
        public dynamic Data { get; set; }
    }
}