using System;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Microsoft.EntityFrameworkCore;

namespace Bootapp.Service
{
    public class ConnectionService : IService<app_connection>
    {
        private readonly ApplicationDbContext _context;

        public ConnectionService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task Create(app_connection obj)
        {
            obj.id = Guid.NewGuid();
            obj.created_at = DateTime.Now;
            _context.app_connection.Add(obj);
            //_context.SaveChanges();
           await  _context.SaveChangesAsync();
        }

        public void Delete(Guid guid)
        {
            _context.app_connection.Remove(_context.app_connection.FirstOrDefault(e => e.id == guid));
            _context.SaveChanges();
           
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public app_connection Get(Guid guid)
        {
            return _context.app_connection.FirstOrDefault(e => e.id == guid);
        }

        public app_connection Get(long id)
        {
            throw new NotImplementedException();
            
        }

        public IQueryable<app_connection> GetAll()
        {
            return _context.app_connection.AsQueryable();
        }

        public async Task Update(app_connection obj)
        {
            var old = _context.app_connection.FirstOrDefault(e => e.id == obj.id);
            obj.created_at = old.created_at;
        
            _context.Entry(old).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();
        }

    }
}
