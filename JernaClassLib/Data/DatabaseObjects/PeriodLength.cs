using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class PeriodLength
{
    public int Id { get; set; }

    public string? DisplayName { get; set; }

    public TimeSpan Timespan { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
