using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class TempCode
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public DateTime Createdate { get; set; }

    public int Userid { get; set; }

    public virtual User User { get; set; } = null!;
}
