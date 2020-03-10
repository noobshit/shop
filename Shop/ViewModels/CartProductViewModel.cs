using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.ViewModels
{
    public class CartProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImagePath { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Count { get; set; }
    }
}
