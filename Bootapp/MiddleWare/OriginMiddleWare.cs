using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Bootapp.MiddleWare
{
    public class OriginMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public OriginMiddleWare(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;

            _logger = logFactory.CreateLogger("OriginMiddleWare");
        }

        public async Task Invoke(HttpContext httpContext)
        {

            var request = httpContext.Request;

            if (request.Headers.ContainsKey("Referer"))
            {
                string host = request.Headers["Host"].ToString();
                string referer = request.Headers["Referer"].ToString();
            }



          //  _logger.LogInformation("MyMiddleware executing..");

            await _next(httpContext);
        }
    }



    public static class OriginMiddleWareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OriginMiddleWare>();
        }
    }

}
