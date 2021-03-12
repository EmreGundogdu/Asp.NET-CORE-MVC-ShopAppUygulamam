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
                new Product{ProductId=1, Name="Iphone X",Price=9000,Description="Yeni Nesil IPhone",IsApproved=true, ImageUrl="1.jpg",CategoryId=1},
                new Product{ProductId=2,Name="Iphone XR",Price=6000,Description="Yeni Nesil Geliştirilmiş IPhone",IsApproved=true, ImageUrl="3.jpg",CategoryId=1},
                new Product{ProductId=3,Name="Iphone XS",Price=9000,Description="Yeni Nesil Küçük IPhone",IsApproved=true, ImageUrl="2.jpg",CategoryId=1},
                new Product{ProductId=4,Name="Iphone 11",Price=10000,Description="Yeni Nesil Güçlendirilmiş IPhone",IsApproved=true, ImageUrl="4.jpg",CategoryId=1},
                new Product{ProductId=5, Name="Dell",Price=9000,Description="Uygun Fiyatlı Laptop",IsApproved=true, ImageUrl="6.jpg",CategoryId=2},
                new Product{ProductId=6,Name="Apple Macbook",Price=6000,Description="Çok Kullanışlı Laptop",IsApproved=true, ImageUrl="7.jpg",CategoryId=2},
                new Product{ProductId=7,Name="Asus",Price=9000,Description="Tasarım Ve Performans Laptopu",IsApproved=true, ImageUrl="8.jpg",CategoryId=2},
                new Product{ProductId=8,Name="MSI",Price=10000,Description="En Güçlü Donanıma Sahip Laptop",IsApproved=true, ImageUrl="9.jpg",CategoryId=2}
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
        public static void EditProduct(Product product)
        {
            foreach (var p in _products)
            {
                if (p.ProductId == product.ProductId)
                {
                    p.Name = product.Name;
                    p.Price = product.Price;
                    p.Description = product.Description;
                    p.ImageUrl = product.ImageUrl;
                    p.IsApproved = product.IsApproved;
                    p.CategoryId = product.CategoryId;
                }
            }
        }
    }
}
