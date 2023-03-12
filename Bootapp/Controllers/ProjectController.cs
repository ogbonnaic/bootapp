using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data.Model;
using Bootapp.Data.ViewModel;
using Bootapp.IServices;
using Bootapp.Util;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bootapp.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Bootapp.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly IService<app_project> _projectService;
        private readonly IService<app_project_table_setting> _tableSettingService;
        private readonly IMapper _mapper;
        private readonly IService<app_datasource> _datasourceService;
        private readonly IKeyTokenUtils _keyTokenUtils;
        private readonly UserManager<IdentityUser> _userManager;

        public ProjectController(IService<app_project> projectService, IMapper mapper, IService<app_project_table_setting> tableSettingService, IService<app_datasource> datasourceService, IKeyTokenUtils keyTokenUtils, UserManager<IdentityUser> userManager)
        {
            _projectService = projectService;
            _mapper = mapper;
            _tableSettingService = tableSettingService;
            _datasourceService = datasourceService;
            _keyTokenUtils = keyTokenUtils;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {

            return Ok(_projectService.GetAll()
                .Include(e => e.datasource)
                
                .AsNoTracking());

        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_projectService.GetAll()
                .Include(e => e.datasource)
                
                .AsNoTracking().FirstOrDefault(e => e.id == id));
        }
        
       
        [HttpPost]
        public async Task<IActionResult> Post(Project project)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var aproject = _mapper.Map<app_project>(project);
            var datasource = _datasourceService.Get(project.datasourceid);
            
            aproject.id = Guid.NewGuid();
            aproject.created_by = user.Id;
            aproject.datasource = datasource;
            aproject.api_key = _keyTokenUtils.GenerateKey(16);
            aproject.api_secret = _keyTokenUtils.GenerateToken(aproject);
            await _projectService.Create(aproject);

            //get all the tables
            var tables=new ProcessQuery(aproject).GetTables();
            foreach(var table in tables)
            {
                var table_setting = new app_project_table_setting
                {
                    project_id = aproject.id,
                    table_name = table.table_name,
                    table_schema = table.table_schema
                };

                await  _tableSettingService.Create(table_setting);
            }

            if (aproject.enable_auth)
            {
                new AuthUtil().CreateDb(aproject);
            }

            return Ok(_projectService.GetAll()
                .Include(e => e.datasource)
                .AsNoTracking().FirstOrDefault(e => e.id == aproject.id));
        }



        [HttpPut]
        public async Task<IActionResult> Update(app_project aproject)
        {

            //var aproject = _mapper.Map<app_project>(project);
            var datasource = _datasourceService.Get(aproject.datasource.id);
            
            var user = await _userManager.GetUserAsync(HttpContext.User);

            aproject.created_by = user.Id;
            aproject.datasource = datasource;
            await _projectService.Update(aproject);

            //get all the tables
            //var tables = new ProcessQuery(aproject).GetTables();
            //foreach (var table in tables)
            //{
            //    var table_setting = new app_project_table_setting
            //    {
            //        project_id = aproject.id,
            //        table_name = table.table_name,
            //        table_schema = table.table_schema
            //    };


            //    await _tableSettingService.Update(table_setting);
            //}

            if (aproject.enable_auth)
            {
                new AuthUtil().CreateDb(aproject);
            }

            return Ok(_projectService.GetAll()
                .Include(e => e.datasource)
                .AsNoTracking().FirstOrDefault(e => e.id == aproject.id));
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectTables(Guid id)
        {
            var project = _projectService.GetAll()
                .Include(e => e.datasource)
                .AsNoTracking().FirstOrDefault(e => e.id == id);

            return Ok(new ProcessQuery(project).GetTables());
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectTablesWithSettings(Guid id)
        {
            var project = _projectService.GetAll()
                .Include(e => e.datasource)
                .AsNoTracking().FirstOrDefault(e => e.id == id);

            var tables = new ProcessQuery(project).GetTables();
            var tables_settings = _tableSettingService.GetAll().Where(e => e.project_id == id).ToList();
            var tableMoels = new List<TableViewModel>();

            foreach(var table in tables)
            {
                var setting = tables_settings.FirstOrDefault(e => e.table_name == table.table_name && e.table_schema == table.table_schema);

                var table_model = new TableViewModel();
                table_model.table_schema = table.table_schema;
                table_model.table_name = table.table_name;
                table_model.rows = table.rows;
                if (setting != null)
                {
                    table_model.id = setting.id;
                    table_model.create = setting.create;
                    table_model.delete = setting.delete;
                    table_model.update = setting.update;
                    table_model.read = setting.read;
                }


                tableMoels.Add(table_model);
            }


            return Ok(tableMoels);
        }



        [HttpGet("{id}/{schema}/{table}")]
        public IActionResult GetTableColumns(Guid id,string schema,string table)
        {
            var project = _projectService.GetAll()
                .Include(e => e.datasource)
                .AsNoTracking().FirstOrDefault(e => e.id == id);

            return Ok(new ProcessQuery(project).GetColumns(schema,table));
        }

    }


}