using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Bootapp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<IdentityUser> userManager,IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }


        public async Task<IActionResult> GetUsers()
        {
            var userViewModels = new List<UserViewModel>();
            var users = _userManager.Users.ToList();

            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel(user, roles.ToList()));

            }

            return Ok(userViewModels);
        }

        public IActionResult GetRoles()
        {
            return Ok(_roleManager.Roles);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.id);
            user.Email = model.email;
            user.UserName = model.user_name;
            user.PhoneNumber = model.phone_number;
            await _userManager.UpdateAsync(user);
            return Ok(200);
        }
    }
}