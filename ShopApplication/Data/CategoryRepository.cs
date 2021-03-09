using ShopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.Data
{
    public static class CategoryRepository
    {
        private static List<Category> _categories = null;
        static CategoryRepository()
        {
            _categories = new List<Category>
            {
                new Category{CategoryId=1, Name="Telefonlar",Description="Telefon Kategorisi"},
                new Category{CategoryId=2,Name="Bilgisayarlar",Description="Bilgisayar Kategorisi"},
                new Category{CategoryId=3,Name="Elektronik Ürünler",Description="Elektronik Ürünler Kategorisi"}
            };
        }
        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }
        public static void AddCategory(Category category) 
        {
            _categories.Add(category);    
        }
        public static Category GetCategoryById(int id)
        {
            return _categories.FirstOrDefault(c => c.CategoryId == id);
        }
    }
}
