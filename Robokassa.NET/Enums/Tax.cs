namespace Robokassa.NET.Enums
{
    public enum Tax
    {
        /// <summary>
        /// Без НДС
        /// </summary>
        None,

        /// <summary>
        /// НДС по ставке 0%
        /// </summary>
        Vat0,

        /// <summary>
        /// НДС чека по ставке 10%
        /// </summary>
        Vat10,

        /// <summary>
        /// НДС чека по расчетной ставке 10/110
        /// </summary>
        Vat110,

        /// <summary>
        /// НДС чека по ставке 20%
        /// </summary>
        Vat20,

        /// <summary>
        /// НДС чека по расчетной ставке 20/120
        /// </summary>
        Vat120
    }
}