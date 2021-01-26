using System.Security.Cryptography;
using System.Text;

namespace Robokassa.NET
{
    internal static class Md5HashService
    {
        public static string GenerateMd5Hash(string stringToHash)
        {
            var md5 = new MD5CryptoServiceProvider();
            var bSignature = md5.ComputeHash(Encoding.ASCII.GetBytes(stringToHash));

            var sbSignature = new StringBuilder();
            foreach (var b in bSignature)
                sbSignature.AppendFormat("{0:x2}", b);

            return sbSignature.ToString();
        }
    }
}