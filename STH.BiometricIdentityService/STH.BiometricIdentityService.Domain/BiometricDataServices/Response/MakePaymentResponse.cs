using System;

namespace STH.BiometricIdentityService.Domain.BiometricDataServices.Response
{
    public class MakePaymentResponse : ResponseBase
    {
        public string UniqueReferenceId { get; set; }

        public DateTime TransactionStartDateTime { get; set; }
        public DateTime TransactionEndDateTime { get; set; }
        public string MerchantId { get; set; }
        public double TotalAmount { get; set; }
        public string TransactionId { get; set; }

    }
}