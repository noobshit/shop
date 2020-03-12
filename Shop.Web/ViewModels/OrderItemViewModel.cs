using System.ComponentModel.DataAnnotations;

namespace Shop.Web.ViewModels
{
    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        [Display(Name ="Name")]
        public string ProductName { get; set; }
        public string ProductImagePath { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
