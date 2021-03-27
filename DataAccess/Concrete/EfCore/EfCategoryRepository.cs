using DataAccess.Abstract;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCategoryRepository : EfGenericRepository<Category, ShopContext>, ICategoryRepository
    {
        public Category GetByIdWithProducts(int categoryId)
        {
            using (var context = new ShopContext())
            {
                return context.Categories.Where(i => i.CategoryId == categoryId).Include(i => i.ProductCategories).ThenInclude(i => i.Product).FirstOrDefault();
            }
        }        
    }
}
