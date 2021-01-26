namespace Robokassa.NET
{
    public interface IRobokassaPaymentValidator
    {
        void CheckResult(string sumString, int invId, string signatureValue);
    }
}