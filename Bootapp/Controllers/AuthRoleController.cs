using System;
using Bootapp.Data.Entities;
using Bootapp.Data.Model;
using Bootapp.Filters;
using Bootapp.IServices;
using Bootapp.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bootapp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiAuthFilter(auth:true)]
    public class AuthRoleController : Controller
    {
        private app_project project;
        private readonly IAuthRoleService _authRoleService;
        private readonly ProjectUtil _projectUtil;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthRoleController(IAuthRoleService authRoleService, ProjectUtil projectUtil, IHttpContextAccessor httpContextAccessor)
        {
            _authRoleService = authRoleService;
            _projectUtil = projectUtil;
            _httpContextAccessor = httpContextAccessor;
            project = _projectUtil.GetProject(_httpContextAccessor.HttpContext);
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _authRoleService.GetAll(project);
            return Ok(roles);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
           
            var role = _authRoleService.GetById(id,project);
            return Ok(role);
        }


        [HttpGet("{user_id}")]
        public IActionResult GetUserRoles(Guid user_id)
        {
           
            return Ok(_authRoleService.GetUserRoles(user_id, project));
        }


        [HttpGet("{user_id}/{role}")]
        public IActionResult UserInRole(Guid user_id,string role)
        {
            
            return Ok(_authRoleService.UserInRole(role, user_id, project));
        }

      
        [HttpPost]
        public IActionResult PostUserToRole(UserRole userRole)
        {
            _authRoleService.AddUserToRole(userRole,project) ;
            return Ok(200);
        }

        [HttpPost]
        public IActionResult PostRole(Role role)
        {
            _authRoleService.Create(role.role_name, project);
            return Ok(200);
        }

        [HttpPost]
        public IActionResult RemoveUserFromRole(UserRole userRole)
        {
            _authRoleService.RemoveUserFromRole(userRole, project);
            return Ok(200);
        }

        [HttpPost]
        public IActionResult UpdateRole(Role role)
        {
            _authRoleService.Update(role, project);
            return Ok(200);
        }

    }
}
