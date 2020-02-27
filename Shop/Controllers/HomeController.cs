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
        private readonly ShopContext _context;

        public HomeController(ShopContext shopContext)
        {
            _context = shopContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}
