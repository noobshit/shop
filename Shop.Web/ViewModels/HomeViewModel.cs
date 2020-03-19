namespace Shop.Web.ViewModels
{
    public class HomeViewModel
    {
        public PaginatedEnumerable<ProductViewModel> Products { get; set; }
        public ProductOrderByEnum OrderBy { get; set; }
        public string SearchPhrase { get; set; }
    }
}
