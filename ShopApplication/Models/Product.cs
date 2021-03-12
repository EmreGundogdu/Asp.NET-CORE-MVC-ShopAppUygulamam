using System.ComponentModel.DataAnnotations;

namespace ShopApplication.Models
{
    public class Product
    {
        [Required]
        public int? CategoryId { get; set; }
        public int ProductId { get; set; }
        [Required]
        [StringLength(60,MinimumLength =10,ErrorMessage ="Name must be between 10 to 60")]
        public string Name { get; set; }   
        [Required(ErrorMessage ="You must enter price")]
        [Range(1,100)]
        public double? Price { get; set; }        
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
    }
}