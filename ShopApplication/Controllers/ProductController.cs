using Microsoft.AspNetCore.Mvc;
using ShopApplication.Data;
using ShopApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System;

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
        public IActionResult List(int? id,string q,double? min_price,double? max_price)
        {           

            var products = ProductRepository.Products;
            if (id!=null)
            {
                products = products.Where(p => p.CategoryId == id).ToList();
            }
            if (! string.IsNullOrEmpty(q))
            {
                products = products.Where(i => i.Name.ToLower().Contains(q.ToLower()) || i.Description.Contains(q)).ToList();
            }
            var productViewModel = new ProductViewModel() {
                Products = products
            };
            return View(productViewModel);
        }
        public IActionResult Details(int id)
        {            
            return View(ProductRepository.GetProductById(id));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product p)
        {
            ProductRepository.AddProduct(p);
            return RedirectToAction("list");
        }
    }
}

