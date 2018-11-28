using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using STH.BiometricIdentityService.Infrastructure.Utilities.Authentication.ActionResults;

namespace STH.BiometricIdentityService.Infrastructure.Utilities.Authentication.Filters
{
    public class ProspectAuthenticationFilter : IAuthenticationFilter
    {
        private readonly ISecurityCredentialService _securityCredentialService;

        public ProspectAuthenticationFilter(ISecurityCredentialService securityCredentialService)
        {
            _securityCredentialService = securityCredentialService;
        }

        public bool AllowMultiple { get { return false; } }
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context,
                                          CancellationToken cancellationToken)
        {
            //context.Result =  new UnauthorizedResult();
            return Task.FromResult(0);
        }

        public class ResultWithChallenge : IHttpActionResult
        {
            private readonly IHttpActionResult next;

            public ResultWithChallenge(IHttpActionResult next)
            {
                this.next = next;
            }

            public async Task<HttpResponseMessage> ExecuteAsync(
                                        CancellationToken cancellationToken)
            {
                var response = await next.ExecuteAsync(cancellationToken);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Headers.WwwAuthenticate.Add(
                           new AuthenticationHeaderValue("Basic", "realm=api"));
                }

                return response;
            }
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // 1. Look for credentials in the request.
            // 2. If there are no credentials, do nothing.
            // 3. If there are credentials but the filter does not recognize the creds in the header, escape
            // 4. If there are credentials that the filter understands, try to validate them.
            // 5. If the credentials are bad, set the error result.      
            // 6. If the credentials are valid, set principal.            
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            if ((authorization == null) || (authorization.Scheme != "Basic"))
            {
                context.ErrorResult = new AuthenticationFailureResult("Failed Authentication", request);
                return;
            }
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                return;
            }
            string clientId, clientApiKey;

            if (!_securityCredentialService.OutputExtractedCredentials(authorization.Parameter, out clientId, out clientApiKey))
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
            }

            if (_securityCredentialService.IsClientCredentialsValid(clientId, clientApiKey)) 
            {
                
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, clientId),
                    new Claim(ClaimTypes.UserData, clientApiKey)                    
                };
                var id = new ClaimsIdentity(claims, "Basic");
                var principal = new ClaimsPrincipal(new[] { id });
                
                context.Principal = principal;
                // The request message contains valid credential                
                
            }
            else
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid Client Id or API key", request);
            }

        }

    }

    public interface ISecurityCredentialService
    {
        bool OutputExtractedCredentials(string authorizationParameter, out string clientId, out string clientApiKey);
        bool IsClientCredentialsValid(string clientId, string clientApiKey);
    }
}