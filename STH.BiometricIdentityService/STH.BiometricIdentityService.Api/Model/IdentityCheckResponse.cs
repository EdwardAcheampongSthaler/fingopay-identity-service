namespace STH.BiometricIdentityService.Api.Model
{
    public class IdentityCheckResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}