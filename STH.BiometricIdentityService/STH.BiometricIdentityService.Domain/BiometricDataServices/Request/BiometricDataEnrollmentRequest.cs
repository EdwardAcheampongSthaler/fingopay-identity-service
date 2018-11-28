using System;

namespace STH.BiometricIdentityService.Domain.BiometricDataServices.Request
{
    public class BiometricDataEnrollmentRequest
    {
        public Guid Uuid { get; set; }
        public byte[] Data { get; set; }
    }
}