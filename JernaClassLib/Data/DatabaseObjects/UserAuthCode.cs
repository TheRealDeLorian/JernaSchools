using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class UserAuthCode
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? AuthCodeId { get; set; }

    public virtual AuthCode? AuthCode { get; set; }

    public virtual User? User { get; set; }
}
