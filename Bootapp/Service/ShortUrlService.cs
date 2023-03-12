using System;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data;
using Bootapp.Data.Model;
using Bootapp.IServices;

namespace Bootapp.Service
{
    public class ShortUrlService : IService<app_short_url>
    {
        private readonly ApplicationDbContext _context;

        public ShortUrlService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task Create(app_short_url obj)
        {
            obj.id = Guid.NewGuid();
            _context.app_short_url.Add(obj);
           await  _context.SaveChangesAsync();
        }

        public void Delete(Guid guid)
        {
            _context.app_project.Remove(_context.app_project.FirstOrDefault(e => e.id == guid));
            _context.SaveChanges();
           
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public app_short_url Get(Guid guid)
        {
            return _context.app_short_url.FirstOrDefault(e => e.id == guid);
        }

        public app_short_url Get(long id)
        {
            throw new NotImplementedException();
            
        }

        public IQueryable<app_short_url> GetAll()
        {
            return _context.app_short_url.AsQueryable();
        }

        public async Task Update(app_short_url obj)
        {
            var old = _context.app_short_url.FirstOrDefault(e => e.id == obj.id);

            _context.Entry(old).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();
        }

    }
}
