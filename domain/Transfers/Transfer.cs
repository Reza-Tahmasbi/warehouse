using domain.Customers;
using domain.Products;
using domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Transfers
{
    public class Transfer : BaseEntity<int>
    {
        public string TransferNumber { get; set; }      // حواله number
        public TransferType Type { get; set; }          // Incoming, Outgoing, Internal
        public TransferStatus Status { get; set; }      // Pending, InTransit, Delivered, Cancelled
        public int? SupplierId { get; set; }            // For incoming transfers
        public int? CustomerId { get; set; }            // For outgoing transfers
        public string FromLocation { get; set; }        // Source location
        public string ToLocation { get; set; }          // Destination location
        public DateTime TransferDate { get; set; }      // When transfer was initiated
        public DateTime? ExpectedDeliveryDate { get; set; } // Expected delivery
        public DateTime? ActualDeliveryDate { get; set; }   // Actual delivery
        public string DriverName { get; set; }          // Driver/courier name
        public string VehicleNumber { get; set; }       // Vehicle/container number
        public string Notes { get; set; }               // Additional notes
        public string CreatedBy { get; set; }           // Who created the transfer
        public string ApprovedBy { get; set; }          // Who approved the transfer

        // Navigation
        public Supplier Supplier { get; set; }
        public Customer Customer { get; set; }
        public ICollection<TransferItem> TransferItems { get; set; }
    }

    public enum TransferType
    {
        Incoming = 1,    // From supplier to warehouse
        Outgoing = 2,    // From warehouse to customer
        Internal = 3     // Between warehouse locations
    }

    public enum TransferStatus
    {
        Pending = 1,     
        InTransit = 2,   
        Delivered = 3,   
        Cancelled = 4    
    }
}
