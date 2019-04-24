using System;

namespace STH.BiometricIdentityService.Data.Entity
{
    public class Transaction
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public DateTime PaymentAccountId { get; set; }
        public string TransactionTypeId { get; set; }
        public string Log { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}