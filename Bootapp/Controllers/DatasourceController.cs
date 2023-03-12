using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Bootapp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bootapp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DatasourceController : BaseController
    {
        private readonly IService<app_datasource> _datasourceService;
        public DatasourceController(IService<app_datasource> datasourceService)
        {
            _datasourceService = datasourceService;
        }

        public IActionResult GetActive()
        {
            return Ok(_datasourceService.GetAll().Where(e => e.active == 1).AsNoTracking());
        }

        public IActionResult GetAll()
        {
            return Ok(_datasourceService.GetAll().AsNoTracking());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImage(int id)
        {
            var file_path = Path.Combine( "images", id+".png");
            var memory = new MemoryStream();
            using (var stream = new FileStream(file_path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "image/jpeg", Path.GetDirectoryName(file_path));

        }
    }
}