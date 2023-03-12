using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Bootapp.Service;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bootapp.Filters
{
    public class ConnectionLogFilter: IActionFilter
    {
        IService<app_connection> _connectionService;
        IService<app_project> _projectService;

        private Stopwatch a;
        public ConnectionLogFilter(IService<app_connection> connectionService, IService<app_project> projectService)
        {
            _connectionService = connectionService;
            _projectService = projectService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            a = Stopwatch.StartNew();
            // Do something before the action executes.
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            a.Stop();
            // Do something after the action executes.
            app_connection connection = new()
            {
                connection_id = context.HttpContext.Connection.Id,
                domain = context.HttpContext.Request.Host.ToString(),
                method = context.HttpContext.Request.Method,
                path = context.HttpContext.Request.Path.Value,
                status = context.HttpContext.Response.StatusCode.ToString(),
                scheme = context.HttpContext.Request.Scheme,
                duration= a.Elapsed.TotalMilliseconds
            };

            

            var path_split = connection.path.Split('/');
            var project_code = path_split[path_split.Length - 1];

            var project = _projectService.GetAll().FirstOrDefault(e => e.code.ToLower() == project_code.ToLower());
            if (project != null)
            {
                connection.project_id = project.id;

                var t = Task.Run(() => _connectionService.Create(connection));
                t.Wait();

            }



        }
    }

}
