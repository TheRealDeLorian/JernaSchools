using System;
using System.Collections.Generic;

namespace JernaClassLib.Data.DatabaseObjects;

public partial class AdminHistory
{
    public int Id { get; set; }

    public int? PurchaseId { get; set; }

    public DateTime FulfilledDate { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<AdminHistoryItem> AdminHistoryItems { get; set; } = new List<AdminHistoryItem>();

    public virtual Purchase? Purchase { get; set; }
}
