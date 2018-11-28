using STH.BiometricIdentityService.Domain.BiometricDataServices.Request;
using STH.BiometricIdentityService.Domain.BiometricDataServices.Response;
using STH.BiometricIdentityService.Domain.Interfaces;
using STH.BiometricIdentityService.FvtClient.Interfaces;

namespace STH.BiometricIdentityService.Domain.BiometricDataServices
{
    public class FvtBiometricDataService : IBiometricDataService
    {
        private readonly IFvtClientRepository _fvtServerRepository;

        public FvtBiometricDataService(IFvtClientRepository fvtServerRepository )
        {
            _fvtServerRepository = fvtServerRepository;
        }

        public BiometricDataEnrollmentResponse Enroll(BiometricDataEnrollmentRequest request)
        {
            var result = _fvtServerRepository.Enroll(request.Uuid, request.Data);
            if (result == null) return new BiometricDataEnrollmentResponse();
            return new BiometricDataEnrollmentResponse()
            {
                Success = result.Success,
                Message = result.Message,
                StatusCode = result.StatusCode
            };
        }

        public BiometricDataReEnrollmentResponse ReEnroll(BiometricDataReEnrollmentRequest request)
        {
            var result = _fvtServerRepository.ReEnroll(request.Uuid, request.Data);
            if (result == null) return new BiometricDataReEnrollmentResponse();
            return new BiometricDataReEnrollmentResponse()
            {
                Success = result.Success,
                Message = result.Message,
                StatusCode = result.StatusCode
            };
        }

        public BiometricDataIdentificationResponse Identify(BiometricDataIdentificationRequest request)
        {
            var result = _fvtServerRepository.Identify(request.Data);
            if(result == null) return new BiometricDataIdentificationResponse();
            return new BiometricDataIdentificationResponse()
            {
                Success = result.Success,
                Message = result.Message,
                StatusCode = result.StatusCode
            };
        }

        public BiometricDataVerificationResponse Verify(BiometricDataVerificationRequest request)
        {
            var result = _fvtServerRepository.Verify(request.Uuid,request.Data);
            if (result == null) return new BiometricDataVerificationResponse();
            return new BiometricDataVerificationResponse()
            {
                Success = result.Success,
                Message = result.Message,
                StatusCode = result.StatusCode
            };
        }

        public BiometricDataDeletionResponse Delete(BiometricDataDeletionRequest request)
        {
            var result = _fvtServerRepository.Delete(request.Uuid);
            if (result == null) return new BiometricDataDeletionResponse();
            return new BiometricDataDeletionResponse()
            {
                Success = result.Success,
                Message = result.Message,
                StatusCode = result.StatusCode
            };
        }
    }
}