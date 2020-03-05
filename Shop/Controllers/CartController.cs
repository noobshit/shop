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
using Microsoft.AspNetCore.Authorization;
using Shop.ViewModels;

namespace Shop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartManager _cartManager;
        private readonly ICartSummary _cartSummary;

        public CartController(ICartManager cartManager, ICartSummary cartSummary)
        {
            _cartManager = cartManager;
            _cartSummary = cartSummary;
        }
        public IActionResult Index()
        {
            var model = new ShowCartViewModel
            {
                CartProducts = _cartManager.List(),
                TotalPayment = _cartSummary.TotalPayment,
            };
            return View(model);
        }
        
        public IActionResult Add(int id)
        {
            _cartManager.Add(id);
            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _cartManager.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SetCount(ShowCartViewModel model, int id)
        {
            var cp = model.CartProducts[id];
            _cartManager.SetCount(cp.ProductId, cp.Count);
            return RedirectToAction("Index");
        }
    }
}
