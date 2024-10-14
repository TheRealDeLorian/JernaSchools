using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class AuthCode
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public DateOnly? IsValidUntil { get; set; }

    public virtual ICollection<UserAuthCode> UserAuthCodes { get; set; } = new List<UserAuthCode>();
}
