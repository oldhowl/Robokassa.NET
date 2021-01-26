using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Robokassa.NET.Enums;
using Robokassa.NET.Extensions;

namespace Robokassa.NET.Models
{

    public class RobokassaReceiptRequest
    {
        [JsonProperty("sno")] public string Sno { get; set; }

        [JsonProperty("items")] public IList<ReceiptOrderItem> Items { get; set; }

        public decimal TotalPrice => Items.Sum(x => x.Sum);

        public RobokassaReceiptRequest(SnoType snoType, IList<ReceiptOrderItem> items)
        {
            Sno = snoType.ToSnakeCaseName();
            Items = items;
        }

        public override string ToString()
        {
            return HttpUtility.UrlEncode(JsonConvert.SerializeObject(this));
        }
    }
}