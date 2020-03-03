using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public IList<CartProduct> CartProducts { get; set; }
    }
}
