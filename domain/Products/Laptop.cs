using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Products
{
    public class Laptop : Product
    {
        // Laptop-specific properties
        public string Brand { get; set; }           // Dell, HP, Lenovo, etc.
        public string Model { get; set; }           // XPS 13, ThinkPad X1, etc.
        public string Color { get; set; }           // Silver, Black, etc.
        public int RAMGB { get; set; }              // 8GB, 16GB, 32GB
        public int StorageGB { get; set; }          // 256GB, 512GB, 1TB
        public string Processor { get; set; }       // Intel i7, AMD Ryzen 7, etc.
        public string GraphicsCard { get; set; }    // NVIDIA RTX 4060, etc.
        public string ScreenSize { get; set; }      // 13.3", 15.6", 17.3"
        public string Resolution { get; set; }      // 1920x1080, 2560x1440, etc.
        public string OperatingSystem { get; set; } // Windows 11, macOS, Linux
        public string SerialNumber { get; set; }    // Device serial number
        public DateTime? WarrantyExpiry { get; set; } // Warranty end date
    }
}