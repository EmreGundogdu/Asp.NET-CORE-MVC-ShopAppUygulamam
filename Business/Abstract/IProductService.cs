﻿using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService:IValidator<Product>
    {
        Product GetById(int id);
        Product GetProductDetails(string url);
        List<Product> GetProductsByCategory(string name,int page,int pageSize);
        List<Product> GetAll();
        List<Product> GetHomePageProducts();
        List<Product> GetSearchResult(string searchString);
        bool Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        int GetCountByCategory(string category);
        Product GetByIdWithCategories(int id);
        bool Update(Product entity, int[] categoryIds);
    }
}
