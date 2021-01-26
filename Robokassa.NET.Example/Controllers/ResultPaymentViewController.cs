using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Robokassa.NET.Example.Controllers
{
    [Route("shop/paymentResult")]
    public class ResultPaymentViewController : ControllerBase
    {
        [HttpGet("success")]
        public IActionResult Success(
            decimal outSum,
            int invId,
            string signatureValue,
            [FromQuery(Name = "Shp_login")] string login,
            [FromQuery(Name = "Shp_name")] string name,
            [FromQuery(Name = "Shp_email")] string email,
            [FromQuery(Name = "Shp_oplata")] string payed)
            =>
                Ok(@$"
        Payment successfully completed! 
        Sum: {outSum}, 
        InvoiceID : {invId}, 
        Signature: {signatureValue}, 
        Login: {login},
        Payed count: {payed},
        Name: {HttpUtility.UrlDecode(name)},
        We send invoice to your email: {HttpUtility.UrlDecode(email)}");

        [HttpGet("fail")]
        public IActionResult Fail() => Ok("Payment failed");
    }
}