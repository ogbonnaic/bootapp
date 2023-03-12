using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Bootapp.Util;
using DbUtil.Entities;


namespace Bootapp.Helpers
{
    public class TableSettingHelper
    {
        private readonly IService<app_project_table_setting> _tableSettingService;

        public TableSettingHelper(IService<app_project_table_setting> tableSettingService)
        {
            _tableSettingService = tableSettingService;
        }

        public async Task GenerateTableSettings(app_project project)
        {
            var tables = new List<TableInfo>();
            var connection_string = new DbConnection().GetConnectionString(project);


            ProcessQuery _processQuery = new(project);

            tables = _processQuery.GetTables();


            if (tables.Any())
            {
                var tableSettings = _tableSettingService.GetAll().ToList();
                foreach (var table in tables)
                {
                   
                    //check if the table already exists in the settings
                    if (!tableSettings.Where(e=>e.table_name==table.table_name && e.project_id==project.id && e.table_schema == table.table_schema).Any())
                    {
                        var table_settings = new app_project_table_setting();
                        table_settings.project_id = project.id;
                        table_settings.table_name = table.table_name;
                        table_settings.table_schema = table.table_schema;

                       await _tableSettingService.Create(table_settings);
                    }
                }
            }
        }
    }
}
