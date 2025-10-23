using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EnterpriseSimpleV2.WebAPI.Filter
{
    public abstract class ExceptionFilterBase : ActionContextLoggerProvider, IExceptionFilter
    {
        protected ExceptionFilterBase(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null && !context.ExceptionHandled)
            {
                var response = GetResponse(context);
                if (response.HasValue)
                {
                    context.ExceptionHandled = true;
                    var errorResponseData = new ErrorResponseData(context.Exception);
                    var jsonResult = new JsonResult(errorResponseData)
                    {
                        StatusCode = (int) response.Value.StatusCode
                    };
                    context.Result = jsonResult;
                }
            }
        }

        protected abstract ExceptionResponse? GetResponse(ExceptionContext exceptionContext);

        protected struct ExceptionResponse
        {
            public ExceptionResponse(HttpStatusCode statusCode, Exception exception)
            {
                StatusCode = statusCode;
                Exception = exception;
            }

            public HttpStatusCode StatusCode { get; }

            public Exception Exception { get; }
        }
    }
}