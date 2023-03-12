using System;
using System.Threading;
using System.Threading.Tasks;
using Bootapp.Data.Model;
using Bootapp.Helpers;
using Bootapp.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bootapp.MiddleWare
{
    public class AppBackgroundService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<AppBackgroundService> _logger;
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;

        public AppBackgroundService(ILogger<AppBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            _timer = new Timer(RunBgEvent, null, TimeSpan.FromMinutes(2),
                TimeSpan.FromHours(1));
       
            return Task.CompletedTask;
        }

        private void RunBgEvent(object state)
        {
            _logger.LogInformation("Updating table settings");
           


            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IService<app_project_table_setting> _tableSettingService =
                    scope.ServiceProvider.GetRequiredService<IService<app_project_table_setting>>();

                IService<app_project> _projectService =
                 scope.ServiceProvider.GetRequiredService<IService<app_project>>();

                var t = Task.Run(
                    () => new BackgroundJobHelper(_tableSettingService, _projectService).RunTableJobs()
                    );
                t.Wait();

               // new BackgroundJobHelper(_tableSettingService, _projectService).RunTableJobs();
            }
            _logger.LogInformation("Completed updating table settings");

        }

        //private void DoWork(object state)
        //{
        //    var count = Interlocked.Increment(ref executionCount);

        //    _logger.LogInformation(
        //        "Timed Hosted Service is working. Count: {Count}", count);
        //}

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
