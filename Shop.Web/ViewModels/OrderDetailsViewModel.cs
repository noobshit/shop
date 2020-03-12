using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Web.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
        [Display(Name ="Total payment")]
        public decimal TotalPayment { get; set; }
        [Display(Name = "First name")]
        public string ContactFirstName { get; set; }
        [Display(Name = "Last name")]
        public string ContactLastName { get; set; }
        [Display(Name ="Phone number")]
        public string ContactPhoneNumber { get; set; }
        [Display(Name = "City")]
        public string ContactCity { get; set; }
        [Display(Name ="Street")]
        public string ContactStreet { get; set; }
        [Display(Name ="Postal code")]
        public string ContactPostalCode { get; set; }
        [Display(Name ="Country")]
        public string ContactCountry { get; set; }

    }
}
