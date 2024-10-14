using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Exceptions;

public class ImpossibleException : Exception
{
    private const string DefaultMessage = "How did this happen???";
    public ImpossibleException() : base(DefaultMessage) { }
}
