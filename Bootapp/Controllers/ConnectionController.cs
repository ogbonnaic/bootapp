using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Bootapp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bootapp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConnectionController : BaseController
    {
        private readonly IService<app_connection> _connectionService;
        private readonly AnalyticsService _analyticsService;
        public ConnectionController(IService<app_connection> connectionService, AnalyticsService analyticsService)
        {
            _connectionService = connectionService;
            _analyticsService = analyticsService;
        }

        public IActionResult GetSummary()
        {
            var connections = _connectionService.GetAll();
            //_analyticsService.GetConnectionSummary();
            
            return Ok(_analyticsService.GetConnectionSummary());
        }
    }
}