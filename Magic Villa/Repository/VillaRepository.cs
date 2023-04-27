using Magic_Villa.Data;
using Magic_Villa.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Magic_Villa.Repository
{
    public class VillaRepository : Repository<Villa>,IVillaRepository
    {
        private readonly ApplicationDbContext db;
        public VillaRepository(ApplicationDbContext _db): base(_db)
        {
            db = _db;


        }
       

        public async Task<Villa> UpdateAsync(Villa Entity)
        {
            Entity.UpdatedDate = DateTime.Now;
            db.Villas.Update(Entity);
            await db.SaveChangesAsync();
            return Entity;
        }
    }
}
