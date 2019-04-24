using System;
using STH.BiometricIdentityService.Domain.BiometricDataServices.Response;

namespace STH.BiometricIdentityService.Domain.PaymentService.Response
{
    public class RefundPaymentResponse : ResponseBase
    {
        public string UniqueReferenceId { get; set; }

        public DateTime TransactionStartDateTime { get; set; }
        public DateTime TransactionEndDateTime { get; set; }
        public string MerchantId { get; set; }
        public Double TotalAmount { get; set; }
        public string TransactionId { get; set; }

    }
}