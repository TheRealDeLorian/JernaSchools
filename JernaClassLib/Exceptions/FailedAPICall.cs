using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Exceptions;

public class FailedAPICall : Exception
{
    public FailedAPICall() { }
    public FailedAPICall(string message) : base(message) { }
    public FailedAPICall(string message, Exception innerException) : base(message, innerException) { }
}
