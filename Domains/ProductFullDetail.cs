using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Techora.Models;

public partial class ProductFullDetail
{
    [Key]
    public int DetailId { get; set; }

    public int? ProductId { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? Color { get; set; }

    public string? Weight { get; set; }

    public string? ScreenSize { get; set; }

    public string? ScreenType { get; set; }

    public string? Resolution { get; set; }

    public string? Processor { get; set; }

    public string? Ram { get; set; }

    public string? Storage { get; set; }

    public string? Gpu { get; set; }

    public string? BatteryCapacity { get; set; }

    public string? BatteryLife { get; set; }

    public string? RearCamera { get; set; }

    public string? FrontCamera { get; set; }

    public string? Wifi { get; set; }

    public string? Bluetooth { get; set; }

    public string? Ports { get; set; }

    public string? OperatingSystem { get; set; }

    public string? Warranty { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public virtual Product? Product { get; set; }
}
