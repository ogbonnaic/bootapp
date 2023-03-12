using System;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data;
using Bootapp.Data.Model;
using Bootapp.IServices;

namespace Bootapp.Service
{
    public class ProjectService : IService<app_project>
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task Create(app_project obj)
        {
            //obj.id = Guid.NewGuid();
            obj.created_at = DateTime.Now;
            obj.active = 1;
            _context.app_project.Add(obj);
            //_context.SaveChanges();
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

        public app_project Get(Guid guid)
        {
            return _context.app_project.FirstOrDefault(e => e.id == guid);
        }

        public app_project Get(long id)
        {
            throw new NotImplementedException();
            
        }

        public IQueryable<app_project> GetAll()
        {
            return _context.app_project.AsQueryable();
        }

        public async Task Update(app_project obj)
        {
            try
            {
                var old = _context.app_project.FirstOrDefault(e => e.id == obj.id);
                obj.created_at = old.created_at;
                obj.updated_at = DateTime.Now;
                _context.Entry(old).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

    }
}
