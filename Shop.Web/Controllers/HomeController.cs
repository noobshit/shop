using AutoMapper;
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

        public IActionResult Index(int page = 0, ProductOrderByEnum orderBy = ProductOrderByEnum.Default, string searchPhrase ="")
        {
            var products = _shopContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchPhrase))
            {
                products = products.Where(p => p.Name.Contains(searchPhrase));
            }

            products = orderBy switch
            {
                ProductOrderByEnum.PriceLow => products.OrderBy(p => p.Price),
                ProductOrderByEnum.PriceHigh => products.OrderByDescending(p => p.Price),
                ProductOrderByEnum.NameLow => products.OrderBy(p => p.Name),
                ProductOrderByEnum.NameHigh => products.OrderByDescending(p => p.Name),
                ProductOrderByEnum.Default => products,
                _ => products,
            };

            var productViewModels = _mapper.ProjectTo<ProductViewModel>(products);
            var paginatedProducts = productViewModels.SetItemsPerPage(9).SetCurrentPage(page);
            var model = new HomeViewModel
            {
                Products = paginatedProducts,
                HasNextPage = paginatedProducts.HasNextPage,
                HasPreviousPage = paginatedProducts.HasPreviousPage,
                CurrentPage = paginatedProducts.CurrentPage,
                OrderBy = orderBy,
                SearchPhrase = searchPhrase,
            };
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _mapper.Map<ProductViewModel>(_shopContext.Products.Find(id));
            return View(model);
        }
    }
}
