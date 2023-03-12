using System;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data;
using Bootapp.Data.Model;
using Bootapp.IServices;

namespace Bootapp.Service
{
    public class DomainService : IService<app_domain>
    {
        private readonly ApplicationDbContext _context;

        public DomainService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task Create(app_domain obj)
        {
            obj.id = Guid.NewGuid();
          
            _context.app_domain.Add(obj);
            //_context.SaveChanges();
           await  _context.SaveChangesAsync();
        }

        public void Delete(Guid guid)
        {
            _context.app_domain.Remove(_context.app_domain.FirstOrDefault(e => e.id == guid));
            _context.SaveChanges();
           
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public app_domain Get(Guid guid)
        {
            return _context.app_domain.FirstOrDefault(e => e.id == guid);
        }

        public app_domain Get(long id)
        {
            throw new NotImplementedException();
            
        }

        public IQueryable<app_domain> GetAll()
        {
            return _context.app_domain.AsQueryable();
        }

        public async Task Update(app_domain obj)
        {
            try
            {
                var old = _context.app_domain.FirstOrDefault(e => e.id == obj.id);
          
                _context.Entry(old).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
           
        }

    }
}
