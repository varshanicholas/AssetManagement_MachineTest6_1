using System;
using System.Collections.Generic;

namespace MachineTest6_1.Model;

public partial class AssetsMaster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? AssetTypeId { get; set; }

    public int? PurchaseOrderId { get; set; }

    public string? SerialNumber { get; set; }

    public string? Model { get; set; }

    public string? Manufacturer { get; set; }

    public string? WarrantyPeriod { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal? Cost { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public virtual AssetCategory? AssetType { get; set; } = null!;

    public virtual PurchaseOrder? PurchaseOrder { get; set; }
}
