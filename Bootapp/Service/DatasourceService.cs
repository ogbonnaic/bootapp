using System;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data;
using Bootapp.Data.Model;
using Bootapp.IServices;

namespace Bootapp.Service
{
    public class DatasourceService : IService<app_datasource>
    {
        //private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly ApplicationDbContext _context;

        public DatasourceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(app_datasource obj)
        {
            _context.app_datasource.Add(obj);
           await  _context.SaveChangesAsync();
        }

        public void Delete(Guid guid)
        {
          
           
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public app_datasource Get(Guid guid)
        {
            throw new NotImplementedException();
            //return _context.app_project.FirstOrDefault(e => e.id == guid);
        }

        public app_datasource Get(long id)
        {
            return _context.app_datasource.FirstOrDefault(e => e.id == id);

        }

        public IQueryable<app_datasource> GetAll()
        {
            return _context.app_datasource.AsQueryable();
        }

        public Task Update(app_datasource obj)
        {
            throw new NotImplementedException();
        }
    }
}
