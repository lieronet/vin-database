using System.Numerics;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace vin_db.Helper_Functions
{
    public static class Parsers
    {
        public static decimal? ParseDecimal(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            if (!decimal.TryParse(input, out var result)) return null;
            return result;
        }
        public static int? ParseInt(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            if (!int.TryParse(input, out var result)) return null;
            return result;
        }
    }
}
