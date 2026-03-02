namespace Techora.Models
{
    public class VwProduct
    {
        
            public int ProductId { get; set; }
            public string ProductName { get; set; } = null!;
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public int CategoryId { get; set; }
            public DateTime CreatedAt { get; set; }
            public string? Brand { get; set; }
            public string? Model { get; set; }
            public string? Color { get; set; }
            public double? Weight { get; set; }
            public double? ScreenSize { get; set; }
            public string? ScreenType { get; set; }
            public string? Resolution { get; set; }
            public string? Processor { get; set; }
            public string? RAM { get; set; }
            public string? Storage { get; set; }
            public string? GPU { get; set; }
            public int? BatteryCapacity { get; set; }   // mAh
            public double? BatteryLife { get; set; }   // hours
            public string? RearCamera { get; set; }
            public string? FrontCamera { get; set; }
            public bool? Wifi { get; set; }
            public bool? Bluetooth { get; set; }
            public string? Ports { get; set; }
            public string? OperatingSystem { get; set; }
            public string? Warranty { get; set; }
            public DateTime? ReleaseDate { get; set; }
        }
    }
