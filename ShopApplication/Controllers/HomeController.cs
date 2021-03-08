using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace ShopApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = new List<Product>()
            {
                new Product{Name="Iphone X",Price=9000,Description="Yeni Nesil IPhone",IsApproved=true},
                new Product{Name="Iphone XR",Price=6000,Description="Yeni Nesil Geliştirilmiş IPhone",IsApproved=true},
                new Product{Name="Iphone XS",Price=9000,Description="Yeni Nesil Küçük IPhone",IsApproved=true},
                new Product{Name="Iphone 11",Price=10000,Description="Yeni Nesil Güçlendirilmiş IPhone",IsApproved=true}
            };
            


            var productViewModel = new ProductViewModel()
            {
                Products = products
            };
            return View(productViewModel);
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact(){
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
    }
}
