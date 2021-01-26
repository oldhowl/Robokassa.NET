using System;
using Newtonsoft.Json;
using Robokassa.NET.Enums;
using Robokassa.NET.Extensions;

namespace Robokassa.NET.Models
{
    public class ReceiptOrderItem
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("quantity")] public int Quantity { get; set; }

        [JsonProperty("sum")] public decimal Sum { get; set; }

        [JsonProperty("payment_method")] public string PaymentMethod { get; set; }

        [JsonProperty("payment_object")] public string PaymentObject { get; set; }

        [JsonProperty("tax")] public string Tax { get; set; }


        /// <summary>
        /// Позиция товара в чеке
        /// </summary>
        /// <param name="name">Наименование товара. Строка, максимальная длина 64 символа. Если в наименовании товара Вы используете специальные символы, например кавычки, то их обязательно необходимо экранировать. </param>
        /// <param name="quantity">Обязательное поле. Количество/вес конкретной товарной позиции. Десятичное число: целая часть не более 5 знаков, дробная часть не более 3 знаков.</param>
        /// <param name="sum">Обязательное поле. Полная сумма в рублях за все количество данного товара с учетом всех возможных скидок, бонусов и специальных цен. Десятичное положительное число: целая часть не более 8 знаков, дробная часть не более 2 знаков.</param>
        /// <param name="paymentMethod">Признак способа расчёта.</param>
        /// <param name="paymentObject">Признак предмета расчёта.</param>
        /// <param name="tax">Это поле устанавливает налоговую ставку в ККТ. Определяется для каждого вида товара по отдельности, но за все единицы конкретного товара вместе.</param>
        /// <param name="nomenclatureCode">Маркировка товара, передаётся в виде кода товара. Максимальная длина – 32 байта (32 символа). Параметр является обязательным только для тех магазинов, которые продают товары подлежащие обязательной маркировке.</param>
        public ReceiptOrderItem(
            string name,
            int quantity,
            decimal sum,
            Tax tax,
            PaymentMethod? paymentMethod,
            PaymentObject? paymentObject,
            string nomenclatureCode = null)
        {
            Quantity = quantity;
            Sum = sum;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PaymentMethod = paymentMethod?.ToSnakeCaseName();
            PaymentObject = paymentObject?.ToSnakeCaseName();
            Tax = tax.ToSnakeCaseName();
        }
    }
}