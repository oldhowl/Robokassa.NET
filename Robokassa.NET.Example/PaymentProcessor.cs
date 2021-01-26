using System;
using System.Threading.Tasks;

namespace Robokassa.NET.Example
{
    public class PaymentProcessor
    {
        public static void OnSuccessPayment(int invoiceId, decimal sum, string name, string login, string email)
        {
            Console.WriteLine($"Success: {invoiceId}, {sum}, {name}, {login}, {email}");
        }

        public static void OnFailPayment(int invoiceId, decimal sum, string name, string login, string email)
        {
            Console.WriteLine($"Fail: {invoiceId}, {sum}, {name}, {login}, {email}");
        }
    }
}