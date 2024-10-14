using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Exceptions;

public class NothingInStorageException : Exception
{
    private const string DefaultMessage = "Nothing in secure storage";
    public NothingInStorageException() : base(DefaultMessage) { }
}
