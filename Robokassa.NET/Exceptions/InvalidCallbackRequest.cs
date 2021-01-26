namespace RMusicians.API.Services.Payment.Exceptions
{
    public class InvalidCallbackRequest : RobokassaBaseException
    {
        public InvalidCallbackRequest(string callbackBody) : base($"Неверный запрос от робокассы, тело запроса: {callbackBody}")
        {
        }
    }
}