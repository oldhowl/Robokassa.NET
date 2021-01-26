namespace RMusicians.API.Services.Payment.Models
{
    public class PaymentUrl
    {
        public PaymentUrl(string link)
        {
            Link = link;
        }

        public string Link { get;  }
    }
}