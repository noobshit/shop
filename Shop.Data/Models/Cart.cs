using System.Collections.Generic;

namespace Shop.Data.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public virtual IList<CartProduct> CartProducts { get; set; }
    }
}
