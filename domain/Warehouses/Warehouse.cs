using domain.Stocks;
using domain.Transfers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Warehouses
{
    public class Warehouse : BaseEntity<int>
    {
        public string Name { get; set; }              // Main Warehouse, Branch A, etc.
        public string Code { get; set; }              // WH001, WH002
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ManagerName { get; set; }       // Warehouse manager
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal Capacity { get; set; }         // Total capacity (cubic meters)
        public string Notes { get; set; }

        // Navigation
        public ICollection<Stock> Stocks { get; set; }
        public ICollection<Transfer> OutgoingTransfers { get; set; }
        public ICollection<Transfer> IncomingTransfers { get; set; }
    }
}
