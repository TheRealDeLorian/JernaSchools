using System.ComponentModel.DataAnnotations;
namespace JernaClassLib.Data;

public class EmailInfo
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Subject { get; set; }

    [Required]
    public required string HTMLBody { get; set; }
}
