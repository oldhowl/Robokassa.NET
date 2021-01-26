namespace Robokassa.NET.Exceptions
{
    public class InvalidSignatureException : RobokassaBaseException
    {
        public InvalidSignatureException(string expectedSignature, string invalidSignature)
            : base($"Signature validation error. expected: {expectedSignature}, but was: {invalidSignature}")
        {
        }
    }
}