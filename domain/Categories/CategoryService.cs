using domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Categories
{
    public class CategoryService
    {
        /// <summary>
        /// Assigns a product to a category
        /// </summary>
        public static ProductCategory AssignProductToCategory(
            int productId, 
            int categoryId, 
            bool isPrimary = false, 
            string assignedBy = null)
        {
            return new ProductCategory
            {
                ProductId = productId,
                CategoryId = categoryId,
                IsPrimary = isPrimary,
                AssignedDate = DateTime.UtcNow,
                AssignedBy = assignedBy,
                IsActive = true
            };
        }

        /// <summary>
        /// Gets all categories for a product
        /// </summary>
        public static List<Category> GetProductCategories(List<ProductCategory> productCategories, int productId)
        {
            return productCategories
                .Where(pc => pc.ProductId == productId && pc.IsActive)
                .OrderBy(pc => pc.IsPrimary ? 0 : 1)
                .ThenBy(pc => pc.SortOrder)
                .Select(pc => pc.Category)
                .ToList();
        }

        /// <summary>
        /// Gets the primary category for a product
        /// </summary>
        public static Category GetPrimaryCategory(List<ProductCategory> productCategories, int productId)
        {
            return productCategories
                .Where(pc => pc.ProductId == productId && pc.IsPrimary && pc.IsActive)
                .Select(pc => pc.Category)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets all products in a category
        /// </summary>
        public static List<Product> GetCategoryProducts(List<ProductCategory> productCategories, int categoryId)
        {
            return productCategories
                .Where(pc => pc.CategoryId == categoryId && pc.IsActive)
                .OrderBy(pc => pc.SortOrder)
                .Select(pc => pc.Product)
                .ToList();
        }

        /// <summary>
        /// Creates a hierarchical category structure
        /// </summary>
        public static Category CreateCategory(
            string name, 
            string code, 
            string description = null, 
            int? parentCategoryId = null, 
            int sortOrder = 0)
        {
            return new Category
            {
                Name = name,
                Code = code,
                Description = description,
                ParentCategoryId = parentCategoryId,
                SortOrder = sortOrder,
                IsActive = true
            };
        }

        /// <summary>
        /// Gets all subcategories of a parent category
        /// </summary>
        public static List<Category> GetSubCategories(List<Category> categories, int parentCategoryId)
        {
            return categories
                .Where(c => c.ParentCategoryId == parentCategoryId && c.IsActive)
                .OrderBy(c => c.SortOrder)
                .ToList();
        }

        /// <summary>
        /// Gets the full category path (breadcrumb)
        /// </summary>
        public static List<Category> GetCategoryPath(List<Category> categories, int categoryId)
        {
            var path = new List<Category>();
            var current = categories.FirstOrDefault(c => c.Id == categoryId);
            
            while (current != null)
            {
                path.Insert(0, current);
                current = categories.FirstOrDefault(c => c.Id == current.ParentCategoryId);
            }
            
            return path;
        }
    }
}
