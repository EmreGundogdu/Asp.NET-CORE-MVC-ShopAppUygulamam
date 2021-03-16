using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entity.Entities;

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

            //var products = ProductRepository.Products;
            //if (id!=null)
            //{
            //    products = products.Where(p => p.CategoryId == id).ToList();
            //}
            //if (! string.IsNullOrEmpty(q))
            //{
            //    products = products.Where(i => i.Name.ToLower().Contains(q.ToLower()) || i.Description.Contains(q)).ToList();
            //}
            //var productViewModel = new ProductViewModel() {
            //    Products = products
            //};
            //return View(productViewModel);
            return View();
        }
        public IActionResult Details(int id)
        {            
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Categories = new SelectList(CategoryRepository.Categories,"CategoryId","Name");
            return View();
        }
        /*[HttpPost]
        public IActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.AddProduct(p);
                return RedirectToAction("list");
            }
            ViewBag.Categories = new SelectList(CategoryRepository.Categories, "CategoryId", "Name");
            return View(new Product());
        
        }*/
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //ViewBag.Categories = new SelectList(CategoryRepository.Categories,"CategoryId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Product p)
        {
            //ProductRepository.EditProduct(p);
            return RedirectToAction("list");
        }
        public IActionResult Delete(Product p)
        {
            //ProductRepository.EditProduct(p);
            return RedirectToAction("list");
        }
    } 
}

