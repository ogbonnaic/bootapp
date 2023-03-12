using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bootapp.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Bootapp.Helpers;
using Microsoft.Extensions.Options;
using Bootapp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Bootapp.Data.ViewModel;
using AutoMapper;
using Bootapp.Util;
using Microsoft.Extensions.Configuration;
using Bootapp.Filters;

namespace Bootapp.Controllers
{
    [ApiAuthFilter]
    [ServiceFilter(typeof(ConnectionLogFilter))]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _userService;
        private readonly IConfiguration _configuration;
        //private readonly IService<app_project> _projectService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ProjectUtil _projectUtil;
        private app_project project;
        public AuthController(IAuthService userService, IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor, ProjectUtil projectUtil)
        {
            _userService = userService;
            _configuration = configuration;
            //_projectService = projectService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _projectUtil = projectUtil;

            project = _projectUtil.GetProject(_httpContextAccessor.HttpContext);
        }

        /// <summary>
        /// Authenticate a user against a progject
        /// </summary>
        /// <param name="model">Authetication model of the user</param>
        /// <returns>User information with the token</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {

            var user = _userService.Authenticate(model.Username, model.Password,project);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            //var tokenHandler = new JwtSecurityTokenHandler();
            ////var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            //var key = Encoding.UTF8.GetBytes("this is a test credential");
            ////Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.id.ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.id,
                Username = user.username,
                FirstName = user.first_name,
                LastName = user.last_name,
                Token = GenerateToken(user)
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

       
            if (!project.enable_auth)
            {
                return BadRequest(new { message = "Authentication is not enabled for this project." });
            }

            try
            {
                // create user
                var result = _userService.Create(user, model.password,project);
                return Ok(new
                {
                    Id = result.id,
                    Username = result.username,
                    FirstName = result.first_name,
                    LastName = result.last_name,
                    Token = GenerateToken(result)
                });
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
        

            var users = _userService.GetAll(project);
            var model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
          

            var user = _userService.GetById(id,project);
            var model = _mapper.Map<UserModel>(user);
            return Ok(model);
        }

        [HttpPut]
        public IActionResult Update(Guid id, [FromBody] UpdateUserModel model)
        {
         
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.id = id;

            try
            {
                // update user 
                _userService.Update(user,project, model.password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid id,string project_code)
        {

            _userService.Delete(id,project);
            return Ok();
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            var key = Encoding.UTF8.GetBytes("this is a test credential");
            //Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}