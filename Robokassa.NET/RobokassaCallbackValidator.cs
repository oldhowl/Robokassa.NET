using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Robokassa.NET.Exceptions;

namespace Robokassa.NET
{
    public class RobokassaCallbackValidator : IRobokassaPaymentValidator
    {
        private readonly string _secondPassword;


        public RobokassaCallbackValidator(string password2) => _secondPassword = password2;

        public void CheckResult(string sumString, int invId, string signatureValue,
            params KeyValuePair<string, string>[] shpParams)
        {
            if (invId == 0)
                throw new InvalidCallbackRequest(JsonConvert.SerializeObject(new
                {
                    sumString, invId, signatureValue
                }));
            string
                outSummStr = sumString,
                outInvIdStr = invId.ToString(),
                srcBase = $"{outSummStr}:{outInvIdStr}:{_secondPassword}" + ":" + string.Join(":",
                    shpParams.OrderBy(x=>x.Key)
                        .Select(x => $"{x.Key}={x.Value}"));
            

            var srcMd5Hash = Md5HashService.GenerateMd5Hash(srcBase);

            if (!signatureValue.ToLower().Equals(srcMd5Hash))
                throw new InvalidSignatureException(signatureValue, srcMd5Hash);
        }
    }
}