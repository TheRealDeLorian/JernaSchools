using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class CartItem
{
    public int Id { get; set; }

    public int? CartId { get; set; }

    public int? ItemId { get; set; }

    public int Quantity { get; set; }
    public string? ContactInfo { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
