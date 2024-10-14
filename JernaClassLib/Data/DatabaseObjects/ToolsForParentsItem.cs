using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class ToolsForParentsItem
{
    public int Id { get; set; }

    public int SubId { get; set; }

    public int ItemId { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Item Sub { get; set; } = null!;
}
