using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.Exceptions;

public class BadDataException : Exception
{
    public BadDataException(string exceptionMessage) : base(exceptionMessage) 
    { 
    }
}
