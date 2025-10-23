using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EnterpriseSimpleV2.WebAPI.Filter
{
    public sealed class MethodCallLogFilter : IActionFilter
    {
        private readonly ILogger<MethodCallLogFilter> _logger;

        private Stopwatch _stopWatch;

        public MethodCallLogFilter(ILogger<MethodCallLogFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.Assert(_stopWatch == null, @"OnActionExecuted must be called");
            _stopWatch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopWatch.Stop();
            _logger.LogDebug($"{context.ActionDescriptor.DisplayName} - {_stopWatch.ElapsedMilliseconds}ms");
        }
    }
}