using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace STH.BiometricIdentityService.Infrastructure.Utilities.Authentication.Handlers
{
    public class RequireHttpsHandler : DelegatingHandler
    {
        private readonly int _httpsPort;

        /// <summary>Initializes a new instance of the <see cref="RequireHttpsHandler" /> class.</summary>
        public RequireHttpsHandler()
            : this(int.Parse(System.Configuration.ConfigurationManager.AppSettings["SSLPort"]))
        {
        }

        /// <summary>Initializes a new instance of the <see cref="RequireHttpsHandler" /> class.</summary>
        /// <param name="httpsPort">The HTTPS port.</param>
        public RequireHttpsHandler(int httpsPort)
        {
            _httpsPort = httpsPort;
        }

        /// <summary>Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.</summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.Scheme == Uri.UriSchemeHttps)
                return base.SendAsync(request, cancellationToken);

            var response = CreateResponse(request);
            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(response);
            return tcs.Task;
        }

        private HttpResponseMessage CreateResponse(HttpRequestMessage request)
        {
            HttpResponseMessage response;
            var uri = new UriBuilder(request.RequestUri);
            uri.Scheme = Uri.UriSchemeHttps;
            uri.Port = _httpsPort;
            var body = string.Format("HTTPS is required<br/>The resource can be found at <a href=\"{0}\">{0}</a>.", uri.Uri.AbsoluteUri);
            if (request.Method.Equals(HttpMethod.Get) || request.Method.Equals(HttpMethod.Head))
            {
                response = request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = uri.Uri;
                if (request.Method.Equals(HttpMethod.Get))
                    response.Content = new StringContent(body, Encoding.UTF8, "text/html");
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.NotFound);
                response.Content = new StringContent(body, Encoding.UTF8, "text/html");
            }

            return response;
        }
    }
}