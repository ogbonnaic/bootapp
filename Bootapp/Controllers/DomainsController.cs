using System;
using System.Threading.Tasks;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bootapp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DomainsController : BaseController
    {
        private readonly IService<app_domain> _domainService;
        public DomainsController(IService<app_domain> domainService)
        {
            _domainService = domainService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(app_domain domain)
        {
            await _domainService.Create(domain);

            return Ok(domain);
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_domainService.Get(id));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_domainService.GetAll());
        }

        [HttpPut]
        public IActionResult Update(app_domain domain)
        {
            _domainService.Update(domain);
            return Ok(200);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _domainService.Delete(id);
            return Ok(200);
        }
    }
}
