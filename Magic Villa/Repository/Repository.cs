using Magic_Villa.Data;
using Magic_Villa.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Magic_Villa.Repository
{
    public class Repository<T>:IRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext _db)
        {
            db = _db;
            _db.VillaNumbers.Include(u=>u.Villa).ToList();


            this.dbSet = db.Set<T>();


        }
        public async Task CreateAsync(T Entity)
        {
            await dbSet.AddAsync(Entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool tracked, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            if (includeProperties != null)
            {
                foreach(var inclupro in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inclupro);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter, string? includeProperties = null, int pagesize = 0, int pagenumber = 1)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (pagesize > 0)
            {
                if (pagesize > 100)
                {
                    pagesize = 100;
                }

                query = query.Skip(pagesize * (pagenumber - 1)).Take(pagesize);
            }
            if (includeProperties != null)
            {
                foreach (var inclupro in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inclupro);
                }
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T Entity)
        {
            dbSet.Remove(Entity);
            await SaveAsync();
        }


        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        
    }
}

