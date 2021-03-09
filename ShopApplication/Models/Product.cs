namespace ShopApplication.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }        
        public double Price { get; set; }        
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
    }
}