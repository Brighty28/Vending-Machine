using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace VendingMachine.Models
{
    public class VendingEntities : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ChangeModel> ChangeAmounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}