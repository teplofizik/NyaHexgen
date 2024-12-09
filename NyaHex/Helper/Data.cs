using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Helper
{
    public static class Data
    {
        /// <summary>
        /// Попытаться преобразовать знаковое число
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Res"></param>
        /// <returns></returns>
        public static bool ParseSigned(string Value, out long Res, long Min, long Max)
        {
            if (Int64.TryParse(Value, out Res))
            {
                if (Res < Min)
                {
                    Log.Error($"Value {Value} is smaller than min");
                    return false;
                }
                if (Res > Min)
                {
                    Log.Error($"Value {Value} is greater than max");
                    return false;
                }
                return true;
            }
            else
            {
                Log.Error($"Value {Value} cannot be converted to signed number");
                return false;
            }
        }

        /// <summary>
        /// Попытаться преобразовать знаковое число
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Res"></param>
        /// <returns></returns>
        public static bool ParseUnsigned(string Value, out ulong Res, ulong Min, ulong Max)
        {
            if (Value.StartsWith("0x"))
            {
                IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
                if (UInt64.TryParse(Value.Substring(2), NumberStyles.AllowHexSpecifier, provider, out Res))
                {
                    if (Res < Min)
                    {
                        Log.Error($"Value {Value} is smaller than min");
                        return false;
                    }
                    if (Res > Max)
                    {
                        Log.Error($"Value {Value} is greater than max");
                        return false;
                    }
                    return true;
                }
                else
                {
                    Log.Error($"Value {Value} cannot be converted to unsigned number");
                    return false;
                }
            }
            else
            {
                if (UInt64.TryParse(Value, out Res))
                {
                    if (Res < Min)
                    {
                        Log.Error($"Value {Value} is smaller than min");
                        return false;
                    }
                    if (Res > Max)
                    {
                        Log.Error($"Value {Value} is greater than max");
                        return false;
                    }
                    return true;
                }
                else
                {
                    Log.Error($"Value {Value} cannot be converted to unsigned number");
                    return false;
                }
            }
        }
    }
}
