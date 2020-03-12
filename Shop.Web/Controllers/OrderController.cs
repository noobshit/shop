using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Context;
using Shop.Data.Models;
using Shop.Web.Services;
using Shop.Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<ShopUser> _userManager;
        private readonly ICartManager _cartManager;
        private readonly ShopContext _shopContext;
        private readonly IMapper _mapper;

        public OrderController(UserManager<ShopUser> userManager, ICartManager cartManager, ShopContext shopContext, IMapper mapper)
        {
            _userManager = userManager;
            _cartManager = cartManager;
            _shopContext = shopContext;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var orders = _shopContext.Orders.Where(o => o.ShopUser.Id == user.Id).ToList();

            var model = orders.Select(o => new OrderSummaryViewModel
            {
                Id = o.Id,
                TotalPayment = o.TotalPayment,
                TotalProducts = o.OrderItems.Count(),
            });
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var order = _shopContext.Orders.SingleOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            if (order.ShopUser.Id != user.Id)
            {
                return StatusCode(403);
            }
            var model = _mapper.Map<OrderDetailsViewModel>(order);

            return View(model);
        }
        public async Task<IActionResult> MakeOrder()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if( user.Contact == null )
            {
                return RedirectToAction("editDetails", "user");
            }
            var cartProducts = _cartManager.List();
            var orderItems = cartProducts.Select(cp => new OrderItem
            {
                Product = cp.Product,
                Price = cp.Product.Price,
                Quantity = cp.Count,
            }).ToList();

            var contact = user.Contact.Clone();
            var order = new Order
            {
                OrderItems = orderItems,
                ShopUser = user,
                Contact = contact,
            };

            _shopContext.Orders.Add(order);
            _shopContext.SaveChanges();

            foreach( var product in cartProducts )
            {
                _cartManager.Remove(product.ProductId);
            }

            return RedirectToAction("Index");
        }
    }
}
