using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Web.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name ="Last name")]
        public string LastName { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
