using Magic_Villa.Models;
using System.Linq.Expressions;

namespace Magic_Villa.Repository
{
    public interface IVillaRepository:IRepository<Villa>
    {
        
        Task<Villa> UpdateAsync(Villa Entity);
       
    }
}
