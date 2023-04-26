﻿using Magic_Villa.Models;
using Microsoft.EntityFrameworkCore;

namespace Magic_Villa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Shubham",
                    Details = "sm,adasas.as/.as/m.das/,.",
                    ImageUrl = "",
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
                    ImageUrl = "",
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
                    Details = "sm,adasas.as/.as/m.dmmas/,.",
                    ImageUrl = "",
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
                    Details = "sm,adasas.as/.as/m.das/,.gg",
                    ImageUrl = "",
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