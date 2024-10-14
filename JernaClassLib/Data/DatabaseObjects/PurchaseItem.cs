using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class PurchaseItem
{
    public int Id { get; set; }

    public int? PurchaseId { get; set; }

    public int? ItemId { get; set; }

    public int Quantity { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Purchase? Purchase { get; set; }
}
