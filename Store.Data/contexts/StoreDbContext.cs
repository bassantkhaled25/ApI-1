using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.contexts
{
    public class StoreDbContext : DbContext

    {                                       
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)                   //if has configurations on entities
        { 
             
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());         //add config لاي حد بيعمل => implement for this interface
            base.OnModelCreating(modelBuilder);
        
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
       

    }
}