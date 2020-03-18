using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Shop.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public ProductOrderByEnum OrderBy { get; set; }
        public string SearchPhrase { get; set; }
    }
}
