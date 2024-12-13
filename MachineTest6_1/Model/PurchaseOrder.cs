using System;
using System.Collections.Generic;

namespace MachineTest6_1.Model;

public partial class PurchaseOrder
{
    public int PurchaseId { get; set; }

    public string? OrderNo { get; set; }

    public int? AssetId { get; set; }

    public int? CategoryId { get; set; }

    public decimal? Quantity { get; set; }

    public int? VendorId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? OrderStatus { get; set; }

    public virtual AssetDefinition? Asset { get; set; }

    public virtual ICollection<AssetsMaster> AssetsMasters { get; set; } = new List<AssetsMaster>();

    public virtual AssetCategory? Category { get; set; }

    public virtual Vendor? Vendor { get; set; }
}
