using System;
using System.Linq;
using System.Text;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Bootapp.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bootapp.Filters
{
    public class ApiAuthFilter : Attribute,IActionFilter
    {
        const string APIKEY = "API_KEY";
        const string APISECRET = "API_SECRET";

        private readonly bool _auth;

        public ApiAuthFilter(bool auth = false)
        {
            _auth = auth;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue(APIKEY, out var api_key);

            if (string.IsNullOrEmpty(api_key))
            {
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("API key must be provided"));
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }

            context.HttpContext.Request.Headers.TryGetValue(APISECRET, out var api_secret);

            if (string.IsNullOrEmpty(api_secret))
            {
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("API secret must be provided"));
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }

            var tokenUtil = context.HttpContext.RequestServices.GetRequiredService<IKeyTokenUtils>();
            if (!tokenUtil.ValidateToken(api_key,api_secret))
            {
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("API secret validation failed"));
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }

            if (_auth)
            {
                var projectService = context.HttpContext.RequestServices.GetRequiredService<IService<app_project>>();

                var project = projectService.GetAll().FirstOrDefault(e => e.api_key == api_key && e.api_secret == api_secret);

                if (!project.enable_auth)
                {
                    context.HttpContext.Response.Body.Write(Encoding.UTF8.GetBytes("Authentication is not enabled for this project"));
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                    return;
                }

            }
        }
    }
}
