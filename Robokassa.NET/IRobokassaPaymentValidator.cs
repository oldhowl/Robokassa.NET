namespace RMusicians.API.Services.Payment
{
    public interface IRobokassaPaymentValidator
    {
        void CheckResult(string sumString,decimal outSum, int invId, string signatureValue, string paymentMethod, string eMail);
    }
}