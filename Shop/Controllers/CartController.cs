using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Models;
using Shop.Data;
using Microsoft.EntityFrameworkCore;
using Shop.Services;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopContext _shopContext;
        private readonly ICartManager _cartManager;

        public CartController(ShopContext shopContext, ICartManager cartManager)
        {
            _shopContext = shopContext;
            _cartManager = cartManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(_cartManager.List());
        }

        
        public async Task<IActionResult> Add(int id)
        {
            var product = _shopContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                NotFound();
            }

            _cartManager.Add(product);
            return RedirectToAction("index");
        }
    }
}
