using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data.Model;
using Bootapp.Data.ViewModel;
using Bootapp.IServices;
using Bootapp.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bootapp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AppAccountController : Controller
    {
        private readonly IService<app_account> _accountService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AppAccountController(IService<app_account> accountService, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _accountService = accountService;
            _userManager = userManager;
            _configuration = configuration;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
           return Ok(_accountService.GetAll());
           
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_accountService.Get(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Setup([FromBody] app_account value)
        {
            _accountService.Create(value);
            return Ok();
        }

        /// <summary>
        /// Check if the the connectionstring of the project is setup and correct
        /// </summary>
        /// <returns></returns>
        public IActionResult IsDatabaseSetup()
        {
            var connection_string = _configuration.GetConnectionString("DefaultConnection");
            var conn = new NpgsqlConnection(connection_string);
            var result = true;
            var isOpen = false;

           using (var connection = new NpgsqlConnection(connection_string))
            {
                try
                {
                    connection.Open();
                    isOpen = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                finally
                {
                    if (isOpen)
                        connection.Close();
                }
            }
            return Ok(result);
        }

        /// <summary>
        /// Check if the application is already setup
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult IsSetup()
        {
            var setup = false;

            try
            {
                if (_accountService.GetAll().Any())
                {
                    setup = true;
                }
            }
            catch (Exception)
            {
                setup = false;
            }
           
            return Ok(setup);

        }


        /// <summary>
        /// Setup a new bootapp account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>200 if the </returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountSetupModel model)
        {
            //check if the database exists
            //var connection_string = string.Format("Server={0};Port={1};Database=postgres;User Id={2};Password={3}",model.config.server, model.config.port, model.config.username, model.config.password);
            //string cmd_text = string.Format("SELECT 1 FROM pg_database WHERE datname='{0}'",model.config.database);

            //var cmd = new NpgsqlCommand(cmd_text);
            //var dbOps = DbOperations.ExecuteScaler(cmd, connection_string);
            //bool dbExists = dbOps != null;

            //if (!dbExists)
            //{
            //    throw new Exception("Database does not exist or current user cannot access the database");
            //}


            var user = new IdentityUser
            {
                UserName = model.reg.Email,
                Email = model.reg.Email,
                PhoneNumber = model.reg.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.reg.Password);

            if (result.Succeeded)
            {
                model.account.active = 1;
                model.account.created_by = user.Id;
                await _accountService.Create(model.account);
            }


            return Ok(200);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}
