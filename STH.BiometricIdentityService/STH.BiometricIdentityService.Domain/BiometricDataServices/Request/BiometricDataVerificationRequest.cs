using System;

namespace STH.BiometricIdentityService.Domain.BiometricDataServices.Request
{
    public class BiometricDataVerificationRequest
    {
        public string Uuid { get; set; }
        public byte[] Data { get; set; }
    }
}