using Microsoft.AspNetCore.Mvc;
using ShopApplication.Data;
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

            var productViewModel = new ProductViewModel() {                
                Products = ProductRepository.Products
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
