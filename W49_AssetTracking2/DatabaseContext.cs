﻿using Microsoft.EntityFrameworkCore;

namespace W49_AssetTracking2
{
    internal class DatabaseContext : DbContext
    {
        string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=AssetTracking;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";

        // Level 1 & 2 Table
        public DbSet<Hardware> Hardwares { get; set; }
        
        // Level 3 Tables
        public DbSet<Office> Offices { get; set; }
        public DbSet<Asset> Assets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We tell the app to use the connectionstring.
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            /*  
             *  --------------------------------------------
             *  ---             Level 1-2                ---
             *  --------------------------------------------
            */
            ModelBuilder.Entity<Hardware>().HasData( new Hardware { 
                Id = 1, 
                Type = "Computer", 
                Brand = "Lenovo", 
                Model = "Legion", 
                Price = 1600, 
                DateOfPurchase = new DateTime(2021, 10, 04) 
            });
            ModelBuilder.Entity<Hardware>().HasData(new Hardware
            {
                Id = 2,
                Type = "Computer",
                Brand = "Acer",
                Model = "Aspire XC",
                Price = 1550,
                DateOfPurchase = new DateTime(2019, 03, 20)
            });
            ModelBuilder.Entity<Hardware>().HasData(new Hardware
            {
                Id = 3,
                Type = "Laptop",
                Brand = "Apple",
                Model = "MacBook",
                Price = 650,
                DateOfPurchase = new DateTime(2021, 05, 03)
            });
            ModelBuilder.Entity<Hardware>().HasData(new Hardware
            {
                Id = 4,
                Type = "Laptop",
                Brand = "Samsung",
                Model = "Galaxy",
                Price = 550,
                DateOfPurchase = new DateTime(2020, 09, 18)
            });
            ModelBuilder.Entity<Hardware>().HasData(new Hardware
            {
                Id = 5,
                Type = "Phone",
                Brand = "Samsung",
                Model = "S21",
                Price = 200,
                DateOfPurchase = new DateTime(2022, 07, 07)
            });
            ModelBuilder.Entity<Hardware>().HasData(new Hardware
            {
                Id = 6,
                Type = "Phone",
                Brand = "Huawei",
                Model = "P30 Pro",
                Price = 150,
                DateOfPurchase = new DateTime(2019, 12, 27)
            });


            /*  
             *  --------------------------------------------
             *  ---              Level 3                 ---
             *  --------------------------------------------
            */
        }
    }
}
