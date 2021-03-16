using Business.Abstract;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace ShopApplication.Controllers
{
    public class HomeController : Controller
    {
        IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }    
        public IActionResult Index()
        {
            var productViewModel = new ProductViewModel()
            {
                Products = _productService.GetAll()
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
    }
}
