using System;

namespace STH.BiometricIdentityService.Data.Entity
{
    public class PaymentOption
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}