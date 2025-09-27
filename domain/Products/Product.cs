using domain.Stocks;
using domain.Suppliers;
using domain.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Products
{
    public class Product : BaseEntity<int>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Sku { get; set; }     // Unique code - will be unique in DB
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal UnitCost { get; set; }        // Cost per unit
        public decimal SellingPrice { get; set; }    // Selling price per unit
        public int ReorderLevel { get; set; }        // When to reorder
        public int ReorderQuantity { get; set; }     // How much to reorder
        public bool IsActive { get; set; } = true;   // Product availability
        public int? SupplierId { get; set; }         // Primary supplier
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<Stock> Stocks { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
