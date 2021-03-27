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
        public IActionResult CategoryList()
        {
            return View(new CategorytListViewModel()
            {
                Categories = _categoryService.GetAll()
            });
        }
        [HttpGet]
        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProductCreate(ProductModel model)
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
                Message = $"{entity.Name} İsimli Ürün Eklendi",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryCreate(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name,
                Url = model.Url
            };
            _categoryService.Create(entity);
            var msg = new Messages()
            {
                Message = $"{entity.Name} İsimli Kategori Eklendi",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public IActionResult ProductEdit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var entity = _productsService.GetByIdWithCategories((int)id);
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
                Description = entity.Description,
                SelectedCategories = entity.ProductCategories.Select(i => i.Category).ToList()
            };
            ViewBag.Categories = _categoryService.GetAll();
            return View(new ProductModel());
        }
        [HttpPost]
        public IActionResult ProductEdit(ProductModel model,int[] categoryIds)
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

            _productsService.Update(entity,categoryIds);
            var msg = new Messages()
            {
                Message = $"{entity.Name} İsimli Ürün Güncellendi",
                AlertType = "warning"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _categoryService.GetByIdWithProducts((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                Products = entity.ProductCategories.Select(p => p.Product).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CategoryEdit(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.CategoryId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Url = model.Url;

            _categoryService.Update(entity);

            var msg = new Messages()
            {
                Message = $"{entity.Name} İsimli Kategori güncellendi.",
                AlertType = "warning"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
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
                Message = $"{entity.Name} İsimli Ürün Silindi",
                AlertType = "danger"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }
        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
            if (entity != null)
            {
                _categoryService.Delete(entity);
            }
            var msg = new Messages()
            {
                Message = $"{entity.Name} İsimli Kategori Silindi",
                AlertType = "danger"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public IActionResult DeleteFromCategory(int productId,int categoryId)
        {
            _categoryService.DeleteFromCategory(productId, categoryId);
            return Redirect("/admin/categories/" + categoryId);
        }
    }
}

