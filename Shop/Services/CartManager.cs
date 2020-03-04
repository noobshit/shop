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
            var userWithContext = _shopContext.Users.Include(u => u.Cart).Single(u => u.Id == userId);
            _cart = userWithContext.Cart;
        }

        private CartProduct FindCartProduct(int productId)
        {
            return _shopContext.CartProducts.Find(_cart.Id, productId);
        }

        public void Add(int productId)
        {
            var cartProduct = FindCartProduct(productId);
            if (cartProduct != null)
            {
                cartProduct.Count += 1;
                _shopContext.SaveChanges();
            } 
            else
            {
                var product = _shopContext.Products.Find(productId);
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
        public void SetCount(int productId, int value)
        {
            var cartProduct = FindCartProduct(productId);
            if (cartProduct == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            cartProduct.Count = value;
            _shopContext.SaveChanges();
        }
        public void Remove(int productId)
        {
            var cartProduct = FindCartProduct(productId);
            _shopContext.Remove(cartProduct);
            _shopContext.SaveChanges();
        }
        public IEnumerable<CartProduct> List()
        {
            return _shopContext.CartProducts
                .Include(cp => cp.Product)
                .Where(cp => cp.CartId == _cart.Id)
                .ToList();
        }
    }
}
