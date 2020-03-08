using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ViewModels
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

        [Required]
        [Display(Name ="Is admin?")]
        public bool IsAdmin { get; set; }
    }
}
