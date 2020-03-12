using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare(nameof(Password), ErrorMessage ="Password doesn't match.")]
        public string ConfirmPassword { get; set; }

        public List<SelectListItem> Roles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value="User", Text="User" },
            new SelectListItem { Value="Admin", Text="Admin" },
        };

        [Required]
        [Display(Name = "Choose role")]
        public string Role { get; set; }
    }
}
