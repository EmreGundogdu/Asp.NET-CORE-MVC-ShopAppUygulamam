using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult List()
        {
            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetAll()
            };
            return View(productViewModel);
        }
    }
}
