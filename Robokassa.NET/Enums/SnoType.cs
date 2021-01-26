namespace Robokassa.NET.Enums
{
    public enum SnoType
    {
        /// <summary>
        /// Общая СН
        /// </summary>
        Osn,
        
        /// <summary>
        /// Упрощенная СН (доходы)
        /// </summary>
        UsnIncome,
        
        /// <summary>
        /// Упрощенная СН (доходы минус расходы)
        /// </summary>
        UsnIncomeOutcome,
        
        /// <summary>
        /// Единый налог на вмененный доход
        /// </summary>
        Envd,
        
        /// <summary>
        /// Единый сельскохозяйственный налог
        /// </summary>
        Esn,
        
        /// <summary>
        /// Патентная СН
        /// </summary>
        Patent
    }
}