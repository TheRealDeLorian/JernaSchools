using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Exceptions;

public class InvalidEmailException : Exception
{
    private const string DefaultMessage = "Email was null or did not contain a '@' sign";
    public InvalidEmailException() : base(DefaultMessage) { }
}
