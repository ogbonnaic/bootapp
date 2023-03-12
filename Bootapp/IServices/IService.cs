using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bootapp.IServices
{
    public interface IService<T>
    {
        public IQueryable<T> GetAll();
        public T Get(Guid guid);
        public T Get(long id);
        public Task Create(T obj);
        public Task Update(T obj);
        public void Delete(Guid guid);
        public void Delete(long id);
    }

}
