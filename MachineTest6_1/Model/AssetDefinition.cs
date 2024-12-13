using System;
using System.Collections.Generic;

namespace MachineTest6_1.Model;

public partial class AssetDefinition
{
    public int AssetId { get; set; }

    public string? AssetName { get; set; }

    public int? CategoryId { get; set; }

    public string? AssetClass { get; set; }

    public virtual AssetCategory? Category { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
