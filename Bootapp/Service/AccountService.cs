using System;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data;
using Bootapp.Data.Model;
using Bootapp.IServices;

namespace Bootapp.Service
{
    public class AccountService : IService<app_account>
    {
        //private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(app_account obj)
        {
            obj.created_at = DateTime.Now;
            _context.app_account.Add(obj);
           await  _context.SaveChangesAsync();
        }

        public void Delete(Guid guid)
        {
            _context.app_account.Remove(_context.app_account.FirstOrDefault(e => e.id == guid));
            _context.SaveChanges();
           
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public app_account Get(Guid guid)
        {
            return _context.app_account.FirstOrDefault(e => e.id == guid);
        }

        public app_account Get(long id)
        {
            throw new NotImplementedException();
            
        }

        public IQueryable<app_account> GetAll()
        {
            return _context.app_account.AsQueryable();
        }

        public async Task Update(app_account obj)
        {
            var old = _context.app_account.FirstOrDefault(e => e.id == obj.id);
            obj.created_at = old.created_at;
            obj.updated_at = DateTime.Now;
            _context.Entry(old).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();
        }

    }
}
