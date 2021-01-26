using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Robokassa.NET.Models;

namespace Robokassa.NET
{
    public class RobokassaService : IRobokassaService
    {
        private readonly RobokassaOptions _options;
        private readonly bool _isTestEnv;


        public RobokassaService(RobokassaOptions options, bool isTestEnv)
        {
            _isTestEnv = isTestEnv;
            _options = options;
        }


        public PaymentUrl GenerateAuthLink(
            decimal totalAmount,
            int invoiceId,
            RobokassaReceiptRequest receipt,
            CustomShpParameters customShpParameters)
        {
            var receiptEncodedJson = receipt?.ToString();

            var customFieldsLine = customShpParameters?.ToString();

            var amountStr = totalAmount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            
            var invoiceIdStr = invoiceId.ToString();

            var signatureValue =
                Md5HashService
                    .GenerateMd5Hash(
                        PrepareMd5SumString(amountStr, invoiceIdStr, receiptEncodedJson, customFieldsLine));

            return new PaymentUrl(BuildPaymentLink(invoiceIdStr, amountStr, signatureValue, receiptEncodedJson,
                customShpParameters));
        }

        private string BuildPaymentLink(string invoiceId, string amount, string signature, string receiptEncodedJson,
            CustomShpParameters customShpParameters)
        {
            const string host = "https://auth.robokassa.ru/Merchant/Index.aspx?";

            var parameters = new Collection<string>();

            if (_isTestEnv)
                parameters.Add("isTest=1");

            parameters.Add("MrchLogin=" + _options.ShopName);
            parameters.Add("InvId=" + invoiceId);
            parameters.Add("OutSum=" + amount);

            if (!string.IsNullOrEmpty(receiptEncodedJson))
                parameters.Add("Receipt=" + HttpUtility.UrlEncode(receiptEncodedJson));

            customShpParameters?
                .GetParameters?
                .ForEach(parameter =>
                    parameters.Add($"{parameter.Key}={HttpUtility.UrlEncode(HttpUtility.UrlEncode(parameter.Value))}"));

            parameters.Add("SignatureValue=" + signature);
            parameters.Add("Culture=ru");

            var url = host + string.Join("&", parameters);
            return url;
        }

        private string PrepareMd5SumString(string amount, string invoiceId, string receiptEncodedJson,
            string customParameters)
        {
            var str = string.Join(":", new List<string>
            {
                _options.ShopName,
                amount,
                invoiceId,
                receiptEncodedJson,
                _options.Password1,
                customParameters
            }.Where(x => x != null));

            return str;
        }
    }
}