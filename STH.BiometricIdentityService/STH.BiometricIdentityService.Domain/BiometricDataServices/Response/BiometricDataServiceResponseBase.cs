namespace STH.BiometricIdentityService.Domain.BiometricDataServices.Response
{
    public class ResponseBase
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
    }
}