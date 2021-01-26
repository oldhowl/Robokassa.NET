using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robokassa.NET.Models
{
    public class CustomShpParameters
    {
        private const string CustomFieldPrefix = "Shp_";
        private List<KeyValuePair<string, string>> _values = new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, string>> GetParameters => _values.OrderBy(x => x.Key).ToList();

        public void Add(string shpKey, string shpValue)
        {
            if (shpKey == null) throw new ArgumentNullException(nameof(shpKey));
            if (shpValue == null) throw new ArgumentNullException(nameof(shpValue));

            if (shpKey.StartsWith(CustomFieldPrefix, StringComparison.InvariantCultureIgnoreCase))
                shpKey = shpKey.Remove(0, 4);

            _values.Add(new KeyValuePair<string, string>(CustomFieldPrefix + shpKey, shpValue));
        }

        public override string ToString()
        {
            return string.Join(":", GetParameters.Select(x => $"{x.Key}={HttpUtility.UrlEncode(x.Value)}"));
        }
    }
}