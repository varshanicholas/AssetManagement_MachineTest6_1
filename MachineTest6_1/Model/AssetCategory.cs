using System;
using System.Collections.Generic;

namespace MachineTest6_1.Model;

public partial class AssetCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<AssetDefinition> AssetDefinitions { get; set; } = new List<AssetDefinition>();

    public virtual ICollection<AssetsMaster> AssetsMasters { get; set; } = new List<AssetsMaster>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
