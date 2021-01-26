using System.Collections.Generic;

namespace Robokassa.NET
{
    public interface IRobokassaPaymentValidator
    {
        void CheckResult(string sumString, int invId, string signatureValue, params KeyValuePair<string,string>[] shpParams);
    }
}