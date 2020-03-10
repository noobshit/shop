using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Context;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Services
{
    public class CartManager : ICartManager
    {
        private readonly ShopContext _shopContext;
        private readonly Cart _cart;
        public CartManager(UserManager<ShopUser> userManager, ShopContext shopContext, IHttpContextAccessor httpContextAccessor)
        {
            _shopContext = shopContext;

            var userId = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            var user = _shopContext.Users.Find(userId);
            _cart = user.Cart;
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
        public IList<CartProduct> List()
        {
            return _shopContext.CartProducts
                .Include(cp => cp.Product)
                .Where(cp => cp.CartId == _cart.Id)
                .ToList();
        }
    }
}
