using System.Collections.Generic;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Robokassa.NET.Exceptions;
using Robokassa.NET.Models;

namespace Robokassa.NET.Example.Controllers
{
    /// <summary>
    /// Указыватся в настройках магазина робокассы, параметр Result Url
    /// </summary>
    [Route("paymentResult")]
    public class RobokassaTestController : ControllerBase
    {
        [HttpPost]
        public IActionResult Process(
            [FromServices] IRobokassaPaymentValidator robokassaPaymentValidator,
            [FromForm] RobokassaCallbackRequest request,
            [FromForm(Name = "Shp_login")] string shpLogin,
            [FromForm(Name = "Shp_name")] string name,
            [FromForm(Name = "Shp_email")] string email,
            [FromForm(Name = "Shp_oplata")] string shpOplata
        )
        {
            try
            {
                robokassaPaymentValidator.CheckResult(
                    request.OutSum,
                    request.InvId,
                    request.SignatureValue,

                    //Не обязательные кастомные shp поля
                    new KeyValuePair<string, string>("Shp_login", shpLogin),
                    new KeyValuePair<string, string>("Shp_name", name),
                    new KeyValuePair<string, string>("Shp_email", email),
                    new KeyValuePair<string, string>("Shp_oplata", shpOplata)
                );

                PaymentProcessor.OnSuccessPayment(
                    request.InvId,
                    request.OutSumDec,
                    //Декодируем кириллицу
                    HttpUtility.UrlDecode(name),
                    shpLogin,
                    //Декодируем @
                    HttpUtility.UrlDecode(email));
            }
            catch (RobokassaBaseException)
            {
                //_log.Error(e.Message);

                PaymentProcessor.OnFailPayment(
                    request.InvId,
                    request.OutSumDec,
                    //Декодируем кириллицу
                    HttpUtility.UrlDecode(name),
                    shpLogin,
                    //Декодируем @
                    HttpUtility.UrlDecode(email));
            }

            //В любом случае возвращаем ОК 
            return Content($"OK{request.InvId}");
        }
    }
}