using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Customers
{
    public class Customer : BaseEntity<int>
    {
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string TaxId { get; set; }           // Tax identification number
        public string PaymentTerms { get; set; }     // e.g., "Net 30"
        public decimal CreditLimit { get; set; }     // Credit limit for this customer
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        // Navigation
        // Customer relationships are handled through Cardex entries
    }
}
