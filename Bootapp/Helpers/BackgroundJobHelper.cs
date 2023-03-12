using System;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Bootapp.Helpers
{
    public class BackgroundJobHelper
    {
        private readonly IService<app_project_table_setting> _tableSettingService;
        private readonly IService<app_project> _projectService;
        public BackgroundJobHelper(IService<app_project_table_setting> tableSettingService, IService<app_project> projectService)
        {
            _tableSettingService = tableSettingService;
            _projectService = projectService;
        }

        public async Task RunTableJobs()
        {
            try
            {
                //get all the projects
                var projects = _projectService.GetAll().Include(e => e.datasource).ToList();
                TableSettingHelper settingHelper = new TableSettingHelper(_tableSettingService);
                foreach (var project in projects)
                {
                    await settingHelper.GenerateTableSettings(project);
                }
            }
            catch (Exception)
            {

            }
          

        }
    }
}
