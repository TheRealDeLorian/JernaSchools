using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Data;

internal class AuthEmailInfo : EmailInfo
{
    [Required]
    public required string TempCode { get; set; }
}
