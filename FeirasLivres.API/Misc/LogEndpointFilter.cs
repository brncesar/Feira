using Microsoft.AspNetCore.Mvc.Filters;

namespace FeirasLivres.Api.Misc
{
    public class LogEndpointFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var endpointRout   = context.ActionDescriptor.AttributeRouteInfo.Template;
            var endpointMethod = context.ActionDescriptor.DisplayName.Substring(0, context.ActionDescriptor.DisplayName.IndexOf(" ("));
            var endpointParams = $"{string.Join(", ", context.ActionArguments.Select(kv => kv.Key + " = " + kv.Value).ToArray())}";

            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var loggerService = loggerFactory.CreateLogger(context.Controller.GetType());

            loggerService.LogInformation($"Endpoint \"{endpointRout}\" » {endpointMethod}({endpointParams})");

            // execute any code before the action executes
            var result = await next();
            // execute any code after the action executes
        }
    }
}
