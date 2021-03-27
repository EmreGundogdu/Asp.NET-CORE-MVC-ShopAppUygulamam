using Business.Abstract;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private ICategoryService _categoryService;

        public AdminController(IProductService productService,ICategoryService categoryService)
        {
            _productsService = productService;
            _categoryService = categoryService;
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
            var msg = new Messages()
            {
                Message = $"{entity.Name} isimli ürün eklendi",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var entity = _productsService.GetById((int)id);
            if (entity==null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Url = entity.Url,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description
            };
            return View(new ProductModel());
        }
        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            var entity = _productsService.GetById(model.ProductId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.Url = model.Url;
            entity.ImageUrl = model.ImageUrl;
            entity.Description = model.Description;

            _productsService.Update(entity);
            var msg = new Messages()
            {
                Message = $"{entity.Name} isimli ürün güncellendi",
                AlertType = "warning"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }
        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productsService.GetById(productId);
            if (entity!=null)
            {
                _productsService.Delete(entity);
            }
            var msg = new Messages()
            {
                Message = $"{entity.Name} isimli ürün silindi",
                AlertType = "delete"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }
    }
}

