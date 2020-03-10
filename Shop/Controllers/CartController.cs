using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Shop.Services;
using Microsoft.AspNetCore.Authorization;
using Shop.ViewModels;
using AutoMapper;

namespace Shop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartManager _cartManager;
        private readonly IMapper _mapper;

        public CartController(ICartManager cartManager, IMapper mapper)
        {
            _cartManager = cartManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var model = _cartManager.List()
                    .Select(_mapper.Map<CartProductViewModel>)
                    .ToList();
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
        public IActionResult SetCount(IList<CartProductViewModel> model, int id)
        {
            if( ModelState.IsValid )
            { 
                var cp = model[id];
                _cartManager.SetCount(cp.ProductId, cp.Count);
            }
            return View("Index", model);
        }
    }
}
