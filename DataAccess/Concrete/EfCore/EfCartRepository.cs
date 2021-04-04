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
    public class EfCartRepository : EfGenericRepository<Cart, ShopContext>, ICartRepository
    {
        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlRaw(cmd, cartId, productId);
            }
        }

        public Cart GetByUserId(string userId)
        {
            using (var context = new ShopContext())
            {
                return context.Carts.Include(i => i.CartItems).ThenInclude(i => i.Product).FirstOrDefault(i => i.UserId == userId);
            }
        }
        public override void Update(Cart entity)
        {
            using (var context = new ShopContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
