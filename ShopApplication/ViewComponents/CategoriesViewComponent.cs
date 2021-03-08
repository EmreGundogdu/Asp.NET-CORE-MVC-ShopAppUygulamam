using Microsoft.AspNetCore.Mvc;
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
            var categories = new List<Category>()
            {
                new Category{Name="Telefonlar",Description="Telefon Kategorisi"},
                new Category{Name="Bilgisayarlar",Description="Bilgisayar Kategorisi"},
                new Category{Name="Elektronik Ürünler",Description="Elektronik Ürünler Kategorisi"}
            };
            return View(categories);
        }
    }
}
