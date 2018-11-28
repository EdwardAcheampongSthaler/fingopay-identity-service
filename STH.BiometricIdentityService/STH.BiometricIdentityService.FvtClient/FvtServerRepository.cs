using System;
using System.Collections.Generic;
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
        private readonly ChannelFactory<AuthenticationServiceSoapChannel> _authenticationServiceSoapFactory;
        private readonly AuthenticationServiceSoapChannel _channel;
        private readonly AuthenticationServiceSoapClient _client;

       //  public FvtServerRepository(IAuthenticationServiceSoap authenticationServiceSoap)
       //// public FvtServerRepository()
       // {
       //     _authenticationServiceSoap = authenticationServiceSoap;
        
            
       //     // _channelFactory = channelFactory;
       //    //   _client = new AuthenticationServiceSoapClient(new BasicHttpBinding(),
       //    //      new EndpointAddress("http://fvt.dev.v3.sthaler.io/FVTServer/AuthenticationService.asmx"));
            
       // }

        public FvtClientRepository(IAuthenticationServiceSoap authenticationServiceSoap)
        {
            _authenticationServiceSoap = authenticationServiceSoap;
        }

        public BiometricFvtResult Enroll(Guid uuid, byte[] data)
        {
            var result = _authenticationServiceSoap.Enrol(new EnrolRequest()
            {
                Body = new EnrolRequestBody()
                {
                    fBackupPresent = false,
                    sUUID = uuid.ToString(),
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
                    Success = true,
                    Uuid = uuid,
                    Message = "Success",
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = result.Body.EnrolResult
                };
            }

            return new BiometricFvtResult()
            {
                Success = false,
                Message = "No Match",
                StatusCode = 500,
                Uuid = uuid
            };
        }

        public BiometricFvtResult ReEnroll(Guid uuid, byte[] data)
        {
            var result = _authenticationServiceSoap.ReEnrol(new ReEnrolRequest()
            {
                Body = new ReEnrolRequestBody()
                {
                    fBackupPresent = false,
                    sUUID = uuid.ToString(),
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
                    Success = true,
                    Uuid = uuid,
                    Message = "Success",
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = result.Body.ReEnrolResult
                };
            }

            return new BiometricFvtResult()
            {
                Success = false,
                Message = "No Match",
                StatusCode = 500,
                Uuid = uuid
            };
        }

        public BiometricFvtResult Identify(byte[] data)
        {
            var uuid = Guid.NewGuid();
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
                    Success = true,
                    Uuid = uuid,
                    Message = "Success",
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = result.Body.IdentifyResult
                };
            }

            return new BiometricFvtResult()
            {
                Success = false,
                Message = "No Match",
                StatusCode = 500,
                Uuid = uuid
            };
        }

        public BiometricFvtResult Verify(Guid uuid, byte[] data)
        {
            var result = _authenticationServiceSoap.Verify(new VerifyRequest()
            {
                Body = new VerifyRequestBody()
                {
                    sUUID = uuid.ToString(),
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
                    Success = true,
                    Uuid = uuid,
                    Message = "Success",
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = result.Body.VerifyResult
                };
            }

            return new BiometricFvtResult()
            {
                Success = false,
                Message = "No Match",
                StatusCode = 500,
                Uuid = uuid
            };
        }

        public BiometricFvtResult Delete(Guid uuid)
        {
            var result = _authenticationServiceSoap.Remove(new RemoveRequest()
            {
                Body = new RemoveRequestBody()
                {
                    sUUID = uuid.ToString(),
                }
            });

            if (result != null)
            {
                return new BiometricFvtResult()
                {
                    Success = true,
                    Uuid = uuid,
                    Message = "Success",
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = result.Body.RemoveResult
                };
            }

            return new BiometricFvtResult()
            {
                Success = false,
                Message = "No Match",
                StatusCode = 500,
                Uuid = uuid
            };
        }

        public BiometricServiceTransactionLogResult GetTransactions(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public BiometricServiceTransactionLogResult GetUserTransactions(Guid uuid, DateTime startDate, DateTime endDate)
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
