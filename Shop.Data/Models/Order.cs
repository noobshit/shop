using System.Collections.Generic;
using System.Linq;

namespace Shop.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
        public decimal TotalPayment { get => OrderItems.Sum(oi => oi.Price * oi.Quantity); }
        public virtual ShopUser ShopUser { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
