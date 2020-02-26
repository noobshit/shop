using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class MockProductRepository : IProductRepository
    {
        private readonly List<Product> _products;
        public MockProductRepository()
        {
            _products = new List<Product>
            {
                new Product { Id = 0, Name = "Paprika", Price = 10.0M },
                new Product { Id = 1, Name = "Carrot", Price = 3.0M },
                new Product { Id = 2, Name = "Banana", Price = 4.5M },
                new Product { Id = 3, Name = "Orange", Price = 4.0M },
            };
        }
        public IEnumerable<Product> GetProducts() 
        {
            return _products;
        }
    }
}
