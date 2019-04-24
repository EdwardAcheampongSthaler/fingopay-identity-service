using System.Net;
using STH.BiometricIdentityService.Data.Interfaces;
using STH.BiometricIdentityService.Domain.BiometricDataServices.Request;
using STH.BiometricIdentityService.Domain.BiometricDataServices.Response;
using STH.BiometricIdentityService.Domain.Interfaces;
using STH.BiometricIdentityService.FvtClient.Interfaces;

namespace STH.BiometricIdentityService.Domain.BiometricDataServices
{
    public class FvtBiometricDataService : IBiometricDataService
    {
        private readonly IFvtClientRepository _fvtServerRepository;
        private readonly IBiometricIdentityRepository _biometricIdentityRepository;

        public FvtBiometricDataService(IFvtClientRepository fvtServerRepository, 
            IBiometricIdentityRepository biometricIdentityRepository)
        {
            _fvtServerRepository = fvtServerRepository;
            _biometricIdentityRepository = biometricIdentityRepository;
        }

        public BiometricDataEnrollmentResponse Enroll(BiometricDataEnrollmentRequest request)
        {
            var result = _fvtServerRepository.Enroll(request.Uuid, request.eTemplate, request.vTemplate);

            if ((result == null)|| (result?.Data == null) || (result?.Success == false)){
                return new BiometricDataEnrollmentResponse()
                {
                    Success = false,
                    Message = "Unable to enroll user. See server logs.",
                    StatusCode = (int) HttpStatusCode.InternalServerError
                };
            }

            // enrolled biometric record - add to database against request account.
            var response = _biometricIdentityRepository.AddBiometricUuidToAccount(request.AccountId, result.Uuid);
            if(response == null) return new BiometricDataEnrollmentResponse()
            {
                Success = false,
                Message = "Unable to link biometric to user account. See server logs.",
                StatusCode = (int)HttpStatusCode.InternalServerError
            };


            return new BiometricDataEnrollmentResponse()
                {
                    Account = response.Account,
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
                StatusCode = result.StatusCode,
                Uuid = result.Uuid
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