using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class CartManager : ICartManager
    {
        private readonly ShopContext _shopContext;
        private readonly Cart _cart;
        public CartManager(UserManager<ShopUser> userManager, ShopContext shopContext, IHttpContextAccessor httpContextAccessor)
        {
            _shopContext = shopContext;

            var userId = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            var userWithContext = _shopContext.Users.Include(u => u.Cart).FirstOrDefault(u => u.Id == userId);
            _cart = userWithContext.Cart;
        }

        public void Add(Product product)
        {
            var cartProduct = _shopContext.CartProducts.FirstOrDefault(cp => cp.CartId == _cart.Id && cp.ProductId == product.Id);
            if (cartProduct != null)
            {
                _shopContext.Attach(cartProduct);
                cartProduct.Count += 1;
                _shopContext.SaveChanges();
            } 
            else
            {
                _shopContext.CartProducts.Add(new CartProduct
                {
                    Cart = _cart,
                    CartId = _cart.Id,
                    Product = product,
                    ProductId = product.Id,
                    Count = 1,
                });
                _shopContext.SaveChanges();
            }
        }
        public void ChangeCount(Product product) => throw new NotImplementedException();
        public void Remove(Product product) => throw new NotImplementedException();
        public IEnumerable<CartProduct> List()
        {
            var cartProducts = _shopContext.CartProducts.Include(cp => cp.Product).Where(cp => cp.CartId == _cart.Id).ToList();
            return cartProducts;
        }
    }
}
