using STH.BiometricIdentityService.Domain.BiometricDataServices.Request;
using STH.BiometricIdentityService.Domain.BiometricDataServices.Response;

namespace STH.BiometricIdentityService.Domain.Interfaces
{
    public interface IBiometricDataService
    {
        BiometricDataEnrollmentResponse Enroll(BiometricDataEnrollmentRequest request);
        BiometricDataReEnrollmentResponse ReEnroll(BiometricDataReEnrollmentRequest request);
        BiometricDataIdentificationResponse Identify(BiometricDataIdentificationRequest request);
        BiometricDataVerificationResponse Verify(BiometricDataVerificationRequest request);
        BiometricDataDeletionResponse Delete(BiometricDataDeletionRequest request);
    }
}