using Microsoft.AspNetCore.Mvc;
using Shop.Web.ViewModels;

namespace Shop.Web.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ProductViewModel model)
        {
            return View(model);
        }
    }
}
