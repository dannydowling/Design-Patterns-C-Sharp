﻿using Microsoft.EntityFrameworkCore;
using ASP_Examples.Models;

namespace ASP_Examples
{

    public class ShoppingContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder
               .UseLazyLoadingProxies()
               .UseSqlite("Data Source=orders.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Ignore(c => c.ProfilePicture);

            base.OnModelCreating(modelBuilder);
        }
    }
}
