using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Exceptions;

public class InvalidAuthCodeException : Exception
{
    private const string DefaultMessage = "Invalid Auth code";
    public InvalidAuthCodeException() : base(DefaultMessage) { }
}
