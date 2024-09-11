using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductListPreview.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductListPreview.DataAcsess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }



        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Clothes", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Accessories", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Phones", DisplayOrder = 3 }
            );
            modelBuilder.Entity<Product>().HasData(
              new Product
              {
                  Id = 1,
                  Title = "Title",
                  Description = "Des ",
                  Price = 90,
                  CategoryID = 1,
                  ImageaUrl = ""
              },
               new Product
               {
                   Id = 2,
                   Title = "Title",
                   Description = "Des ",
                   Price = 30,
                   CategoryID = 1,
                   ImageaUrl = ""
               });


        }
    

    }
}
