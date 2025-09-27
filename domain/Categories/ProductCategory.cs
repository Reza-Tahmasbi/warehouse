using domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Categories
{
    public class ProductCategory : BaseEntity<int>
    {
        [Required]
        public int ProductId { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        public bool IsPrimary { get; set; } = false;  // Main category
        public int SortOrder { get; set; } = 0;       // Display order
        public bool IsActive { get; set; } = true;
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
        public string AssignedBy { get; set; }        // Who assigned this category

        // Navigation
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
