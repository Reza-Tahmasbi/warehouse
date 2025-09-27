using domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Suppliers
{
    public class Supplier : BaseEntity<int>
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
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<Product> Products { get; set; }
    }
}
