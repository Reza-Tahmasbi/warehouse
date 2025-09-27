using domain.Products;
using domain.Transfers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Cardex
{
    public class Cardex : BaseEntity<int>
    {
        public int ProductId { get; set; }              // FK to Product
        public DateTime TransactionDate { get; set; }    // Date of movement
        public string Reference { get; set; }            // Reference number (PO, Invoice, etc.)
        public string Description { get; set; }          // Description of movement
        public CardexType Type { get; set; }             // In, Out, Adjustment, etc.
        public int QuantityIn { get; set; } = 0;         // Quantity received
        public int QuantityOut { get; set; } = 0;        // Quantity issued
        public int Balance { get; set; }                 // Running balance
        public decimal UnitCost { get; set; }            // Cost per unit
        public decimal TotalCost { get; set; }           // Total cost
        public string Location { get; set; }             // Warehouse location
        public string BatchNumber { get; set; }          // Batch/lot number
        public DateTime? ExpiryDate { get; set; }        // For perishable items
        public string SerialNumbers { get; set; }        // Serial numbers
        public string Notes { get; set; }                // Additional notes
        public string CreatedBy { get; set; }            // Who created the entry
        public string ApprovedBy { get; set; }           // Who approved the entry
        
        // Foreign Keys to related entities
        public int? TransferId { get; set; }             // FK to Transfer
        public int? TransferItemId { get; set; }         // FK to TransferItem
        
        // Navigation
        public Product Product { get; set; }
        public Transfer Transfer { get; set; }
        public TransferItem TransferItem { get; set; }
    }

    public enum CardexType
    {
        Purchase = 1,           // From supplier
        Sale = 2,               // To customer
        TransferIn = 3,         // Incoming transfer
        TransferOut = 4,        // Outgoing transfer
        Adjustment = 5,         // Stock adjustment
        Return = 6,             // Return from customer
        Damage = 7,             // Damaged goods
        Loss = 8,               // Lost items
        OpeningBalance = 9,     // Opening balance
        ClosingBalance = 10,    // Closing balance
        InternalTransfer = 11   // Between locations
    }
}
