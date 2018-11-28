using System;

namespace STH.BiometricIdentityService.Api.Model
{
    public class IdentityCheckRequest
    {
        public byte[] Data { get; set; }
        public IdentityRequestType IdentityRequestType { get; set; }
        public int Timeout { get; set; } // time to lapse before error
        public Guid TransactionId { get; set; }
        public string Signature { get; set; }
    }
}