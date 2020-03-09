﻿using System.Collections.Generic;

namespace Shop.Models
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
