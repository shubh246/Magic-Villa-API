using Magic_Villa.Models;
using System.Linq.Expressions;

namespace Magic_Villa.Repository
{
    public interface IVillaNoRepository:IRepository<VillaNumber>
    {
        
        Task<VillaNumber> UpdateAsync(VillaNumber Entity);
       
    }
}
