using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["action"].ToString() == "list")
                ViewBag.SelectedCategory = RouteData?.Values["id"];
            return View(_categoryService.GetAll());            
        }
    }
}
