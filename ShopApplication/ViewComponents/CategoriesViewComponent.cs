using Microsoft.AspNetCore.Mvc;
using ShopApplication.Data;
using ShopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {            
            return View(CategoryRepository.Categories);
        }
    }
}
