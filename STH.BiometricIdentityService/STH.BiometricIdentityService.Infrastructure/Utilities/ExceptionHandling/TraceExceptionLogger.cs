//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Net;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.ExceptionHandling;

//namespace Mclaren.PMP.RestAPI.Utilities.ExceptionHandling
//{
//    public class ExceptionLogger : IExceptionLogger
//    {
//        public virtual Task LogAsync(ExceptionLoggerContext context,
//            CancellationToken cancellationToken)
//        {
//            if (!ShouldLog(context))
//            {
//                return Task.FromResult(0);
//            }

//            return LogAsyncCore(context, cancellationToken);
//        }

//        public virtual Task LogAsyncCore(ExceptionLoggerContext context,
//            CancellationToken cancellationToken)
//        {
//            LogCore(context);
//            return Task.FromResult(0);
//        }

//        public virtual void LogCore(ExceptionLoggerContext context)
//        {
//        }

//        public virtual bool ShouldLog(ExceptionLoggerContext context)
//        {
//            IDictionary exceptionData = context.ExceptionContext.Exception.Data;

//            if (!exceptionData.Contains("MS_LoggedBy"))
//            {
//                exceptionData.Add("MS_LoggedBy", new List<object>());
//            }

//            ICollection<object> loggedBy = ((ICollection<object>) exceptionData["LoggedByKey"]);

//            if (!loggedBy.Contains(this))
//            {
//                loggedBy.Add(this);
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//    }

//    public class ExceptionHandler : IExceptionHandler
//    {
//        public virtual Task HandleAsync(ExceptionHandlerContext context,
//            CancellationToken cancellationToken)
//        {
//            if (!ShouldHandle(context))
//            {
//                return Task.FromResult(0);
//            }

//            return HandleAsyncCore(context, cancellationToken);
//        }

//        public virtual Task HandleAsyncCore(ExceptionHandlerContext context,
//            CancellationToken cancellationToken)
//        {
//            HandleCore(context);
//            return Task.FromResult(0);
//        }

//        public virtual void HandleCore(ExceptionHandlerContext context)
//        {
//        }

//        public virtual bool ShouldHandle(ExceptionHandlerContext context)
//        {
//            return context.ExceptionContext.CatchBlock.IsTopLevel;
//        }
//    }

//    public class TraceExceptionLogger : ExceptionLogger
//    {
//        public override void LogCore(ExceptionLoggerContext context)
//        {
//            Trace.TraceError(context.ExceptionContext.Exception.ToString());
//        }

        
//    }

//    private class OopsExceptionHandler : ExceptionHandler
//    {
//        public override void HandleCore(ExceptionHandlerContext context)
//        {
//            context.Result = new TextPlainErrorResult
//            {
//                Request = context.ExceptionContext.Request,
//                Content = "Oops! Sorry! Something went wrong." +
//                          "Please contact support@contoso.com so we can try to fix it."
//            };
//        }

//        private class TextPlainErrorResult : IHttpActionResult
//        {
//            public HttpRequestMessage Request { get; set; }

//            public string Content { get; set; }

//            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//            {
//                HttpResponseMessage response =
//                    new HttpResponseMessage(HttpStatusCode.InternalServerError);
//                response.Content = new StringContent(Content);
//                response.RequestMessage = Request;
//                return Task.FromResult(response);
//            }
//        }
//    }
//}