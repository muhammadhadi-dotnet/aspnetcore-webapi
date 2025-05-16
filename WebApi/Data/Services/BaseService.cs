using Microsoft.EntityFrameworkCore;
using WebApi.Data.CommonInterface;

namespace WebApi.Data.Services
{
    public class BaseService<T> : BaseInterface<T> where T : class
    {

        private readonly MyDbContext _context;
        internal DbSet<T> _dbSet;
        public BaseService(MyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public  void Add(T model)
        {
            _dbSet.Add(model);
        }

        public void Remove(T model)
        {
            _dbSet.Update(model);
        }

        public void Update(T model)
        {
            _dbSet.Remove(model);
        }

    }
}
