using System;

namespace STH.BiometricIdentityService.Data.Entity
{
    public class AccountPaymentOption
    {
        public string Id { get; set; }
        public string PaymentOptionId { get; set; }
        public string PaymentToken { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}