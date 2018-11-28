using System;
using System.Net;
using System.Text;
using System.Web.Http;
using STH.BiometricIdentityService.Api.Controllers.Base;
using STH.BiometricIdentityService.Domain.BiometricDataServices.Request;
using STH.BiometricIdentityService.Domain.Interfaces;
using Swashbuckle.Swagger.Annotations;

namespace STH.BiometricIdentityService.Api.Controllers
{
    public class BirController : BaseWebApiController
    {
        private readonly IBiometricDataService _biometricDataService;

        public BirController(IBiometricDataService biometricDataService)
        {
            _biometricDataService = biometricDataService;
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
        public IHttpActionResult MakePayment(int id)
        {
            return Ok("value");
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
        public IHttpActionResult EnrollAndPayment(int id)
        {
            return Ok("value");
        }

        [HttpPost]
        [SwaggerOperation("Enroll")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Enroll()
        {
            if (this == null) return BadRequest();

            var result = _biometricDataService.Enroll(new BiometricDataEnrollmentRequest()
            {
                Data = Encoding.UTF8.GetBytes("This is a template"),
                Uuid = Guid.NewGuid()
            });

            if (result == null)
            {
                return BadRequest();
            }
            return Ok(new {
                Message = "Successfully Enrolled",
                Success = true,
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }

}