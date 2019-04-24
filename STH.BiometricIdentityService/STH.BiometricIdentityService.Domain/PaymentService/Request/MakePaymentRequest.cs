namespace STH.BiometricIdentityService.Domain.PaymentService.Request
{
    public class MakePaymentRequest
    {
        public string MerchantId { get; set; }
        public double TotalAmount { get; set; }
        public string TransactionId { get; set; }
    }
}