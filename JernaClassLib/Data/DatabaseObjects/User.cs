using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Notes { get; set; }

    public bool? Isadmin { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<TempCode> TempCodes { get; set; } = new List<TempCode>();

    public virtual ICollection<UserAuthCode> UserAuthCodes { get; set; } = new List<UserAuthCode>();
}
