using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.AccessControl;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using STH.BiometricIdentityService.Api.Controllers.Base;
using STH.BiometricIdentityService.Api.Model;
using STH.BiometricIdentityService.Domain.BiometricDataServices.Request;
using STH.BiometricIdentityService.Domain.Interfaces;
using STH.BiometricIdentityService.Domain.PaymentService.Interfaces;
using Swashbuckle.Swagger.Annotations;

namespace STH.BiometricIdentityService.Api.Controllers
{
    public class BirController : BaseWebApiController
    {
        private readonly IBiometricDataService _biometricDataService;
        private readonly IPaymentService _paymentService;

        public BirController(IBiometricDataService biometricDataService, IPaymentService paymentService)
        {
            _biometricDataService = biometricDataService;
            _paymentService = paymentService;


            // var paymentservice = new CardStreamPaymentService();
        }


        [HttpGet]
        [SwaggerOperation("Get")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Get()
        {
            return Ok(new
            {
                Message = "Sthaler Biometric Identity Service is running!"
            });
        }


        [HttpPost]
        [SwaggerOperation("RegisterTerminal")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult RegisterTerminal()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        [HttpPost]
        [SwaggerOperation("GetServiceStatus")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult GetServiceStatus(int id)
        {
            return Ok("value");
        }

        [HttpPost]
        [SwaggerOperation("MakePayment")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult MakePayment(BiometricPaymentRequest request)
        {
            if (request == null) return BadRequest();
            var identityResponse = _biometricDataService.Identify(new BiometricDataIdentificationRequest()
            {
                Data = Convert.FromBase64String(request.Template), //Encoding.UTF8.GetBytes("This is a template"),

            });
            if (identityResponse == null) return InternalServerError();

            var uuid = identityResponse.Uuid;
            // store the uuid against a user - if unknown fail.

            //if known try make a payment - user has entered card details in already or not.

            // make a payment.
            var makePaymentResponse = _paymentService.MakePayment(new MakePaymentRequest()
            {
                TransactionId = request.TransactionId,
                MerchantId = request.MerchantId,
                TotalAmount = request.TotalAmount
            });

            if (makePaymentResponse == null || makePaymentResponse.Success == false)
                return BadRequest($"Unable to authorise payment - please review payment options. Transaction Id: {request.TransactionId}");

            return Ok(new BiometricPaymentResponse
            {
                TransactionId = makePaymentResponse.TransactionId,
                MerchantId = makePaymentResponse.MerchantId,
                Success = makePaymentResponse.Success,
                UniqueReferenceId = Guid.NewGuid().ToString(),                
                Message = $"Payment authorised. Unique Reference Id: {makePaymentResponse.UniqueReferenceId}",               
                StatusCode = makePaymentResponse.StatusCode,
                InitialStartDateTime = DateTime.UtcNow,
                RemoteProcessEndDateTime = makePaymentResponse.TransactionEndDateTime,
                RemoteProcessStartDateTime = makePaymentResponse.TransactionStartDateTime,
                ResponseDateTime = DateTime.UtcNow
            });
        }


        [HttpPost]
        [SwaggerOperation("IdentifyAndPay")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult IdentifyAndPay(IdentifyRequest request)
        {
            if (request == null) return BadRequest();
            var identityResponse = _biometricDataService.Identify(new BiometricDataIdentificationRequest()
            {
                Data = Convert.FromBase64String(request.IdentificationTemplate), //Encoding.UTF8.GetBytes("This is a template"),

            });
            if (identityResponse == null) return InternalServerError();

            var uuid = identityResponse.Uuid;
            // store the uuid against a user - if unknown fail.

            //if known try make a payment - user has entered card details in already or not.

            // make a payment.
            var makePaymentResponse = _paymentService.MakePayment(new MakePaymentRequest()
            {
                TransactionId = "",
                MerchantId = "",
                TotalAmount = 021
            });

            if (makePaymentResponse == null || makePaymentResponse.Success == false)
                return BadRequest($"Unable to authorise payment - please review payment options. Transaction Id: {request.Errors}");

            return Ok(new BiometricPaymentResponse
            {
                TransactionId = makePaymentResponse.TransactionId,
                MerchantId = makePaymentResponse.MerchantId,
                Success = makePaymentResponse.Success,
                UniqueReferenceId = Guid.NewGuid().ToString(),
                Message = $"Payment authorised. Unique Reference Id: {makePaymentResponse.UniqueReferenceId}",
                StatusCode = makePaymentResponse.StatusCode,
                InitialStartDateTime = DateTime.UtcNow,
                RemoteProcessEndDateTime = makePaymentResponse.TransactionEndDateTime,
                RemoteProcessStartDateTime = makePaymentResponse.TransactionStartDateTime,
                ResponseDateTime = DateTime.UtcNow
            });
        }

        [HttpPost]
        [SwaggerOperation("CancelPayment")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult CancelPayment(int id)
        {
            return Ok("value");
        }

        [HttpPost]
        [SwaggerOperation("EnrollAndPayment")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult EnrollAndPayment(BiometricEnrollAndPaymentRequest request)
        {
            if (request == null) return BadRequest();
            var uuid = Guid.NewGuid().ToString();
            var identityResponse = _biometricDataService.Enroll(new BiometricDataEnrollmentRequest()
            {
                eTemplate = Convert.FromBase64String(request.Template),
                Uuid = uuid

            });
            if (identityResponse == null) return InternalServerError();

            // store the new uuid against an account. --> enrollment codes.
            //if enrollmentcode == enrollmentcode - get user account id.
            // store user account id and uuid.
            // store the uuid against a user - if unknown fail.
            //if known try make a payment - user has entered card details in already or not.

            // make a payment.
            var makePaymentResponse = _paymentService.MakePayment(new MakePaymentRequest()
            {
                TransactionId = request.MerchantId,
                MerchantId = request.MerchantId,
                TotalAmount = request.TotalAmount
            });

            if (makePaymentResponse == null)
            {
                return Ok(new BiometricEnrollAndPaymentResponse
                {
                    Message = $"Unable to authorise payment : {uuid}",
                    Success = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Transaction = request,
                    UniqueReferenceId = Guid.NewGuid().ToString(),
                    RemoteProcessStartDateTime = DateTime.UtcNow,
                    RemoteProcessEndDateTime = DateTime.UtcNow,
                    InitialStartDateTime = DateTime.UtcNow,
                    ResponseDateTime = DateTime.UtcNow,
                    Username = uuid

                });
            }

            // store payment details against Account

            return Ok(new BiometricEnrollAndPaymentResponse
            {
                Message = $"Successfully Enrolled {uuid}",
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Transaction = request,
                UniqueReferenceId = makePaymentResponse.UniqueReferenceId,
                RemoteProcessStartDateTime = makePaymentResponse.TransactionStartDateTime,
                RemoteProcessEndDateTime = makePaymentResponse.TransactionEndDateTime,
                InitialStartDateTime = DateTime.UtcNow,
                ResponseDateTime = DateTime.UtcNow,
                Username = uuid

            });
        }

        [HttpPost]
        [SwaggerOperation("Enroll")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Enroll(EnrollIdentityRequest request)
        {
            if (request == null) return BadRequest();
            var uuid = Guid.NewGuid();
            var result = _biometricDataService.Enroll(new BiometricDataEnrollmentRequest()
            {
                eTemplate = Convert.FromBase64String(request.EnrollmentTemplate),
                vTemplate = Convert.FromBase64String(request.VerificationTemplate),
                Uuid = uuid.ToString()
            });

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                Message = $"Successfully Enrolled {uuid}",
                Success = true,
                StatusCode = (int)HttpStatusCode.OK
            });
        }


        [HttpDelete]
        [SwaggerOperation("Remove")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Remove(RemoveIdentityRequest request)
        {
            if (request == null) return BadRequest();

            var result = _biometricDataService.Delete(new BiometricDataDeletionRequest()
            {
                Uuid = request.Uuid.ToString()
            });

            if (result == null)
            {
                return BadRequest();
            }
            return Ok(new
            {
                Message = $"Successfully Removed {request.Uuid}",
                Success = true,
                StatusCode = (int)HttpStatusCode.OK
            });
        }


    }

    public class BiometricEnrollAndPaymentRequest
    {
        public string AccountId { get; set; }
        public string EnrollmentPinCode { get; set; }
        public string Template { get; set; }
        public IEnumerable<BasketItem> BasketItems { get; set; }
        public string MerchantId { get; set; }
        public double TotalAmount { get; set; }
        public string TransactionId { get; set; }
        public string Currency { get; set; }
    }

    public class BiometricPaymentRequest
    {
        public string Template { get; set; }
        public IEnumerable<BasketItem> BasketItems { get; set; }
        public string MerchantId { get; set; }
        public double TotalAmount { get; set; }
        public string TransactionId { get; set; }
        public string Currency { get; set; }
    }

    public class BiometricEnrollAndPaymentResponse : IdentityCheckResponse
    {
        public string Username { get; set; }
        public BiometricEnrollAndPaymentRequest Transaction { get; set; }
        public string UniqueReferenceId { get; set; }
        public DateTime InitialStartDateTime { get; set; }
        public DateTime RemoteProcessStartDateTime { get; set; }
        public DateTime RemoteProcessEndDateTime { get; set; }
        public DateTime ResponseDateTime { get; set; }
    }
    public class BiometricPaymentResponse : IdentityCheckResponse
    {
        public string UniqueReferenceId { get; set; }
        public string MerchantId { get; set; }
        public string TransactionId { get; set; }
        public DateTime InitialStartDateTime { get; set; }
        public DateTime RemoteProcessStartDateTime { get; set; }
        public DateTime RemoteProcessEndDateTime { get; set; }
        public DateTime ResponseDateTime { get; set; }
    }
    public class IdentifyRequest
    {
        [JsonProperty("iTemplate")]
        public string IdentificationTemplate{ get; set; }
        [JsonProperty("errors")]
        public string Errors { get; set; }
    }

    public class EnrollIdentityRequest
    {
        //public byte[] Data { get; set; }
        //public string Data { get; set; }
        [JsonProperty("eTemplate")]
        public string EnrollmentTemplate { get; set; }
        [JsonProperty("vTemplate")]
        public string VerificationTemplate { get; set; }
    }


    public class RemoveIdentityRequest
    {
        public string Uuid { get; set; }
    }
    public class BasketItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public string Barcode { get; set; }
        public string Currency { get; set; }
    }

}