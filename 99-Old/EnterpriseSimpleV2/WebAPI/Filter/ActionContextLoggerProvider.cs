using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;

namespace EnterpriseSimpleV2.WebAPI.Filter
{
    public abstract class ActionContextLoggerProvider
    {
        private readonly ILoggerFactory _loggerFactory;

        protected ActionContextLoggerProvider(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        protected ILogger CreateLogger(ActionContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor cas)
            {
                return _loggerFactory.CreateLogger(cas.ControllerTypeInfo.AsType());
            }

            // Should not happen, but if it does we use string identifier.
            return _loggerFactory.CreateLogger("WebApiFilter");
        }
    }
}