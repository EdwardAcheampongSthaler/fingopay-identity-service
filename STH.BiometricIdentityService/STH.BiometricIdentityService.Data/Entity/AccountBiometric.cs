using System;

namespace STH.BiometricIdentityService.Data.Entity
{
    public class AccountBiometric
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string BiometricId { get; set; }
        public bool Active { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}