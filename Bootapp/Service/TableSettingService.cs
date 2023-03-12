using System;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Microsoft.EntityFrameworkCore;

namespace Bootapp.Service
{
    public class TableSettingService : IService<app_project_table_setting>
    {
        private readonly ApplicationDbContext _context;

        public TableSettingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(app_project_table_setting obj)
        {
            _context.app_project_table_setting.Add(obj);
            await _context.SaveChangesAsync();
        }

        public void Delete(Guid guid)
        {


        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public app_project_table_setting Get(Guid guid)
        {
            throw new NotImplementedException();
            //return _context.app_project.FirstOrDefault(e => e.id == guid);
        }

        public app_project_table_setting Get(long id)
        {
            return _context.app_project_table_setting.FirstOrDefault(e => e.id == id);

        }

        public IQueryable<app_project_table_setting> GetAll()
        {
            return _context.app_project_table_setting.AsQueryable();
        }

        public async Task Update(app_project_table_setting obj)
        {
            var old = _context.app_project_table_setting.FirstOrDefault(e => e.id == obj.id);
            _context.Entry(old).CurrentValues.SetValues(obj);
            //old.create = obj.create;
            //old.delete = obj.delete;
            //old.read = obj.read;
            //old.update = obj.update;
            //_context.Update(obj);
            //_context.app_project_table_setting.Attach(obj);
            //_context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //try
            //{
            //    _context.SaveChanges();
            //}
            //catch(Exception ex)
            //{

            //}
           
        }
    }
}
