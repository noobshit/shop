using System.ComponentModel.DataAnnotations;

namespace Shop.Web.ViewModels
{
    public class OrderSummaryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Total payment")]
        public decimal TotalPayment { get; set; }

        [Display(Name = "Total products")]
        public decimal TotalProducts { get; set; }
    }
}
