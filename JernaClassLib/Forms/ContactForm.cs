using System.ComponentModel.DataAnnotations;

namespace JernaClassLib.Forms;

public class ContactForm
{
    [EmailAddress]
    [Required]
    public string? CustomerEmail { get; set; }
    [Required]
    public string? Message { get; set; }
}



