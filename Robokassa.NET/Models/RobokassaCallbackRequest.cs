namespace Robokassa.NET.Models
{
    public class RobokassaCallbackRequest
    {
        /// <summary> 
        /// Исходящий баланс: сумма к зачислению (сумма, которую указал пользователь) 
        /// </summary>
        ///
        
        public string OutSum { get; set; }

        /// <summary> 
        /// Входящий баланс: сумма оплаты + комиссия 
        /// </summary> 
        
        public string IncSum { get; set; }

        public decimal OutSumDec
        {
            get
            {
                try
                {
                    return System.Convert.ToDecimal(OutSum);
                }
                catch
                {
                    return 0M;
                }
            }
            set
            {
                try
                {
                    OutSum = value.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    OutSum = "0.000000";
                }
            }
        }

        public decimal IncSumDec
        {
            get
            {
                try
                {
                    return System.Convert.ToDecimal(IncSum);
                }
                catch
                {
                    return 0M;
                }
            }
            set
            {
                try
                {
                    IncSum = value.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    IncSum = "0.000000";
                }
            }
        }

         public int InvId { get; set; }
         public string SignatureValue { get; set; }
         public string IncCurrLabel { get; set; }
         public string EMail { get; set; }
    }
}