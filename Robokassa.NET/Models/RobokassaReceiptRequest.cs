using System.Collections.Generic;
using Newtonsoft.Json;

namespace RMusicians.API.Services.Payment.Models
{
    public class OrderItem
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("quantity")] public int Quantity { get; set; }

        [JsonProperty("sum")] public decimal Sum { get; set; }

        [JsonProperty("payment_method")] public string PaymentMethod { get; set; }

        [JsonProperty("payment_object")] public string PaymentObject { get; set; }

        [JsonProperty("tax")] public string Tax { get; set; }



        public OrderItem(string name, int quantity, decimal sum, string paymentMethod = "full_payment", string paymentObject = "service", string tax = "none", string nomenclatureCode = null)
        {
            Name = name;
            Quantity = quantity;
            Sum = sum;
            PaymentMethod = paymentMethod;
            PaymentObject = paymentObject;
            Tax = tax;
        }
    }

    public class RobokassaReceiptRequest
    {
        [JsonProperty("sno")] public string Sno { get; set; }

        [JsonProperty("items")] public IList<OrderItem> Items { get; set; }

        public RobokassaReceiptRequest(string sno, IList<OrderItem> items)
        {
            Sno = sno;
            Items = items;
        }
        
    }
}