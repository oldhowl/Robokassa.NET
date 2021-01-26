using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Robokassa.NET.Exceptions;

namespace Robokassa.NET
{
    public class RobokassaCallbackValidator : IRobokassaPaymentValidator
    {
        private readonly string _secondPassword;


        public RobokassaCallbackValidator(string password2) => _secondPassword = password2;

        public void CheckResult(string sumString, int invId, string signatureValue)
        {
            if (invId == 0)
                throw new InvalidCallbackRequest(JsonConvert.SerializeObject(new
                {
                    sumString, invId, signatureValue
                }));
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