using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace STH.BiometricIdentityService.Infrastructure.Utilities.ApiConfiguration.Constraints
{
    public class ApiVersionConstraint : IHttpRouteConstraint
    {
        private readonly string _apiVersion;
        
        public ApiVersionConstraint(string apiVersion)
        {
            _apiVersion = apiVersion;        
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            if ( routeDirection != HttpRouteDirection.UriResolution ){ return false; }
            // match the apiVersion of the media type 
            return
                request.Headers.Accept.Any(
                    x => x.MediaType.Equals(_apiVersion, StringComparison.InvariantCultureIgnoreCase));

        }
    }
}