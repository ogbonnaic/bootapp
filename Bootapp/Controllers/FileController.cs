using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data.Model;
using Bootapp.Filters;
using Bootapp.Helpers;
using Bootapp.IServices;
using Bootapp.Util;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bootapp.Controllers
{
    [ApiAuthFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IService<app_file> _fileService;
        private readonly IService<app_file_type> _fileTypeService;
        private app_project project;
        private readonly ProjectUtil _projectUtil;
        private readonly IService<app_short_url> _shortUrlService;
        string file_path = "file_server";
        IHttpContextAccessor _httpContextAccessor;

        public FileController(IService<app_file> fileService, IService<app_file_type> fileTypeService, ProjectUtil projectUtil, IService<app_short_url> shortUrlService, IHttpContextAccessor httpContextAccessor)
        {
            _fileService = fileService;
            _fileTypeService = fileTypeService;
            _projectUtil = projectUtil;
            _shortUrlService = shortUrlService;
            _httpContextAccessor = httpContextAccessor;
            project = _projectUtil.GetProject(_httpContextAccessor.HttpContext);

            // file_path = Path.Combine("file_server", project.code);

        }


        // GET api/file/upload
        /// <summary>
        /// Receive a file, save file the file and create a short url for the file
        /// </summary>
        /// <param name="file">Received IFormFile file</param>
        /// <returns>IAction Result</returns>
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            
            var extension = Path.GetExtension(file.FileName);

            var file_type = _fileTypeService.GetAll().FirstOrDefault(e => e.extension == "." + extension.ToUpper());
            // create the new file name consisting of the current time plus a GUID
            string newFileName = DateTime.Now.Ticks + "_" + Guid.NewGuid().ToString() + file_type.extension.ToLower();

            file_path = Path.Combine(file_path, project.code);
           
            Directory.CreateDirectory(file_path);
            var filePath = Path.Combine(file_path, newFileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                //copy the contents of the received file to the newly created local file 
                await file.CopyToAsync(stream);
            }
            //
            var short_url = ShortUrlHelper.Encode(12);
            var afile = new app_file
            {
                id = Guid.NewGuid(),
                original_file_name = file.FileName,
                file_name = newFileName,
                short_url=short_url,
                project_id=project.id,
                file_type_id=file_type.id,
                file_size=file.Length
            };

            await _fileService.Create(afile);

            var url = new app_short_url
            {
                id=Guid.NewGuid(),
                original_url=string.Format("/file/download/{0}",afile.id),
                short_url=short_url
            };

            await _shortUrlService.Create(url);
            // return the file name for the locally stored file
            return Ok(afile);
        }

        [HttpPost]
        public async Task<IActionResult> BatchUpload(List<IFormFile> files)
        {
            
            var result_files = new List<app_file>();

            file_path = Path.Combine(file_path, project.code);
             Directory.CreateDirectory(file_path);

            foreach (var file in files)
            {
                //get the extension of the file
                var extension = Path.GetExtension(file.FileName);

                var file_type = _fileTypeService.GetAll().FirstOrDefault(e => e.extension == "." + extension.ToUpper());
                // create the new unique filename for the file to reduce duplicates
                string newFileName = DateTime.Now.Ticks + "_" + Guid.NewGuid().ToString() + file_type.extension.ToLower();

               
                var filePath = Path.Combine(file_path, newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    //save the file to the machine
                    await file.CopyToAsync(stream);
                }
                //
                var short_url = ShortUrlHelper.Encode(12);
                var afile = new app_file
                {
                    id = Guid.NewGuid(),
                    original_file_name = file.FileName,
                    file_name = newFileName,
                    short_url = short_url,
                    project_id = project.id,
                    file_type_id = file_type.id,
                    file_size = file.Length
                };

                await _fileService.Create(afile);

                var url = new app_short_url
                {
                    id = Guid.NewGuid(),
                    original_url = string.Format("/file/download/{0}", afile.id),
                    short_url = short_url
                };

                await _shortUrlService.Create(url);

                result_files.Add(afile);
            }

            // return the file name for the locally stored file
            return Ok(result_files);
        }


        // GET api/file/downlaod
        /// <summary>
        /// Return a locally stored file based on id to the requesting client
        /// </summary>
        /// <param name="id">unique identifier for the requested file</param>
        /// <returns>an IAction Result</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Download(Guid id)
        {

            var file = _fileService.Get(id);
            string path = Path.Combine(file_path, file.file_name);

            if (System.IO.File.Exists(path))
            {
                // Get all bytes of the file and return the file with the specified file contents 
                byte[] b = await System.IO.File.ReadAllBytesAsync(path);
                return File(b, "application/octet-stream");
            }

            else
            {
                // return error if file not found
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet]
        public IActionResult GetFiles()
        {
            //project = _projectUtil.GetProject(Request.HttpContext);

            var files = _fileService.GetAll()
                .Where(e => e.project_id == project.id)
                .Include(e=>e.file_type);
            return Ok(files);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            app_project project = _projectUtil.GetProject(Request.HttpContext);
            var file = _fileService.GetAll().FirstOrDefault(e => e.project_id == project.id && e.id == id);
            if (file == null)
            {
                return NotFound();
            }
            return Ok(file);
        }


    }
}