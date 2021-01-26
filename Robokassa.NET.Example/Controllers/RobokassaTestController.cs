using System;
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
            [FromForm] RobokassaCallbackRequest request)
        {
            try
            {
                robokassaPaymentValidator.CheckResult(request.OutSum, request.InvId, request.SignatureValue);
                //_paymentProcessor.OnSuccess(request.InvId, request.OutSumDec, request.EMail);
                return Content($"OK{request.InvId}");
            }
            catch (RobokassaBaseException e)
            {
                //_log.Error(e.Message);
                //_paymentProcessor.OnFail(request.InvId, request.OutSumDec, request.EMail);
                return Content($"OK{request.InvId}");
            }
        }
    }
}