using System.Collections.Generic;

namespace Shop.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public virtual IList<CartProduct> CartProducts { get; set; }
    }
}
