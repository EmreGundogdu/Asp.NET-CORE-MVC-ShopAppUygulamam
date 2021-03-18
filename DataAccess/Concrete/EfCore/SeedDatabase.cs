//using Entity.Entities;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccess.Concrete.EfCore
//{
//    public static class SeedDatabase
//    {
//        public static void Seed()
//        {
//            var context = new ShopContext();
//            if (context.Database.GetPendingMigrations().Count() == 0)
//            {
//                if (context.Categories.Count() == 0)
//                {
//                    context.Categories.AddRange(Categories);
//                }
//                if (context.Products.Count() == 0)
//                {
//                    context.Products.AddRange(Products);
//                }
//            }
//            context.SaveChanges();
//        }
//        private static Category[] Categories = {
//            new Category(){Name="Telefon"},
//            new Category(){Name="Bilgisayar"},
//            new Category(){Name="Elektronik"},
//        };
//        private static Product[] Products = {
//            new Product(){Name="IPhone 12",Price=12000,ImageUrl="5.jpg",Description="iyi telefon",IsApproved=true},
//            new Product(){Name="IPhone 11",Price=11000,ImageUrl="4.jpg",Description="iyi telefon",IsApproved=true},
//            new Product(){Name="IPhone XR",Price=10000,ImageUrl="3.jpg",Description="iyi telefon",IsApproved=true},
//            new Product(){Name="IPhone XS",Price=9000,ImageUrl="2.jpg",Description="iyi telefon",IsApproved=true},
//            new Product(){Name="IPhone X",Price=8000,ImageUrl="1.jpg",Description="iyi telefon",IsApproved=true}
//        };
//    }
//}
