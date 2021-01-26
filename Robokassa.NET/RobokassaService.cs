using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RMusicians.API.Application.Options;
using RMusicians.API.Services.Payment.Models;

namespace RMusicians.API.Services.Payment
{
    public class RobokassaService : IRobokassaService
    {
        private readonly RobokassaOptions _options;
        private readonly bool _isTestEnv;

        public RobokassaService(IOptions<RobokassaOptions> options, IWebHostEnvironment webHostEnvironment)
            : this(options.Value, !webHostEnvironment.IsProduction())
        {
        }

        public RobokassaService(RobokassaOptions options, bool isTestEnv)
        {
            _isTestEnv = isTestEnv;
            _options = options;
        }


        public PaymentUrl GenerateAuthLink(
            decimal totalAmount,
            object invoiceId,
            RobokassaReceiptRequest receipt = null)
        {
            var receiptEncodedJson =
                receipt != null ? HttpUtility.UrlEncode(JsonConvert.SerializeObject(receipt)) : null;

            var amountStr = totalAmount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            var invoiceIdStr = invoiceId.ToString();
            
            var signatureValue =
                Md5HashService
                    .GenerateMd5Hash(
                        PrepareMd5SumString(amountStr, invoiceIdStr, receiptEncodedJson));

            return new PaymentUrl(BuildPaymentLink(invoiceIdStr, amountStr, signatureValue, receiptEncodedJson));
        }

        private string BuildPaymentLink(string invoiceId, string amount, string signature, string receiptEncodedJson)
        {
            var host = "https://auth.robokassa.ru/Merchant/Index.aspx?";

            var parameters = new List<string>();

            if (_isTestEnv)
                parameters.Add("isTest=1");

            parameters.Add("MrchLogin=" + _options.ShopName);
            parameters.Add("InvId=" + invoiceId);
            parameters.Add("OutSum=" + amount);

            if (!string.IsNullOrEmpty(receiptEncodedJson))
                parameters.Add("Receipt=" + HttpUtility.UrlEncode(receiptEncodedJson));

            parameters.Add("SignatureValue=" + signature);
            parameters.Add("Culture=ru");

            return host + string.Join("&", parameters);
        }

        private string PrepareMd5SumString(string amount, string invoiceId, string receiptEncodedJson)
        {
            return string.Join(":", new List<string>
            {
                _options.ShopName,
                amount,
                invoiceId,
                receiptEncodedJson,
                _options.Password1
            }.Where(x => x != null));
        }
    }
}