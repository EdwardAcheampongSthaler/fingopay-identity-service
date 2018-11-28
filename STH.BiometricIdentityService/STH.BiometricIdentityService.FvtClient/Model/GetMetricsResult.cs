namespace STH.BiometricIdentityService.FvtClient.Model
{
    public class GetMetricsResult
    {
        public int AttemptsCount { get; set; }
        public int SuccessesCount { get; set; }
        public int MultiplesCount { get; set; }
        public int NotMatchedCount { get; set; }
        public int FailuresCount { get; set; }
    }
}