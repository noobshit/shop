using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Services
{
    public interface ICartSummary
    {
        decimal TotalPayment { get; }
        int ProductCount { get; }
    }
}
