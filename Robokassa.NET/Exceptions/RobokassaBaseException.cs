using System;

namespace Robokassa.NET.Exceptions
{
    public abstract class RobokassaBaseException : Exception
    {
        protected RobokassaBaseException(string message) : base(message)
        {
        }
    }
}