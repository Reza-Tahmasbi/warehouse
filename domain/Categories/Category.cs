using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Categories
{
    public class Category : BaseEntity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }        // "Electronics", "Mobile Phones"
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        [MaxLength(50)]
        public string Code { get; set; }        // "ELEC", "MOBILE", "LAPTOP"
        
        public int? ParentCategoryId { get; set; }  // For subcategories
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; } = 0;     // Display order
        public string ImageUrl { get; set; }        // Category image
        public string Notes { get; set; }

        // Navigation
        public Category ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
