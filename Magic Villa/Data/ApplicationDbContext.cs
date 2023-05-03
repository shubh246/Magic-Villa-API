using Magic_Villa.Models;
using Microsoft.EntityFrameworkCore;

namespace Magic_Villa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Shubham",
                    Details = "sm,adasas.as/.as/m.das/,.",
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa3.jpg",
                    Occupancy = 4,
                    Rate = 300,
                    Sqft = 200,
                    Amenity = "",
                    CreatedDate=DateTime.Now,
                },
                new Villa
                {
                    Id = 2,
                    Name = "Shubham",
                    Details = "sm,adasas.as/.as/m.das/,.",
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa1.jpg",
                    Occupancy = 4,
                    Rate = 200,
                    Sqft = 900,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa
                {
                    Id = 3,
                    Name = "Shubham",
                    Details = "ssssss",
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa4.jpg",
                    Occupancy = 4,
                    Rate = 600,
                    Sqft = 700,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa
                {
                    Id = 4,
                    Name = "Shubham3",
                    Details = "sssssddf",
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa5.jpg",
                    Occupancy = 4,
                    Rate = 200,
                    Sqft = 100,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                }







                );

        }
    }
}
