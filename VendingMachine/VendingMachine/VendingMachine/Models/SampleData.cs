using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VendingMachine.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<VendingEntities>
    {
        protected override void Seed(VendingEntities context)
        {

            new List<ProductModel>
            {
                new ProductModel { 
                    ProductName = "A1", 
                    Stock = 10,
                    Price = 8.99M
                },
                new ProductModel
                {
                    ProductName = "A2",
                    Stock = 10,
                    Price = 1.5M
                },
                new ProductModel
                {
                    ProductName = "A3",
                    Stock =10,
                    Price = 0.59M
                },
                new ProductModel
                {
                    ProductName = "A4",
                    Stock = 10,
                    Price = 1.73M
                },
                new ProductModel
                {
                    ProductName = "B7",
                    Stock = 0,
                    Price = 1.75M
                },
                new ProductModel
                {
                    ProductName ="B6",
                    Stock = 10,
                    Price = 1.75M
                },
                new ProductModel
                {
                    ProductName = "B8",
                    Stock = 10,
                    Price = 1.72M
                }
                
            }.ForEach(p => context.Products.Add(p));
            //base.Seed(context);
        }

        //public ChangeModel fivePenceCount { get; set; }
    }
}