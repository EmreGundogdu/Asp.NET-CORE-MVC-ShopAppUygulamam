using Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        //[Display(Name = "Product Name")] 
        //[Required(ErrorMessage ="Name zorunlu bir alandır.")]
        //[StringLength(60,MinimumLength =5,ErrorMessage ="Ürün ismi 5-10 karakter aralığında olmalıdır")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Price zorunlu bir alandır.")]
        //[Range(1,100000,ErrorMessage = "Price için 1-100000 arasında bir değer girmelisiniz")]
        public int Price { get; set; }

        //[Required(ErrorMessage = "Description zorunlu bir alandır.")]
        //[StringLength(100,MinimumLength =5,ErrorMessage ="Description 5-100 karakter arasında olmalıdır")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "ImageUrl zorunlu bir alandır.")]
        public string ImageUrl { get; set; }

        public bool IsApproved { get; set; }

        //[Required(ErrorMessage = "Url zorunlu bir alandır.")]
        public string Url { get; set; }
        public bool IsHome { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}
