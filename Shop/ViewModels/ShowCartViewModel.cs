using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ViewModels
{
    public class ShowCartViewModel
    {
        public IList<CartProduct> CartProducts { get; set; }
        public decimal TotalPayment { get; set; }

    }
}
