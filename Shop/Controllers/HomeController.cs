using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using Shop.ViewModels;
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

        public IActionResult Index(int page = 0)
        {
            var model = new PaginatedEnumeration<Product>(_shopContext.Products, page);
            return View(model);
        }
    }
}
