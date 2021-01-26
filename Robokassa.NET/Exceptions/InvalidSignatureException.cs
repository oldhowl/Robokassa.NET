namespace RMusicians.API.Services.Payment.Exceptions
{
    public class InvalidSignatureException : RobokassaBaseException
    {
        public InvalidSignatureException(string expectedSignature, string invalidSignature)
            : base($"Ошибка проверки подписи. Ожидаемая сигнатура: {expectedSignature}, пришла: {invalidSignature}")
        {
        }
    }
}