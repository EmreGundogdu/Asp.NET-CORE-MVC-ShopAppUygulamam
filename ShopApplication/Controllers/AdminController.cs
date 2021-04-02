﻿using Business.Abstract;
using Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApplication.Extensions;
using ShopApplication.Identity;
using ShopApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductService _productsService;
        private ICategoryService _categoryService;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;

        public AdminController(IProductService productService,ICategoryService categoryService,RoleManager<IdentityRole> roleManager,UserManager<User> userManager)
        {
            _productsService = productService;
            _categoryService = categoryService;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<User>();
            var nonmembers = new List<User>();
            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user,role.Name)?members:nonmembers;
                list.Add(user);
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd??new string[] {})
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user!=null)
                    {
                        var result = await _userManager.AddToRoleAsync(user,model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            return Redirect("/admin/role/"+model.RoleId);
        }
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }
        public IActionResult RoleCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
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
            if (ModelState.IsValid)
            {
                var entity = new Product()
                {
                    Name = model.Name,
                    Url = model.Url,
                    Price = model.Price,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                };
                if (_productsService.Create(entity))
                {
                    TempData.Put("message", new Messages()
                    {
                        Title = "Kayıt Eklendi",
                        Message = "Kayıt Eklendi",
                        AlertType = "success"

                    });
                    return RedirectToAction("ProductList");
                }
                TempData.Put("message", new Messages()
                {
                    Title = "Hata",
                    Message = _productsService.ErrorMessage,
                    AlertType = "danger"

                });
                return View(model);
            }
            return View(model);
            
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryCreate(CategoryModel model)
        {
            if (ModelState.IsValid)
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
            return View(model);
            
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
                IsApproved = entity.IsApproved,
                IsHome = entity.IsHome,
                SelectedCategories = entity.ProductCategories.Select(i => i.Category).ToList()
            };            
            return View(new ProductModel());
        }
        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductModel model,int[] categoryIds,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = _productsService.GetById(model.ProductId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Url = model.Url;
                entity.Description = model.Description;
                entity.IsHome = model.IsHome;
                entity.IsApproved = model.IsApproved;
                if (file!=null)
                {
                    entity.ImageUrl = file.FileName;
                    var extension = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{extension}");
                    entity.ImageUrl = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images",randomName);
                    using (var stream = new FileStream(path,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                
                if (_productsService.Update(entity, categoryIds))
                {
                    TempData.Put("message", new Messages()
                    {
                        Title = "Kayıt Güncellendi",
                        Message = "Kayıt Güncellendi",
                        AlertType = "success"

                    });
                    return RedirectToAction("ProductList");
                }
                TempData.Put("message", new Messages()
                {
                    Title = "Hata",
                    Message = _productsService.ErrorMessage,
                    AlertType = "danger"

                });          
            }
            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
            
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
            if (ModelState.IsValid)
            {
                var entity = _categoryService.GetById(model.CategoryId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Url = model.Url;

                _categoryService.Update(entity);
                TempData.Put("message", new Messages()
                {
                    Title = "Kayıt Güncellendi",
                    Message = $"{entity.Name} İsimli Kategori Güncellendi.",
                    AlertType = "warning"

                });                
                return RedirectToAction("CategoryList");
            }
            return View(model);
            
        }
        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productsService.GetById(productId);
            if (entity!=null)
            {
                _productsService.Delete(entity);
            }
            TempData.Put("message", new Messages()
            {
                Title = "Kayıt Silindi",
                Message = $"{entity.Name} İsimli Ürün Silindi",
                AlertType = "danger"

            });

            return RedirectToAction("ProductList");
        }
        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
            if (entity != null)
            {
                _categoryService.Delete(entity);
            }
            TempData.Put("message", new Messages()
            {
                Title = "Kayıt Silindi",
                Message = $"{entity.Name} İsimli Kategori Silindi",
                AlertType = "danger"

            });
            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public IActionResult DeleteFromCategory(int productıd, int categoryıd)
        {
            _categoryService.DeleteFromCategory(productıd, categoryıd);
            return Redirect("/admin/categories/" + categoryıd);
        }        
    }
}

