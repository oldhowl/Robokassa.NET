using Robokassa.NET.Models;

namespace Robokassa.NET
{
    public interface IRobokassaService
    {
        PaymentUrl GenerateAuthLink(decimal totalAmount, int invoiceId, RobokassaReceiptRequest receipt = null, CustomShpParameters shpParameters = null);
    }
}