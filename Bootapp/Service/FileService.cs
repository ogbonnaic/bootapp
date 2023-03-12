using System;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data;
using Bootapp.Data.Model;
using Bootapp.IServices;

namespace Bootapp.Service
{
    public class FileService : IService<app_file>
    {
        private readonly ApplicationDbContext _context;

        public FileService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task Create(app_file obj)
        {
            obj.id = Guid.NewGuid();
            obj.created_at = DateTime.Now;
            _context.app_file.Add(obj);
            //_context.SaveChanges();
           await  _context.SaveChangesAsync();
        }

        public void Delete(Guid guid)
        {
            _context.app_file.Remove(_context.app_file.FirstOrDefault(e => e.id == guid));
            _context.SaveChanges();
           
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public app_file Get(Guid guid)
        {
            return _context.app_file.FirstOrDefault(e => e.id == guid);
        }

        public app_file Get(long id)
        {
            throw new NotImplementedException();
            
        }

        public IQueryable<app_file> GetAll()
        {
            return _context.app_file.AsQueryable();
        }

        public async Task Update(app_file obj)
        {
            var old = _context.app_file.FirstOrDefault(e => e.id == obj.id);
            obj.created_at = old.created_at;
            obj.updated_at = DateTime.Now;
            _context.Entry(old).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();
        }

    }
}
