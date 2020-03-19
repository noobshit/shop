using Microsoft.AspNetCore.Mvc;
using Shop.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Web.ViewComponents
{
    public class PaginationViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(IPaginated paginated, int pageLimit)
        {
            var allRouteData = new Dictionary<string, string>(
                Request.Query.Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value))
                );
            var model = new PaginationViewModel
            {
                CurrentPage = paginated.CurrentPage,
                HasNextPage = paginated.HasNextPage,
                HasPreviousPage = paginated.HasPreviousPage,
                FollowingPages = paginated.GetFollowingPages(pageLimit),
                PreviousPages = paginated.GetPreviousPages(pageLimit),
                LastPage = paginated.LastPage,
                AllRouteData = allRouteData,
            };
            return View(model);
        }
    }
}
