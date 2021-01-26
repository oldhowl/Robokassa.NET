using Microsoft.AspNetCore.Mvc;

namespace Robokassa.NET.Example.Controllers
{
    [Route("shop/paymentResult")]
    public class ResultPaymentViewController : ControllerBase
    {
        [HttpGet("success")]
        public IActionResult Success(decimal outSum, int invId, string signatureValue) => Ok($"Payment successfully completed! Sum: {outSum}, InvoiceID : {invId}, Signature: {signatureValue}");

        [HttpGet("fail")]
        public IActionResult Fail() => Ok("Payment failed");
    }
}