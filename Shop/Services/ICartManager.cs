using Shop.Models;
using System.Collections.Generic;

namespace Shop.Services
{
    public interface ICartManager
    {
        void Add(Product product);
        void Remove(Product product);
        void ChangeCount(Product product);
        IEnumerable<CartProduct> List();
    }
}
