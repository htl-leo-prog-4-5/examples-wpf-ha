using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EnterpriseSimpleV2.WebAPI.Filter
{
    public sealed class UnhandledExceptionFilter : ExceptionFilterBase
    {
        public UnhandledExceptionFilter(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        protected override ExceptionResponse? GetResponse(ExceptionContext exceptionContext)
        {
            var logger = CreateLogger(exceptionContext);
            var exception = exceptionContext.Exception;
            logger.LogError(exception.Message, exception);

            return new ExceptionResponse(HttpStatusCode.InternalServerError, exception);
        }
    }
}