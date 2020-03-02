using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopContext _shopContext;

        public HomeController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public IActionResult Index()
        {
            var model = _shopContext.Products;
            return View(model);
        }
    }
}
