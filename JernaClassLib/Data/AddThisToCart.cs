using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Data;

public class AddThisToCart
{
    public required int ItemId { get; set; }
    public required int UserId { get; set; }
    public required int Amount { get; set; }
    public string? ContactInfo { get; set; }
}
