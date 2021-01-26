using System.Globalization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RMusicians.API.Application.Options;
using RMusicians.API.Services.Payment.Exceptions;

namespace RMusicians.API.Services.Payment
{
    public class RobokassaCallbackValidator : IRobokassaPaymentValidator
    {
        private readonly string _secondPassword;

        public RobokassaCallbackValidator(IOptions<RobokassaOptions> options)
        {
            _secondPassword = options.Value.Password2;
        }

        public void CheckResult(string sumString,decimal outSum, int invId, string signatureValue, string paymentMethod, string eMail)
        {
            if (invId == 0)
                throw new InvalidCallbackRequest(JsonConvert.SerializeObject(new
                {
                    invId, outSum, signatureValue, paymentMethod, eMail
                }, Formatting.Indented));

            string
                outSummStr = sumString,
                outInvIdStr = invId.ToString(),
                srcBase = $"{outSummStr}:{outInvIdStr}:{_secondPassword}";

            var srcMD5Hash = Md5HashService.GenerateMd5Hash(srcBase);

            if (!signatureValue.ToLower().Equals(srcMD5Hash))
                throw new InvalidSignatureException(signatureValue, srcMD5Hash);
        }
    }
}