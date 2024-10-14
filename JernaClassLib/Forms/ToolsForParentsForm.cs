using JernaClassLib.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Forms;

public class ToolsForParentsForm
{
    [Required]
    public string? Name { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsDigital { get; set; } = true;
    public List<int>? ChildrensAges { get; set; }
    [Required]
    public string? SubscriptionLength { get; set; }
    [Required]
    public string? ToolsType { get; set; }
}
