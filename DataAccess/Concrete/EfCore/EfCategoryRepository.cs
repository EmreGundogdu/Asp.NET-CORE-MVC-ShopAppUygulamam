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
        public void DeleteFromCategory(int productId, int categoryId)
        {
            using (var context = new ShopContext())
            {
                var cmd = "delete from ProductCategory where ProductId=@p0 and CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd,productId,categoryId);
            }
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            using (var context = new ShopContext())
            {
                return context.Categories.Where(i => i.CategoryId == categoryId).Include(i => i.ProductCategories).ThenInclude(i => i.Product).FirstOrDefault();
            }
        }        
    }
}
