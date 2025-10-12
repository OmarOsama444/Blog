using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Presentation.Fitlers
{
    public class LogActionFilter(ILogger<LogActionFilter> logger) : IAsyncActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger = logger;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var arguments = context.ActionArguments;

            _logger.LogInformation("Starting action {ActionName} with args {@Args}", actionName, arguments);

            var executedContext = await next(); // execute the action

            if (executedContext.Exception == null)
            {
                _logger.LogInformation("Action {ActionName} completed successfully", actionName);
            }
            else
            {
                _logger.LogError(executedContext.Exception, "Action {ActionName} failed", actionName);
            }
        }
    }
}