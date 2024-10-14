using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class Transaction
{
    public int Id { get; set; }

    public decimal? PurchasePrice { get; set; }

    public int? PurchaseId { get; set; }

    public virtual Purchase? Purchase { get; set; }
}
