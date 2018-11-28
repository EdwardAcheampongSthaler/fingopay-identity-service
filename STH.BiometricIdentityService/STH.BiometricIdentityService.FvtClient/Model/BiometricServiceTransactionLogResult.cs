using System.Collections.Generic;

namespace STH.BiometricIdentityService.FvtClient.Model
{
    public class BiometricServiceTransactionLogResult
    {
        private IEnumerable<BiometricServiceTransactionLog> BiometricServiceLogs { get; set; }
        public bool Success { get; set; }
    }
}