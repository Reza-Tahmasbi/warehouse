using domain;
using domain.Products;
using domain.Stocks;
using domain.Suppliers;
using domain.Customers;
using domain.Transfers;
using domain.Cardex;
using domain.Warehouses;
using domain.Orders;
using domain.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;


namespace persis.context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<MobilePhone> MobilePhones { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferItem> TransferItems { get; set; }
        public DbSet<Cardex> Cardex { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Table-Per-Hierarchy (TPH) inheritance
            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<Product>("Product")
                .HasValue<MobilePhone>("MobilePhone")
                .HasValue<Laptop>("Laptop");

            // Configure 1-to-1: Product → Stock
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Stocks)
                .HasForeignKey(s => s.ProductId);

            // Configure Product → Supplier relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure Transfer → Supplier relationship
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.Supplier)
                .WithMany()
                .HasForeignKey(t => t.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Transfer → Customer relationship
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.Customer)
                .WithMany()
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure TransferItem → Transfer relationship
            modelBuilder.Entity<TransferItem>()
                .HasOne(ti => ti.Transfer)
                .WithMany(t => t.TransferItems)
                .HasForeignKey(ti => ti.TransferId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure TransferItem → Product relationship
            modelBuilder.Entity<TransferItem>()
                .HasOne(ti => ti.Product)
                .WithMany()
                .HasForeignKey(ti => ti.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Cardex → Product relationship
            modelBuilder.Entity<Cardex>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // Configure Cardex → Transfer relationship
            modelBuilder.Entity<Cardex>()
                .HasOne(c => c.Transfer)
                .WithMany()
                .HasForeignKey(c => c.TransferId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Cardex → TransferItem relationship
            modelBuilder.Entity<Cardex>()
                .HasOne(c => c.TransferItem)
                .WithMany()
                .HasForeignKey(c => c.TransferItemId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Stock → Warehouse relationship
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Warehouse)
                .WithMany(w => w.Stocks)
                .HasForeignKey(s => s.WarehouseId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Order → Customer relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure OrderItem → Order relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure OrderItem → Product relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure unique constraints
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Sku)
                .IsUnique();

            modelBuilder.Entity<Warehouse>()
                .HasIndex(w => w.Code)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            // Configure many-to-many: Product ↔ Category
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure self-referencing: Category → ParentCategory
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure unique constraints
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // Configure decimal precision for SQL Server
            modelBuilder.Entity<Product>()
                .Property(p => p.UnitCost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.SellingPrice)
                .HasPrecision(18, 2);


            modelBuilder.Entity<Customer>()
                .Property(c => c.CreditLimit)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cardex>()
                .Property(c => c.UnitCost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cardex>()
                .Property(c => c.TotalCost)
                .HasPrecision(18, 2);

            // Global Query Filters (like soft-delete if we later add IsRemoved flag)
            ApplyQueryFilter(modelBuilder);

            // Seed Data (temporarily commented out for migration)
            // SeedData(modelBuilder);
        }
        private void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            // Apply soft delete filter only to the root Product entity
            // This will automatically apply to all inherited entities (MobilePhone, Laptop)
            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => !p.IsRemoved);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Example: seed a Product
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Sample Product",
                Sku = "PRD001",
                Description = "Demo product",
                UnitOfMeasure = "Piece",
                UnitCost = 10.50m,
                SellingPrice = 15.99m,
                ReorderLevel = 20,
                ReorderQuantity = 100,
                IsActive = true,
                SupplierId = null, // Will be set after supplier is created
                InsertTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            // Example: seed stock
            modelBuilder.Entity<Stock>().HasData(new Stock
            {
                Id = 1,
                ProductId = 1,
                Quantity = 100,
                ReservedQuantity = 0,
                Location = "A-01-01", // Warehouse location
                LastUpdated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                InsertTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            // Example: seed categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    Code = "ELEC",
                    Description = "Electronic devices and gadgets",
                    ParentCategoryId = null,
                    SortOrder = 1,
                    IsActive = true,
                    InsertTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 2,
                    Name = "Mobile Phones",
                    Code = "MOBILE",
                    Description = "Smartphones and mobile devices",
                    ParentCategoryId = 1, // Electronics
                    SortOrder = 1,
                    IsActive = true,
                    InsertTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 3,
                    Name = "Laptops",
                    Code = "LAPTOP",
                    Description = "Portable computers",
                    ParentCategoryId = 1, // Electronics
                    SortOrder = 2,
                    IsActive = true,
                    InsertTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Example: seed product-category relationships (JUNCTION TABLE DATA)
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory
                {
                    Id = 1,
                    ProductId = 1,
                    CategoryId = 1, // Electronics (Primary)
                    IsPrimary = true,
                    SortOrder = 1,
                    IsActive = true,
                    AssignedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    AssignedBy = "System",
                    InsertTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new ProductCategory
                {
                    Id = 2,
                    ProductId = 1,
                    CategoryId = 2, // Mobile Phones (Secondary)
                    IsPrimary = false,
                    SortOrder = 2,
                    IsActive = true,
                    AssignedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    AssignedBy = "System",
                    InsertTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
