namespace Robokassa.NET.Exceptions
{
    public class InvalidCallbackRequest : RobokassaBaseException
    {
        public InvalidCallbackRequest(string callbackBody) : base($"Invalid Robokassa callback request: {callbackBody}")
        {
        }
    }
}