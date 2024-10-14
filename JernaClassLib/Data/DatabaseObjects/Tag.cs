using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class Tag
{
    public int Id { get; set; }

    public string? TagName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<TagItem> TagItems { get; set; } = new List<TagItem>();
}
