using Microsoft.AspNetCore.Mvc;
using ShopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return View();
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
