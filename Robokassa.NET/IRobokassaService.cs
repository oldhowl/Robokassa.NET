using RMusicians.API.Services.Payment.Models;

namespace RMusicians.API.Services.Payment
{
    public interface IRobokassaService
    {
        PaymentUrl GenerateAuthLink(decimal totalAmount, object invoiceId, RobokassaReceiptRequest receipt);
    }
}