using Business.Abstract;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using ShopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productsService;
        public AdminController(IProductService productService)
        {
            _productsService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductList()
        {
            return View(new ProductListViewModel()
            {
                Products = _productsService.GetAll()
            }); 
        }     

        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                Url = model.Url,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
            };
            _productsService.Create(entity);
            return RedirectToAction("ProductList");
        }
    }
}

