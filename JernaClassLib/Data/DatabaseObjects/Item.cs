using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class Item
{
    public int Id { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public byte[]? Image { get; set; }

    public bool Isdisplayed { get; set; }

    public byte[]? Mediafile { get; set; }

    public bool? IsDigital { get; set; }

    public bool? IsPhysical { get; set; }

    public int PeriodLengthId { get; set; }

    public virtual ICollection<AdminHistoryItem> AdminHistoryItems { get; set; } = new List<AdminHistoryItem>();

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual PeriodLength PeriodLength { get; set; } = null!;

    public virtual ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

    public virtual ICollection<TagItem> TagItems { get; set; } = new List<TagItem>();

    public virtual ICollection<ToolsForParentsItem> ToolsForParentsItemItems { get; set; } = new List<ToolsForParentsItem>();

    public virtual ICollection<ToolsForParentsItem> ToolsForParentsItemSubs { get; set; } = new List<ToolsForParentsItem>();
}
