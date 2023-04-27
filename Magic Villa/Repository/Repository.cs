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
            this.dbSet = db.Set<T>();


        }
        public async Task CreateAsync(T Entity)
        {
            await dbSet.AddAsync(Entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool tracked)
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
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
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

