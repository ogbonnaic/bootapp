using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bootapp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TableSettingController : BaseController
    {
        private readonly IService<app_project_table_setting> _tableSettingService;
        public TableSettingController(IService<app_project_table_setting> tableSettingService)
        {
            _tableSettingService = tableSettingService;
        }

        [HttpGet("{project_id}")]
        public IActionResult GetProjectTableSettings(Guid project_id)
        {
            return Ok(_tableSettingService.GetAll().Where(e => e.project_id == project_id));
        }

        public async Task<IActionResult> Update(app_project_table_setting setting)
        {
            await _tableSettingService.Update(setting);
            return Ok(200);
        }
    }
}