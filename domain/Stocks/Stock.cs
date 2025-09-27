using domain.Products;
using domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Stocks
{
    public class Stock : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }           // FK to Warehouse
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; } = 0;  // Reserved for orders
        public int AvailableQuantity => Quantity - ReservedQuantity;  // Calculated property
        public string Location { get; set; }            // Specific location within warehouse (A-01-01)
        public string BatchNumber { get; set; }         // Batch/lot number
        public DateTime? ExpiryDate { get; set; }       // For perishable items
        public DateTime LastUpdated { get; set; }       // When stock was last modified
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
