using Microsoft.AspNetCore.Mvc;
using ShopApplication.Models;
using System.Collections.Generic;


namespace ShopApplication.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            //Viewbag
            //Model  --> Veri Gönderme Yöntemleri - Dimaik Şekilde
            //ViewData

            var product = new Product();
            product.Name = "Iphone 12";
            product.Price = 8000;
            product.Description = "Pahalı Telefon";
            
            return View(product);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult List()
        {
            var products = new List<Product>()
            {
                new Product{Name="Iphone X",Price=9000,Description="Yeni Nesil IPhone",IsApproved=true},
                new Product{Name="Iphone XR",Price=6000,Description="Yeni Nesil Geliştirilmiş IPhone",IsApproved=true},
                new Product{Name="Iphone XS",Price=9000,Description="Yeni Nesil Küçük IPhone",IsApproved=true},
                new Product{Name="Iphone 11",Price=10000,Description="Yeni Nesil Güçlendirilmiş IPhone",IsApproved=true}
            };
            
            

            var productViewModel = new ProductViewModel() {                
                Products = products
            };
            return View(productViewModel);
        }
        public IActionResult Details(int id)
        {
            var product = new Product();
            product.Name = "İPhone XR";
            product.Price = 6500;
            product.Description = "Güzel Telefon";
            return View(product);
        }
    }
}
