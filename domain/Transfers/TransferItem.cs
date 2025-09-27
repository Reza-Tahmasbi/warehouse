using domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Transfers
{
    public class TransferItem : BaseEntity<int>
    {
        public int TransferId { get; set; }         // FK to Transfer
        public int ProductId { get; set; }          // FK to Product
        public int Quantity { get; set; }           // Quantity in transfer
        public int? ReceivedQuantity { get; set; }  // Actually received quantity
        public decimal UnitCost { get; set; }       // Cost per unit
        public decimal TotalCost { get; set; }      // Total cost for this item
        public string BatchNumber { get; set; }     // Batch/lot number
        public DateTime? ExpiryDate { get; set; }   // For perishable items
        public string SerialNumbers { get; set; }   // Serial numbers (comma-separated)
        public string Notes { get; set; }           // Item-specific notes
        public bool IsReceived { get; set; } = false; // Whether item was received

        // Navigation
        public Transfer Transfer { get; set; }
        public Product Product { get; set; }
    }
}
