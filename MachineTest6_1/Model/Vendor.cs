using System;
using System.Collections.Generic;

namespace MachineTest6_1.Model;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string? VendorName { get; set; }

    public string? VendorType { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? ContractStartDate { get; set; }

    public DateTime? ContractEndDate { get; set; }

    public string? VendorAddress { get; set; }

    public virtual AssetCategory? Category { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
