using Magic_Villa.Data;
using Magic_Villa.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Magic_Villa.Repository
{
    public class VillaNoRepository : Repository<VillaNumber>,IVillaNoRepository
    {
        private readonly ApplicationDbContext db;
        public VillaNoRepository(ApplicationDbContext _db): base(_db)
        {
            db = _db;


        }
       

        public async Task<VillaNumber> UpdateAsync(VillaNumber Entity)
        {
            Entity.UpdatedDate = DateTime.Now;
            db.VillaNumbers.Update(Entity);
            await db.SaveChangesAsync();
            return Entity;
        }
    }
}
