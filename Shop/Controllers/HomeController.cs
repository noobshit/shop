using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public string Index()
        {
            return "Hello world!";
        }
    }
}
