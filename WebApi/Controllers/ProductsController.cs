using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] Products =
        {
            "samsung s8","samsung s10","iphone 12","iphone 12max"
        };
        public string[] GetProducts()
        {
            return Products;
        }
        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return Products[id];
        }
    }
}
