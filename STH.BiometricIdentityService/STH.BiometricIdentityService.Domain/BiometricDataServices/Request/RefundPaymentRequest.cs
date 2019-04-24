using System;

namespace STH.BiometricIdentityService.Domain.BiometricDataServices.Request
{
    public class RefundPaymentRequest
    {
        public string MerchantId { get; set; }
        public double TotalAmount { get; set; }
        public string TransactionId { get; set; }
    }
}