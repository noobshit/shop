using Shop.Data.Models;
using System.Collections.Generic;

namespace Shop.Services
{
    public interface ICartManager
    {
        void Add(int productId);
        void Remove(int productId);
        void SetCount(int productId, int value);
        IList<CartProduct> List();
    }
}
