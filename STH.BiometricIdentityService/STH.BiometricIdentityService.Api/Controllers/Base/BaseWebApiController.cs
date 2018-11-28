using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace STH.BiometricIdentityService.Api.Controllers.Base
{
    public class BaseWebApiController : ApiController
    {
        protected string GetCurrentClaimIdentity()
        {
            var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var result = string.Empty;
            if (principal == null) return result;
            var identity =
                principal.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Name);
            if (identity != null)
            {
                result = identity.Value;
            }
            return result;
        }

        protected string GetDeviceUId()
        {
            var duid = string.Empty;
            try
            {
                IEnumerable<string> outputList;
                Request.Headers.TryGetValues("DeviceUId", out outputList);
                
                if (outputList != null)
                {
                    var enumerable = outputList.ToArray();
                    if (enumerable.Any())
                    {
                        duid = enumerable.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                // handle exception

            }

            return duid;
        }
    }
}