using System;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data;
using Bootapp.Data.Model;
using Bootapp.IServices;

namespace Bootapp.Service
{
    public class FileTypeService : IService<app_file_type>
    {
        private readonly ApplicationDbContext _context;

        public FileTypeService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task Create(app_file_type obj)
        {

            _context.app_file_type.Add(obj);
            //_context.SaveChanges();
           await  _context.SaveChangesAsync();
        }

        public void Delete(Guid guid)
        {

            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            _context.app_file_type.Remove(_context.app_file_type.FirstOrDefault(e => e.id == id));
            _context.SaveChanges();
        }

        public app_file_type Get(Guid guid)
        {
            throw new NotImplementedException();
        }

        public app_file_type Get(long id)
        {
            
            return _context.app_file_type.FirstOrDefault(e => e.id == id);

        }

        public IQueryable<app_file_type> GetAll()
        {
            return _context.app_file_type.AsQueryable();
        }

        public async Task Update(app_file_type obj)
        {
            var old = _context.app_file_type.FirstOrDefault(e => e.id == obj.id);
            _context.Entry(old).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();
        }

    }
}
