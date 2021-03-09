using ShopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.Data
{
    public static class ProductRepository
    {
        private static List<Product> _products = null;
        static ProductRepository()
        {
            _products = new List<Product>
            {
                new Product{ProductId=1, Name="Iphone X",Price=9000,Description="Yeni Nesil IPhone",IsApproved=true, ImageUrl="1.jpg"},
                new Product{ProductId=2,Name="Iphone XR",Price=6000,Description="Yeni Nesil Geliştirilmiş IPhone",IsApproved=true, ImageUrl="3.jpg"},
                new Product{ProductId=3,Name="Iphone XS",Price=9000,Description="Yeni Nesil Küçük IPhone",IsApproved=true, ImageUrl="2.jpg"},
                new Product{ProductId=4,Name="Iphone 11",Price=10000,Description="Yeni Nesil Güçlendirilmiş IPhone",IsApproved=true, ImageUrl="4.jpg"}
            };
        }
        public static List<Product> Products
        {
            get
            {
                return _products;
            }
        }
        public static void AddProduct(Product product)
        {
            _products.Add(product);
        }
        public static Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p=>p.ProductId == id);
        }
    }
}
