using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<ShoppingCartItem> CartItems { get; set; }
        public DbSet<Shipping> Shippings { get; set; }

        //identity
        public DbSet<User> Users { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}