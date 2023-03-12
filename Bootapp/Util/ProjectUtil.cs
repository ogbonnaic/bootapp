using System;
using System.Linq;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bootapp.Util
{
    public class ProjectUtil
    {
        const string APIKEY = "API_KEY";
        const string APISECRET = "API_SECRET";


        public app_project GetProject(HttpContext context)
        {
            var projectService = context.RequestServices.GetRequiredService<IService<app_project>>();

            //get the API_KEY and API_SECRET of the context
            context.Request.Headers.TryGetValue(APISECRET, out var api_secret);
            context.Request.Headers.TryGetValue(APIKEY, out var api_key);
            //throw an error if the key and/or secret is missing
            return projectService.GetAll().Include(e => e.datasource).FirstOrDefault(e => e.api_key == api_key.ToString() && e.api_secret == api_secret.ToString());
        }
    }
}
