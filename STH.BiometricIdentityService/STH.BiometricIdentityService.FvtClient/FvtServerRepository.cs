using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using STH.BiometricIdentityService.FvtClient.FvtProxy;
using STH.BiometricIdentityService.FvtClient.Interfaces;
using STH.BiometricIdentityService.FvtClient.Model;

namespace STH.BiometricIdentityService.FvtClient
{
    public class FvtClientRepository : IFvtClientRepository
    {
        private readonly IAuthenticationServiceSoap _authenticationServiceSoap;

        public FvtClientRepository(IAuthenticationServiceSoap authenticationServiceSoap)
        {

            _authenticationServiceSoap = authenticationServiceSoap;
        }

        public BiometricFvtResult Enroll(string uuid, byte[] eTemplate, byte[] vTemplate)
        {

            var result = _authenticationServiceSoap.Enrol(new EnrolRequest()
            {
                Body = new EnrolRequestBody()
                {
                    fBackupPresent = false,
                    sUUID = uuid,
                    etTemplate = new EnrolTemplate()
                    {
                        Version = TemplateVersion.Primary,
                        primaryHand = Hand.Left,
                        backupHand = Hand.Left,
                        primaryFinger = Finger.MiddleFinger,
                        backupFinger = Finger.MiddleFinger,
                        primaryEnrolTemplate = eTemplate,
                        backupEnrolTemplate = eTemplate,
                        primaryVerifyTemplate = vTemplate,
                        backupVerifyTemplate = vTemplate
                    }
                }
            });

            if (result != null)
            {
                return new BiometricFvtResult()
                {
                    Success = result.Body.EnrolResult.Result == AuthenticationResultCode.Succeed,
                    Uuid = (result.Body.EnrolResult.nMatches > 0 )? 
                        result.Body.EnrolResult.sUUIDs.First() : uuid,
                    Message = result.Body.EnrolResult.ReturnMessage,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = result.Body.EnrolResult
                };
            }

            return new BiometricFvtResult()
            {
                Success = false,
                Message = $"[ERROR] - FVT Query #Enroll# - Result was Null - Unable to Enroll. Check FVT Server Logs.",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Uuid = uuid
            };
        }

        public BiometricFvtResult ReEnroll(string uuid, byte[] data)
        {
            var result = _authenticationServiceSoap.ReEnrol(new ReEnrolRequest()
            {
                Body = new ReEnrolRequestBody()
                {
                    fBackupPresent = false,
                    sUUID = uuid,
                    etTemplate = new EnrolTemplate()
                    {
                        Version = TemplateVersion.Primary,
                        primaryHand = Hand.Right,
                        backupHand = Hand.Left,
                        primaryFinger = Finger.PointerFinger,
                        backupFinger = Finger.MiddleFinger,
                        primaryEnrolTemplate = data,
                        backupVerifyTemplate = data,
                        primaryVerifyTemplate = data,
                        backupEnrolTemplate = data
                    }
                }
            });

            if (result != null)
            {
                return new BiometricFvtResult()
                {
                    Success = result.Body.ReEnrolResult.Result == AuthenticationResultCode.Succeed,
                    Uuid = (result.Body.ReEnrolResult.nMatches > 0) ?
                        result.Body.ReEnrolResult.sUUIDs.First() : string.Empty,
                    Message = result.Body.ReEnrolResult.ReturnMessage,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = result.Body.ReEnrolResult
                };
            }

            return new BiometricFvtResult()
            {
                Success = false,
                Message = $"[ERROR] - FVT Query #ReEnroll# - Result was Null - Unable to ReEnroll. Check FVT Server Logs.",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Uuid = uuid
            };
        }

        public BiometricFvtResult Identify(byte[] data)
        {
            var uuid = Guid.NewGuid().ToString();
            var result = _authenticationServiceSoap.Identify(new IdentifyRequest()
            {
                Body = new IdentifyRequestBody()
                {
                    template = new VerifyTemplate()
                    {
                        template = data
                    }
                }
            });

            if (result != null)
            {
                return new BiometricFvtResult()
                {
                    Success = result.Body.IdentifyResult.Result == AuthenticationResultCode.Succeed,
                    Uuid = (result.Body.IdentifyResult.nMatches > 0) ?
                        result.Body.IdentifyResult.sUUIDs.First() : string.Empty,
                    Message = result.Body.IdentifyResult.ReturnMessage,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = result.Body.IdentifyResult
                };
            }


            return new BiometricFvtResult()
            {
                Success = false,
                Message = $"[ERROR] - FVT Query #Identify# - Result was Null - Unable to perform Identify. Check FVT Server Logs.",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Uuid = uuid
            };
        }

        public BiometricFvtResult Verify(string uuid, byte[] data)
        {
            var result = _authenticationServiceSoap.Verify(new VerifyRequest()
            {
                Body = new VerifyRequestBody()
                {
                    template = new VerifyTemplate()
                    {
                        template = data
                    }
                }
            });

            if (result != null)
            {
                return new BiometricFvtResult()
                {
                    Success = result.Body.VerifyResult.Result == AuthenticationResultCode.Succeed,
                    Uuid = (result.Body.VerifyResult.sUUIDs.Count > 0) ?
                        result.Body.VerifyResult.sUUIDs.First() : string.Empty,
                    Message = result.Body.VerifyResult.ReturnMessage,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = result.Body.VerifyResult
                };
            }


            return new BiometricFvtResult()
            {
                Success = false,
                Message = $"[ERROR] - FVT Query #Verify# - Result was Null - Unable to perform Verify. Check FVT Server Logs.",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Uuid = uuid
            };
        }

        public BiometricFvtResult Delete(string uuid)
        {
            var result = _authenticationServiceSoap.Remove(new RemoveRequest()
            {
                Body = new RemoveRequestBody()
                {
                    sUUID = uuid,
                }
            });

            if (result != null)
            {
                return new BiometricFvtResult()
                {
                    Success = result.Body.RemoveResult.Result == AuthenticationResultCode.Succeed,
                    Uuid = (result.Body.RemoveResult.nMatches > 0) ?
                        result.Body.RemoveResult.sUUIDs.First() : string.Empty,
                    Message = result.Body.RemoveResult.ReturnMessage,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = result.Body.RemoveResult
                };
            }


            return new BiometricFvtResult()
            {
                Success = false,
                Message = $"[ERROR] - FVT Query #Delete# - Result was Null - Unable to perform Delete. Check FVT Server Logs.",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Uuid = uuid
            };
        }

        public BiometricServiceTransactionLogResult GetTransactions(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public BiometricServiceTransactionLogResult GetUserTransactions(string uuid, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTransactionLog(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public GetMetricsResult GetIdMetrics(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public GetMetricsResult GetVerMetrics(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Guid> GetUsers(int noOfUsers)
        {
            throw new NotImplementedException();
        }

        public GetVersionResult DeleteAllUsers()
        {
            throw new NotImplementedException();
        }

        public BiometricFvtResult GetVersion()
        {
            throw new NotImplementedException();
        }

        public bool SetLicense(string license)
        {
            throw new NotImplementedException();
        }
    }
}
