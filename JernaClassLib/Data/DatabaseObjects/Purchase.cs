using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class Purchase
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime PurchaseDate { get; set; }

    public double Taxpercent { get; set; }

    public virtual ICollection<AdminHistory> AdminHistories { get; set; } = new List<AdminHistory>();

    public virtual ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User? User { get; set; }
}
