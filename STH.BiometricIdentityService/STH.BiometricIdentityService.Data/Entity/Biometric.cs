using System;

namespace STH.BiometricIdentityService.Data.Entity
{
    public class Biometric
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string EncryptedBIR { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}