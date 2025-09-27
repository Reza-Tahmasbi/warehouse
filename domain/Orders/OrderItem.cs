using domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public int OrderId { get; set; }              // FK to Order
        public int ProductId { get; set; }            // FK to Product
        public int Quantity { get; set; }             // Quantity ordered
        public int? ReservedQuantity { get; set; }    // Quantity reserved from stock
        public decimal UnitPrice { get; set; }        // Price at time of order
        public decimal TotalPrice { get; set; }       // Quantity * UnitPrice
        public string Notes { get; set; }             // Item-specific notes

        // Navigation
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
