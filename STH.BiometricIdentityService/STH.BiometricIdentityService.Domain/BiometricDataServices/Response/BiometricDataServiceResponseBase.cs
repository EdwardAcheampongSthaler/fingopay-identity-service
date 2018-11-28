namespace STH.BiometricIdentityService.Domain.BiometricDataServices.Response
{
    public class BiometricDataServiceResponseBase
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
    }
}