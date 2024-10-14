using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class AdminHistoryItem
{
    public int Id { get; set; }

    public int? AdminHistoryId { get; set; }

    public int? ItemId { get; set; }

    public virtual AdminHistory? AdminHistory { get; set; }

    public virtual Item? Item { get; set; }
}
