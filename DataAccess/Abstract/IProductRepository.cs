﻿using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
        Product GetProductDetails(string url);
        List<Product> GetProductsByCategory(string name);
        List<Product> GetPopularProducts();
        List<Product> GetTop5Products();
    }
}
