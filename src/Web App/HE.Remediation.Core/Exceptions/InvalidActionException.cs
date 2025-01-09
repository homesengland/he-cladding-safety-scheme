namespace HE.Remediation.Core.Exceptions;

public class InvalidActionException : Exception
{
    public InvalidActionException(string message) : base(message)
    {
    }
}