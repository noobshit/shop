using System.Collections.Generic;

namespace Shop.Web.ViewModels
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public IEnumerable<int> PreviousPages { get; set; }
        public IEnumerable<int> FollowingPages { get; set; }
        public int LastPage { get; set; }
        public Dictionary<string, string> AllRouteData { get; set; }
    }
}
