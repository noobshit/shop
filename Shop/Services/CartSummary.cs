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
    public class CartSummary : ICartSummary
    {
        public CartSummary(UserManager<ShopUser> userManager, ShopContext shopContext, IHttpContextAccessor httpContextAccessor)
        {
            var userId = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            var user = shopContext.Users.Find(userId);
            if (user == null)
            {
                TotalPayment = 0.0M;
                ProductCount = 0;
            }
            else
            {
                var productsInCart = shopContext.CartProducts.Include(cp => cp.Product).Where(cp => cp.CartId == user.Cart.Id);
                TotalPayment = productsInCart.Sum(cp => cp.Product.Price * cp.Count);
                ProductCount = productsInCart.Count();
            }
        }

        public decimal TotalPayment { get; private set; }

        public int ProductCount { get; private set; }
    }
}
