using STH.BiometricIdentityService.Domain.BiometricDataServices.Request;
using STH.BiometricIdentityService.Domain.BiometricDataServices.Response;

namespace STH.BiometricIdentityService.Domain.PaymentService.Interfaces
{
    public interface IPaymentService
    {
        MakePaymentResponse MakePayment(MakePaymentRequest request);
        RefundPaymentResponse RefundPayment(RefundPaymentRequest request);
    }
}