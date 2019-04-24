using System;

namespace STH.BiometricIdentityService.Domain.BiometricDataServices.Request
{
    public class BiometricDataEnrollmentRequest
    {
        public string AccountId { get; set; }
        public string Uuid { get; set; }
        //public byte[] Data { get; set; }
        public byte[] eTemplate { get; set; }
        public byte[] vTemplate { get; set; }
    }
}