using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Products
{
    public class MobilePhone : Product
    {
        // Mobile-specific properties
        public string Brand { get; set; }           // Samsung, iPhone, etc.
        public string Model { get; set; }           // Galaxy S24, iPhone 15, etc.
        public string Color { get; set; }           // Black, White, Blue, etc.
        public int StorageGB { get; set; }          // 128GB, 256GB, 512GB
        public string OperatingSystem { get; set; } // Android, iOS
        public string ScreenSize { get; set; }      // 6.1", 6.7", etc.
        public string Processor { get; set; }       // Snapdragon 8 Gen 3, A17 Pro
        public int BatteryCapacity { get; set; }    // mAh
        public bool IsUnlocked { get; set; }        // Unlocked or carrier-locked
        public string IMEI { get; set; }            // Unique device identifier
        public string SerialNumber { get; set; }    // Device serial number
        public DateTime? WarrantyExpiry { get; set; } // Warranty end date
    }
}