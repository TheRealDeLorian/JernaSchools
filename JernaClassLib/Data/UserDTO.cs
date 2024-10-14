using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Data;

public class UserDTO
{
    public required User User { get; set; }
    public required string AuthCode { get; set; }
}
