using domain.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Orders
{
    public class Order : BaseEntity<int>
    {
        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; set; }       // ORD-2024-001
        
        [Required]
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }       // Pending, Confirmed, Shipped, Delivered, Cancelled
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }   // When customer needs it
        public DateTime? ShippedDate { get; set; }
        public decimal SubTotal { get; set; }         // Before tax
        public decimal TaxAmount { get; set; }        // Tax amount
        public decimal TotalAmount { get; set; }      // Final total
        public string PaymentTerms { get; set; }      // Net 30, COD, etc.
        public string ShippingAddress { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }

        // Navigation
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public enum OrderStatus
    {
        Pending = 1,        
        Confirmed = 2,      
        Processing = 3,     
        Shipped = 4,        
        Delivered = 5,      
        Cancelled = 6 
    }
}
