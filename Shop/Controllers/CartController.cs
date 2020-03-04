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

namespace Shop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartManager _cartManager;

        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }
        public IActionResult Index()
        {
            return View(_cartManager.List());
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
        public IActionResult SetCount(IList<CartProduct> model, int id)
        {
            _cartManager.SetCount(model[id].ProductId, model[id].Count);
            return RedirectToAction("Index");
        }
    }
}
