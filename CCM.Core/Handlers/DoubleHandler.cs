using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CCM.Core.Handlers
{
    public static class DoubleHandler
    {
        public static String GetDecimalPoint( this double input, String pattern = "0.00")
        {
            return input.ToString(pattern, CultureInfo.InvariantCulture);
        }
    }
}
