using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class TagItem
{
    public int Id { get; set; }

    public int? TagId { get; set; }

    public int? ItemId { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Tag? Tag { get; set; }
}
