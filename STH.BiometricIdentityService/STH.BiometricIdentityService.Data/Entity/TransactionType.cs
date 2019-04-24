using System;

namespace STH.BiometricIdentityService.Data.Entity
{
    public class TransactionType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}