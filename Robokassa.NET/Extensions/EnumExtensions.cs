using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Robokassa.NET.Extensions
{
    public static class EnumExtensions
    {
        public static string ToSnakeCaseName(this Enum enumValue)
        {
            return string.Concat(enumValue.ToString()
                    .Replace("_", "-")
                    .Select((x, i) => { return i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString(); }))
                .ToLower();
        }
    }
}