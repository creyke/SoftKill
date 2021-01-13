using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SoftKill.Middleware
{
    public class SoftKillMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<SoftKillMiddleware> logger;

        public SoftKillMiddleware(RequestDelegate next, ILogger<SoftKillMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var executingEndpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = executingEndpoint?.Metadata.OfType<KillSoftlyAttribute>().FirstOrDefault();

            if (attribute is object)
            {
                var delay = attribute.GetDelay(DateTime.UtcNow);
                if (delay.HasValue)
                {
                    var delaySeconds = delay.Value;

                    if (delaySeconds < 0)
                    {
                        logger.LogWarning($"Request to {context.Request.Path} denied as method marked for soft kill and condemnation date has expired.");
                        context.Response.StatusCode = (int)HttpStatusCode.Gone;
                    }
                    else
                    {
                        logger.LogWarning($"Request to {context.Request.Path} delaying artificially for {delaySeconds} seconds as method marked for soft kill.");
                        await Task.Delay(delaySeconds * 1000);
                        await next.Invoke(context);
                    }
                }
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
