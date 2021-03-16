using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        
        
        public List<ProductCategory> ProductCategories { get; set; }

    }
}
