using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Exceptions;

public class InvalidTempCodeException : Exception
{
    private const string DefaultMessage = "Invalid Temorary code";
    public InvalidTempCodeException() : base(DefaultMessage) { }
}
