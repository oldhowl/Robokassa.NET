using System;

namespace RMusicians.API.Services.Payment.Exceptions
{
    public class RobokassaBaseException : Exception
    {
        public RobokassaBaseException(string? message) : base(message)
        {
        }
    }
}