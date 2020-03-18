﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Context;
using Shop.Web.ViewModels;
using System.Linq;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopContext _shopContext;
        private readonly IMapper _mapper;

        public HomeController(ShopContext shopContext, IMapper mapper)
        {
            _shopContext = shopContext;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 0, string orderBy = "", string searchPhrase ="")
        {
            var products = _shopContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchPhrase))
            {
                products = products.Where(p => p.Name.Contains(searchPhrase));
            }

            products = orderBy switch
            {
                "price_low" => products.OrderBy(p => p.Price),
                "price_high" => products.OrderByDescending(p => p.Price),
                "name_low" => products.OrderBy(p => p.Name),
                "name_high" => products.OrderByDescending(p => p.Name),
                "default" => products,
                _ => products,
            };

            var productViewModels = _mapper.ProjectTo<ProductViewModel>(products);
            var model = new PaginatedEnumeration<ProductViewModel>(productViewModels, page);
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _mapper.Map<ProductViewModel>(_shopContext.Products.Find(id));
            return View(model);
        }
    }
}
