using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace STH.BiometricIdentityService.Infrastructure.Utilities.Authentication.Filters
{
    public class HandleApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var request = context.ActionContext.Request;

            var response = new 
            {
                Message = context.Exception.Message + " " + context.Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, new HttpError()),
                StatusCode = (int)HttpStatusCode.MethodNotAllowed,
                Success = false
            };

            context.Response = request.CreateResponse(HttpStatusCode.BadRequest, response);
        }
    }
}