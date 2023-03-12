using System;
using System.Linq;
using Bootapp.Data;
using Bootapp.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Bootapp.Service
{
    public class AnalyticsService
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public dynamic GetConnectionSummary()
        {
            //group all requests by date
            var result = _context.app_connection.GroupBy(x => new { x.created_at.Date }).Select(e => new
            {
              date=  e.Key.Date,
                count = e.Count()
            }).ToList();
           

            var dates = result.OrderBy(e=>e.date).Select(e => e.date.ToString("dd MMM yyyy")).ToList();
            var values = result.OrderBy(e => e.date).Select(e => e.count).ToList();


            var project_connections = _context.app_connection.GroupBy(e => e.project_id).Select(e => new {project_id= e.Key,requests= e.Count() }).ToList();


            return new { data = values, categories = dates, project_connections };
        }
    }
}
