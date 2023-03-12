using System.Collections.Generic;
using System.Linq;
using Bootapp.Data.Model;
using DbUtil.Entities;
using Bootapp.IServices;
using Bootapp.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Bootapp.Filters;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bootapp.Controllers
{
    [ApiAuthFilter]
    [ServiceFilter(typeof(ConnectionLogFilter))]
    [Route("api/v1/[controller]")]
    public class DataController : Controller
    {
        
        private readonly IService<app_project_table_setting> _tableSettingService;

        private app_project project;
        private readonly ProjectUtil _projectUtil;
        IHttpContextAccessor _httpContextAccessor;

        //private readonly ProcessQuery _processQuery;
        public DataController(IService<app_project_table_setting> tableSettingService, ProjectUtil projectUtil,  IHttpContextAccessor httpContextAccessor)
        {
            _tableSettingService = tableSettingService;
            _projectUtil = projectUtil;
            _httpContextAccessor = httpContextAccessor;
            project = _projectUtil.GetProject(_httpContextAccessor.HttpContext);

        }

        /// <summary>
        /// get data for a project from the specified table
        /// </summary>
        /// <param name="project_code">code of the project</param>
        /// <param name="table">Name of the table</param>
        /// <returns>A datatable</returns>
        [HttpGet("{table}")]
        public JsonResult Get([FromQuery]QueryParameters parameters,string table)
        {
            parameters.table = table;
            if (project.active != 1) {
                return Json(new { message = "This project is currently disabled." });
            }

            ProcessQuery _processQuery = new(project);

            //check if read is enabled for this table
            var table_setting = _tableSettingService.GetAll().FirstOrDefault(e => e.table_name == parameters.table && e.project_id == project.id);
            if (table_setting != null && !table_setting.read)
            {
                return Json(new { message="Read is not enabled for this object."});
            }

            Response.Headers.Add("data_query", JsonConvert.SerializeObject(parameters));

            return Json( _processQuery.Select(parameters));
        }

      
        // POST api/values
        [HttpPost("{table}")]
        public JsonResult Post([FromBody] dynamic value,string table)
        {

            ProcessQuery _processQuery = new(project);
            //check if create is enabled for this table
            var table_setting = _tableSettingService.GetAll().FirstOrDefault(e => e.table_name == table && e.project_id == project.id);
            if (table_setting != null && !table_setting.create)
            {
                return Json(new { message = "Create is not enabled for this object." });
            }


            JObject attributesAsJObject = value;
            Dictionary<string, object> values = attributesAsJObject.ToObject<Dictionary<string, object>>();


            PostParameters parameters = new()
            {
                fields = values.Keys.ToArray(),
                values = values.Values.ToArray()
            };

            parameters.table = table;
            return Json(_processQuery.Insert(parameters));
        }

        // PUT api/values/5
        [HttpPut("{table}")]
        public IActionResult Put([FromBody] PutParameters parameters, string table)
        {

            ProcessQuery _processQuery = new(project);
            parameters.table = table;
            //check if create is enabled for this table
            var table_setting = _tableSettingService.GetAll().FirstOrDefault(e => e.table_name == parameters.table && e.project_id == project.id);
            if (table_setting != null && !table_setting.update)
            {
                return Ok(new { message = "Update is not enabled for this object." });
            }

            _processQuery.Update(parameters);

            return Accepted(200);
        }

        // DELETE api/values/5
        [HttpDelete("{table}")]
        public IActionResult Delete([FromBody] DeleteParameters parameters,string table)
        {

            ProcessQuery _processQuery = new(project);
            parameters.table = table;
            //check if create is enabled for this table
            var table_setting = _tableSettingService.GetAll().FirstOrDefault(e => e.table_name == parameters.table && e.project_id == project.id);
            if (table_setting != null && !table_setting.update)
            {
                return Ok(new { message = "Delete is not enabled for this object." });
            }

            _processQuery.Delete(parameters);

            return Accepted(200);
        }
    }
}
