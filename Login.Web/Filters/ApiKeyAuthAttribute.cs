using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Web.Filters
{
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out StringValues potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            IConfiguration configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            string apiKey = configuration.GetValue<string>(ApiKeyHeaderName);

            if (apiKey != potentialApiKey.ToString())
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
