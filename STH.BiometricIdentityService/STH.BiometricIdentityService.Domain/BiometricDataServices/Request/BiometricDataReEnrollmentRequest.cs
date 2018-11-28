using System;

namespace STH.BiometricIdentityService.Domain.BiometricDataServices.Request
{
    public class BiometricDataReEnrollmentRequest
    {
        public Guid Uuid { get; set; }
        public byte[] Data { get; set; }
    }
}