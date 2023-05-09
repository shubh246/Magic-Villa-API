using Magic_Villa.Models;
using System.Linq.Expressions;

namespace Magic_Villa.Repository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T Entity);
        Task RemoveAsync(T Entity);
        Task SaveAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,string? includeProperties=null, int pagesize = 0, int pagenumber = 1);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        
    }
}
