//
// ********************************************************************************************************
// ********************************************************************************************************
// ***                                                                                                  ***
// *** Code Copyright © 2020, Will `Willow' Osborn.                                                     ***
// ***                                                                                                  ***
// *** This code is provided 'AS IS, NO WARRANTY' and is intended for no specific use or person.        ***
// *** In fact, the code herein is so confuggled, it should not be used by ANYONE EVER and ANYTHING     ***
// *** that happens as a result of its use is COMPLETELY and UTTERLY YOUR FAULT.  :p                    ***
// ***                                                                                                  ***
// *** You have my permission to extract, copy, modify, steal, borrow, beg, fold, spindle, mutilate or  ***
// *** otherwise abuse the code herein PROVIDED YOU LEAVE ME OUT OF IT! You Acknowledge and Accept      ***
// *** FULL and SOLE responsibility and culpability for ANYTHING you do with or around it.              ***
// ***                                                                                                  ***
// ********************************************************************************************************
// ********************************************************************************************************
//

using System.Linq;

namespace WillPower
{
    /// <summary>
    /// Container for extension methods.
    /// </summary>
    public static partial class Extensions
    {

        /// <summary>
        /// Removes the provided <see cref="System.String">string</see> from the <see cref="System.String">value</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> to evaluate.</param>
        /// <param name="remove">The <see cref="System.String">string</see> to remove.</param>
        /// <returns>The <see cref="System.String">value</see> without the removed <see cref="System.String">string</see>.</returns>
        public static string Remove(this string value, string remove)
        {
            return value.Replace(remove, "");
        }

        /// <summary>
        /// Returns a <see cref="System.String">string</see> containing only the <see cref="System.Char">characters</see> specified.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> to evaluate.</param>
        /// <param name="chars">The <see cref="System.Collections.ICollection">collection</see> or allowable <see cref="System.Char">characters</see>.</param>
        /// <returns>A <see cref="System.String">string</see> containing only the <see cref="System.Char">characters</see> specified.</returns>
        public static string Only(this string value, char[] chars)
        {
            string ret = "";
            foreach (char c in value?.ToCharArray())
            {
                if (chars.Contains(c))
                {
                    ret += c;
                }
            }
            return ret;
        }

        /// <summary>
        /// Determines if the <see cref="System.Object">object</see> is of a simple <see cref="System.Type">type</see>,
        /// including implementations of <see cref="System.Nullable">System.Nullable</see> upon simple types.
        /// </summary>
        /// <param name="value">The <see cref="System.Object">object</see> to evaluate.</param>
        /// <returns><see cref="bool">True</see> if the <see cref="System.Object">object</see> is not of a complex 
        /// <see cref="System.Type">type</see>, aka a <see cref="System.String">string</see>, a number, a 
        /// <see cref="System.DateTime">DateTime</see>, or a <see cref="System.Nullable">Nullable</see> implementation of any.
        /// </returns>
        public static bool IsSimpleType(this object value)
        {
            System.Type t = value.ForceGetType();
            if (t == null)
            {
                return false;
            }
            return !(t != typeof(string)
                && t != typeof(bool) && t != typeof(bool?)
                && t != typeof(byte) && t != typeof(byte?)
                && t != typeof(short) && t != typeof(short?)
                && t != typeof(int) && t != typeof(int?)
                && t != typeof(long) && t != typeof(long?)
                && t != typeof(System.DateTime) && t != typeof(System.DateTime?)
                && t != typeof(double) && t != typeof(double?)
                && t != typeof(decimal) && t != typeof(decimal?)
                && t != typeof(float) && t != typeof(float?));
        }

        /// <summary>
        /// Returns the <see cref="System.Type">System.Type</see> of the object even if it is null.
        /// </summary>
        /// <param name="value">The <see cref="System.Object">object</see> to evaluate.</param>
        /// <returns>The <see cref="System.Type">System.Type</see> of the object.</returns>
        public static System.Type ForceGetType(this object value)
        {
            if (value != null)
            {
                return value.GetType();
            }
            return GetObjectType(() => value);
        }
        private static System.Type GetObjectType(System.Linq.Expressions.Expression<System.Func<object>> expr)
        {
            var obj = ((System.Linq.Expressions.UnaryExpression)expr.Body).Operand;
            return (System.Type)(obj.GetType().GetProperty("Type", 
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)).GetValue(obj, null);
        }

        /// <summary>
        /// Returns a <see cref="System.String">string</see> containing only the numbers and, optionally, one decimal 
        /// (.) (the first encountered) from the value.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> to parse.</param>
        /// <param name="allowDecimal">If <see cref="bool">true</see> will allow a single '.' (the first encountered). 
        /// Default is <see cref="bool">false</see>.</param>
        /// <returns>A <see cref="System.String">string</see> containing only the numbers and, optionally, one decimal (.) 
        /// (the first encountered) from the value.</returns>
        public static string NumbersOnly(this string value, bool allowDecimal = false)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            bool foundDecimal = false;
            foreach (char c in value.ToCharArray())
            {
                if (c == '.' && allowDecimal && !foundDecimal)
                {
                    foundDecimal = true;
                    sb.Append(c);
                }
                else if (char.IsDigit(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns <see cref="bool">true</see> if all characters in the string are digits and,
        /// if allowDecimal is <see cref="bool">true</see>, only one '.' exists.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> to evaluate.</param>
        /// <param name="allowDecimal">If <see cref="bool">true</see> will allow a single '.' (the first encountered). 
        /// Default is <see cref="bool">false</see>.</param>
        /// <returns>
        /// <see cref="bool">True</see> if all characters are numbers and, optionally, only one decimal (.) exists.
        /// </returns>
        public static bool IsNumeric(this string value, bool allowDecimal = false)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            bool foundDecimal = false;
            foreach (char c in value.ToCharArray())
            {
                if (c == '.' && allowDecimal && !foundDecimal)
                {
                    foundDecimal = true;
                }
                else if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Converts the byte <see cref="System.Array">array</see> from the provided source <see cref="System.Text.Encoding">Encoding</see> 
        /// to the optional destination <see cref="System.Text.Encoding">Encoding</see> or <see cref="System.Text.Encoding.UTF8">UTF8</see> 
        /// as a byte <see cref="System.Array">array</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <param name="sourceEncoding">The <see cref="System.Text.Encoding">Encoding</see> scheme of the bytes provided.</param>
        /// <param name="destinationEncoding">The <see cref="System.Text.Encoding">Encoding</see> scheme of the bytes expected.</param>
        /// <returns>The resulting byte <see cref="System.Array">array</see>.</returns>
        public static byte[] Convert(this byte[] value, System.Text.Encoding sourceEncoding,
            System.Text.Encoding destinationEncoding = null)
        {
            if (value == null)
            {
                return null;
            }
            if (sourceEncoding == (destinationEncoding ?? System.Text.Encoding.UTF8))
            {
                return value;
            }
            return System.Text.Encoding.Convert(sourceEncoding,
                destinationEncoding ?? System.Text.Encoding.UTF8, value);
        }

        /// <summary>
        /// Converts the <see cref="System.Byte">byte</see> from the provided source <see cref="System.Text.Encoding">Encoding</see> 
        /// to the optional destination <see cref="System.Text.Encoding">Encoding</see> or <see cref="System.Text.Encoding.UTF8">UTF8</see> 
        /// as a byte <see cref="System.Array">array</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.Byte">byte</see> to evaluate.</param>
        /// <param name="sourceEncoding">The <see cref="System.Text.Encoding">Encoding</see> scheme of the bytes provided.</param>
        /// <param name="destinationEncoding">The <see cref="System.Text.Encoding">Encoding</see> scheme of the bytes expected.</param>
        /// <returns>The converted <see cref="System.Byte">byte</see>.</returns>
        public static byte Convert(this byte value, System.Text.Encoding sourceEncoding,
            System.Text.Encoding destinationEncoding = null)
        {
            return (new byte[] { value }).Convert(sourceEncoding, destinationEncoding)[0];
        }

        /// <summary>
        /// Converts the byte <see cref="System.Array">array</see> from the provided source <see cref="System.Text.Encoding">Encoding</see> 
        /// to the optional destination <see cref="System.Text.Encoding">Encoding</see> or <see cref="System.Text.Encoding.UTF8">UTF8</see> as string.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <param name="sourceEncoding">The <see cref="System.Text.Encoding">Encoding</see> scheme of the bytes provided.</param>
        /// <param name="destinationEncoding">The <see cref="System.Text.Encoding">Encoding</see> scheme of the bytes expected.</param>
        /// <returns>
        /// The string <see cref="System.Text.Encoding">encoded</see> as <see cref="System.Text.Encoding">destinationEncdoing</see> 
        /// or <see cref="System.Text.Encoding.UTF8">UTF8</see>.
        /// </returns>
        public static string ToEncodedString(this byte[] value, System.Text.Encoding sourceEncoding,
            System.Text.Encoding destinationEncoding = null)
        {
            if (value == null)
            {
                return null;
            }
            return (destinationEncoding ?? System.Text.Encoding.UTF8)
                .GetString(value.Convert(sourceEncoding, destinationEncoding));
        }

        /// <summary>
        /// A shortcut method to implement <see cref="System.Math.Pow(double, double)">System.Math.Pow</see> for decimal values.
        /// </summary>
        /// <param name="value">The <see cref="System.Decimal">decimal</see> value.</param>
        /// <param name="exponent">The exponent expressed as <see cref="System.Double">double</see>.</param>
        /// <returns>The resultant as <see cref="System.Decimal">decimal</see>.</returns>
        public static decimal Raise(this decimal value, double exponent)
        {
            return (decimal)System.Math.Pow((double)value, exponent);
        }

        /// <summary>
        /// quickly converts a <see cref="System.Decimal">decimal</see> to the <see cref="Round(decimal, double)">rounded</see> value.
        /// </summary>
        /// <param name="value">The <see cref="System.Decimal">decimal</see> value.</param>
        /// <returns>The value as <see cref="System.Int32">int</see>.</returns>
        public static int AsInt(this decimal value)
        {
            if (value > int.MaxValue || value < int.MinValue)
            {
                return 0;
            }
            return System.Convert.ToInt32(value.Round());
        }

        /// <summary>
        /// Rounds to the <see cref="System.Decimal">decimal</see> value at the hundredths (10 to the 2nd, or a precision of 2). 
        /// See <seealso cref="Round(decimal, double)">Round()</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Decimal">decimal</see> value.</param>
        /// <returns>The <see cref="System.Decimal">decimal</see> rounded value.</returns>
        /// <seealso cref="Round(decimal, double)"/>
        public static decimal ToTheCent(this decimal value)
        {
            return value.Round(2);
        }

        /// <summary>
        /// Rounds to the highest <see cref="System.Decimal">decimal</see> value at the hundredths (10 to the 2nd, or a precision of 2). 
        /// See <seealso cref="RoundUp(decimal, double)">RoundUp()</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Decimal">decimal</see> value.</param>
        /// <returns>The <see cref="System.Decimal">decimal</see> rounded up.</returns>
        /// <seealso cref="RoundUp(decimal, double)"/>
        public static decimal ToTheHighCent(this decimal value)
        {
            return value.RoundUp(2);
        }

        /// <summary>
        /// Rounds to the lowest <see cref="System.Decimal">decimal</see> value at the hundredths (10 to the 2nd, or a precision of 2). 
        /// See <seealso cref="RoundDown(decimal, double)">RoundDown()</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Decimal">decimal</see> value.</param>
        /// <returns>The <see cref="System.Decimal">decimal</see> rounded down.</returns>
        public static decimal ToTheLowCent(this decimal value)
        {
            return value.RoundDown(2);
        }

        /// <summary>
        /// Rounds to the <see cref="System.Decimal">decimal</see> value at the provided precision value (or 0). 
        /// If desired for currency, use <see cref="ToTheCent(decimal)">ToTheCent()</see> instead.
        /// </summary>
        /// <param name="value">The <see cref="System.Decimal">decimal</see> value.</param>
        /// <param name="precision">The <see cref="System.Double">precision</see> to apply to the decimal value.</param>
        /// <returns>The <see cref="System.Decimal">decimal</see> rounded value.</returns>
        public static decimal Round(this decimal value, double precision = 0)
        {
            return System.Math.Round(value * (decimal)(System.Math.Pow(10, precision))) / (decimal)System.Math.Pow(10, precision);
        }

        /// <summary>
        /// Rounds to the highest <see cref="System.Decimal">decimal</see> value at the provided precision value (or 0). 
        /// If desired for currency, use <see cref="ToTheHighCent(decimal)">ToTheHighCent()</see> instead.
        /// </summary>
        /// <param name="value">The <see cref="System.Decimal">decimal</see> value.</param>
        /// <param name="precision">The <see cref="System.Double">precision</see> to apply to the decimal value.</param>
        /// <returns>The <see cref="System.Decimal">decimal</see> rounded up.</returns>
        public static decimal RoundUp(this decimal value, double precision = 0)
        {
            return System.Math.Ceiling(value * (decimal)(System.Math.Pow(10, precision))) / (decimal)System.Math.Pow(10, precision);
        }

        /// <summary>
        /// Rounds to the lowest <see cref="System.Decimal">decimal</see> value at the provided precision value (or 0). 
        /// If desired for currency, use <see cref="ToTheLowCent(decimal)">ToTheLowCent()</see> instead.
        /// </summary>
        /// <param name="value">The <see cref="System.Decimal">decimal</see> value.</param>
        /// <param name="precision">The <see cref="System.Double">precision</see> to apply to the decimal value.</param>
        /// <returns>The <see cref="System.Decimal">decimal</see> rounded down.</returns>
        public static decimal RoundDown(this decimal value, double precision = 0)
        {
            return System.Math.Floor(value * (decimal)(System.Math.Pow(10, precision))) / (decimal)System.Math.Pow(10, precision);
        }

        /// <summary>
        /// Returns the Century of the <see cref="System.DateTime">System.DateTime</see> as an <see cref="System.Int32">int</see>.
        /// Valid return values are 0 to 99.
        /// </summary>
        /// <param name="value">The <see cref="System.DateTime">System.DateTime</see> value.</param>
        /// <returns>The Century as an <see cref="System.Int32">int</see>.</returns>
        public static int Century(this System.DateTime value)
        {
            return System.Convert.ToInt32(System.Math.Floor((double)value.Year / 100));
        }

        /// <summary>
        /// Returns the last 2 digits of the year of the <see cref="System.DateTime">System.DateTime</see> as an 
        /// <see cref="System.Int32">int</see>.
        /// Valid return values are 0 to 99.
        /// </summary>
        /// <param name="value">The <see cref="System.DateTime">System.DateTime</see> value.</param>
        /// <returns>The Year as a 2 digit (or less) <see cref="System.Int32">integer</see>.</returns>
        public static int TwoDigitYear(this System.DateTime value)
        {
            return value.Year - (value.Century() * 100);
        }

        /// <summary>
        /// Returns a sentence (or a single word) as pascal-cased ("Mr. George Willington Gracey Esq." is an example of pascal casing).
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> value to evaluate.</param>
        /// <returns>The <see cref="System.String">string</see> value pascal-cased.</returns>
        public static string PascalCased(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            string[] words = value.Trim().Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string word in words)
            {
                sb.Append($"{word.Capitalize()} ");
            }
            return sb.ToString().Trim();
        }

        /// <summary>
        /// Shifts any first character of the string to UpperInvariant and any remaining characters ToLowerInvariant.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> value to evaluate.</param>
        /// <returns>The reformatted <see cref="System.String">string</see>.</returns>
        public static string Capitalize(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            string ret = value.Trim();
            if (ret.Length < 2)
            {
                return ret.ToUpperInvariant();
            }
            return $"{ret.Substring(0, 1).ToUpperInvariant()}{ret.Substring(startIndex: 1).ToLowerInvariant()}";
        }

        /// <summary>
        /// Converts an <see cref="System.Object">object</see> to an <see cref="System.Array">array</see> of bytes.
        /// </summary>
        /// <param name="value">The <see cref="System.Object">object</see> to evaluate.</param>
        /// <param name="encoding">The <see cref="System.Text.Encoding">Encoding</see> scheme of the bytes expected or 
        /// <see cref="System.Text.Encoding.UTF8">UTF8</see>.</param>
        /// <returns>The resulting <see cref="System.Array">array</see> of bytes.</returns>
        public static byte[] ToByteArray(this object value, System.Text.Encoding encoding = null)
        {
            if (value == null)
            {
                return new byte[] { };
            }
            if (typeof(string) == value.GetType())
            {
                return (encoding ?? System.Text.Encoding.UTF8).GetBytes(value.ToString());
            }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf =
                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var ms = new System.IO.MemoryStream())
            {
                bf.Serialize(ms, value);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Converts an <see cref="System.Array">array</see> of bytes to an <see cref="System.Object">object</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The resulting <see cref="System.Object">object</see>.</returns>
        public static object ToObject(this byte[] value)
        {
            if (value == null || value.Length < 1)
            {
                return null;
            }
            using (var ms = new System.IO.MemoryStream())
            {
                ms.Write(value, 0, value.Length);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return (new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter())
                    .Deserialize(ms);
            }
        }

        /// <summary>
        /// Converts an <see cref="System.Array">array</see> of bytes to the specified <see cref="System.Type">type</see>.
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Type">type</see> of return object.</typeparam>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The object as <see cref="System.Type">T</see>.</returns>
        public static T ToObject<T>(this byte[] value)
        {
            object ret = value.ToObject();
            if (ret == null)
            {
                return default;
            }
            return (T)ret;
        }

        /// <summary>
        /// Converts the string to a <see cref="bool">bool</see> or null.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> value to evaluate.</param>
        /// <returns><see cref="bool">Boolean</see> or null.</returns>
        public static bool? ToBool(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            if (value.Trim().Length < 4)
            {
                string ret = value.Trim().Substring(0, 1).ToUpperInvariant();
                if (ret == "T" || ret == "Y" || ret == "S" || ret == "D" || ret == "O")
                {
                    return true;
                }
                else if (ret == "F" || ret == "N")
                {
                    return false;
                }
                else if (ret == "1" || ret == "0")
                {
                    return ret == "1";
                }
                else
                {
                    return null;
                }
            }
            if (bool.TryParse(value.Trim(), out bool bret))
            {
                return bret;
            }
            return null;
        }

        /// <summary>
        /// Returns the formatted <see cref="System.String">string</see> representation of the <see cref="System.Boolean">boolean</see> 
        /// value provided using the <see cref="BooleanStringRepresentation">format</see> provided or empty ("").
        /// </summary>
        /// <param name="value">The <see cref="System.Boolean">boolean</see> value to evaluate.</param>
        /// <param name="format">
        /// The <see cref="BooleanStringRepresentation">format</see> identifying how the <see cref="System.Boolean">boolean</see> 
        /// value will be represented as a <see cref="System.String">string</see>.
        /// </param>
        /// <returns>
        /// The formatted <see cref="System.String">string</see> representation of the <see cref="System.Boolean">boolean</see> value.
        /// </returns>
        public static string ToString(this bool value, BooleanStringRepresentation format)
        {
            switch (format)
            {
                case BooleanStringRepresentation.One_Zero:
                    return value ? "1" : "0";
                case BooleanStringRepresentation.True_False:
                    return value.ToString().PascalCased();
                case BooleanStringRepresentation.true_false:
                    return value.ToString().ToLowerInvariant();
                case BooleanStringRepresentation.Yes_No:
                case BooleanStringRepresentation.yes_no:
                    string ret = value ? 
                        IO.FileParser.Properties.Resources.ResourceManager.GetString("Yes") 
                        : IO.FileParser.Properties.Resources.ResourceManager.GetString("No");
                    return format == BooleanStringRepresentation.yes_no ? ret.ToLowerInvariant() : ret;
                case BooleanStringRepresentation.Zero_Minus1:
                    return value ? "0" : "-1";
            }
            return "";
        }

        /// <summary>
        /// Converts the string to a <see cref="System.DateTime">System.DateTime</see> or null.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> value to evaluate.</param>
        /// <param name="yearFirst">If <see cref="bool">true</see> it assumes the year is the first part of the string. 
        /// Default is <see cref="bool">false</see>.
        /// Note: If the <see cref="System.String">string</see> contains a parsable date character ( / or - ) it will attempt to use 
        /// <see cref="System.DateTime.TryParse(string, out System.DateTime)">TryParse</see> and return any successful result. 
        /// If <see cref="System.DateTime.TryParse(string, out System.DateTime)">TryParse</see> fails it proceeds on to 
        /// <see cref="System.String">string</see> parsing.
        /// </param>
        /// <returns><see cref="System.DateTime">System.DateTime</see> or null.</returns>
        public static System.DateTime? ToDateTime(this string value, bool yearFirst = false)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            if ((value.Contains("/") || value.Contains("-")) && (value.Length > 5))
            {
                if (System.DateTime.TryParse(value, out System.DateTime ret))
                {
                    return ret;
                }
            }
            string val = value.NumbersOnly();
            if (val.Length != 6 && val.Length != 8)
            {
                return null;
            }
            if (!int.TryParse(val.Substring(yearFirst ? 0 : 4, val.Length == 8 ? 4 : 2), out int year)
                || year < System.DateTime.MinValue.Year || year > System.DateTime.MaxValue.Year)
            {
                return null;
            }
            if (!int.TryParse(val.Substring(yearFirst ? (val.Length == 8 ? 4 : 2) : 0, 2), out int month)
                || month < 1 || month > 12)
            {
                return null;
            }
            if (!int.TryParse(val.Substring(yearFirst ? (val.Length == 8 ? 6 : 4) : 2, 2), out int day)
                || day < 1 || day > System.DateTime.DaysInMonth(year, month))
            {
                return null;
            }
            if (year < 100)
            {
                int century = System.DateTime.Today.Century() * 100;
                if (year > (System.DateTime.Today.Year - century))
                {
                    //assume previous century
                    century -= 100;
                }
                year += century;
            }
            return new System.DateTime(year, month, day);
        }

        /// <summary>
        /// Converts an <see cref="System.UInt32">unsigned integer</see> to a <see cref="System.Int32">signed integer</see>.
        /// If the <see cref="System.UInt32">unsigned integer</see> is greater than <see cref="System.Int32.MaxValue">int.MaxValue</see> 
        /// it will return a negative <see cref="System.Int32">integer</see> starting at -1 for each value greater than 
        /// <see cref="System.Int32.MaxValue">int.MaxValue</see>.
        /// I.e., int.MaxValue + 1 returns -1, int.MaxValue + 2 returns -2, int.MaxValue + 3 returns -3, and so forth.
        /// <code>
        /// public static int ToInt(this uint value)
        /// {
        ///     if (value > int.MaxValue)
        ///     {
        ///         return <seealso cref="System.Convert.ToInt32(int)">System.Convert.ToInt32</seealso>((value - int.MaxValue) * -1);
        ///     }
        ///     return <seealso cref="System.Convert.ToInt32(int)">System.Convert.ToInt32</seealso>(value);
        /// }
        /// </code>
        /// </summary>
        /// <param name="value">The <see cref="System.UInt32">unsigned integer</see> to evaluate.</param>
        /// <param name="throwOnInvalid">If <see cref="bool">true</see> it will throw an 
        /// <see cref="System.InvalidCastException">InvalidCastException</see> if value exceeds int.MaxValue. 
        /// Default is <see cref="bool">false</see>.</param>
        /// <returns>The resulting <see cref="System.Int32">signed integer</see>.</returns>
        public static int ToInt(this uint value, bool throwOnInvalid = false)
        {
            if (value > int.MaxValue)
            {
                if (throwOnInvalid)
                {
                    throw new System.InvalidCastException(
                        $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} {value}");
                }
                return System.Convert.ToInt32((value - int.MaxValue) * -1);
            }
            return System.Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts a <see cref="System.Int32">signed integer</see> to an <see cref="System.UInt32">unsigned integer</see>.
        /// If the <see cref="System.Int32">signed integer</see> is negative then it will return
        /// a value greater than <see cref="System.Int32.MaxValue">int.MaxValue</see> starting at +1 for each value below 0.
        /// I.e., -1 returns int.MaxValue + 1, -2 returns int.MaxValue + 2, -3 returns int.MaxValue + 3, and so forth.
        /// <code>
        /// public static uint ToUInt(this int value)
        /// {
        ///     return value <![CDATA[<]]> 0 
        ///         ? <seealso cref="System.Convert.ToUInt32(int)">System.Convert.ToUInt32</seealso>(int.MaxValue + System.Math.Abs(value)) 
        ///         : <seealso cref="System.Convert.ToUInt32(int)">System.Convert.ToUInt32</seealso>(value);
        /// }
        /// </code>
        /// </summary>
        /// <param name="value">The <see cref="System.Int32">signed integer</see> to evaluate.</param>
        /// <param name="throwOnInvalid">If <see cref="bool">true</see> it will throw an 
        /// <see cref="System.InvalidCastException">InvalidCastException</see> if value is negative. 
        /// Default is <see cref="bool">false</see>.</param>
        /// <returns>The resulting <see cref="System.UInt32">unsigned integer</see>.</returns>
        public static uint ToUInt(this int value, bool throwOnInvalid = false)
        {
            if (value < 0)
            {
                if (throwOnInvalid)
                {
                    throw new System.InvalidCastException(
                        $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} {value}");
                }
                return System.Convert.ToUInt32(int.MaxValue + System.Math.Abs(value));
            }
            return System.Convert.ToUInt32(value);
        }

        /// <summary>
        /// Returns the default <see cref="System.Threading.Tasks.TaskScheduler">TaskScheduler</see> of the 
        /// <see cref="System.Threading.Tasks.TaskFactory"> TaskFactory's</see> 
        /// <see cref="System.Threading.Tasks.TaskScheduler.MaximumConcurrencyLevel">MaximumConcurrencyLevel</see> 
        /// as an <see cref="System.UInt32">unsigned integer</see>. If any values are null, it will return 0.
        /// <code>
        /// public static uint MaximumConcurrency(this System.Threading.Tasks.TaskFactory value)
        /// {
        ///     return (value?.<see cref="System.Threading.Tasks.TaskFactory.Scheduler">Scheduler</see>?
        ///         .MaximumConcurrencyLevel ?? 0).<see cref="ToUInt(int, bool)">ToUInt()</see>;
        /// }
        /// </code>
        /// </summary>
        /// <param name="value">The <see cref="System.Threading.Tasks.TaskFactory">TaskFactory</see> to evaluate.</param>
        /// <returns>
        /// The resulting <see cref="System.Int32">signed integer</see> as an <see cref="System.UInt32">unsigned integer</see> or 0.
        /// </returns>
        public static uint MaximumConcurrency(this System.Threading.Tasks.TaskFactory value)
        {
            return (value?.Scheduler?.MaximumConcurrencyLevel ?? 0).ToUInt();
        }

        /// <summary>
        /// Evaulates an <see cref="System.Object">object</see> for null and returns default if null (for <see cref="System.Nullable">nullable</see> 
        /// <see cref="System.Type">types</see> this could be null or a default value). For all other <see cref="System.Type">types</see> it returns 
        /// the default of the <see cref="System.Type">type (T)</see> provided.
        /// If not null, will attempt a <see href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions">
        /// direct cast</see> to <see cref="System.Type">type (T)</see>.
        /// <code>
        /// public static T CastAs<![CDATA[<T>]]>(this object value)
        /// {
        ///     if (value == null)
        ///     {
        ///         return default;
        ///     }
        ///     return (T)value;
        /// }
        /// </code>
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Type">type</see> of return object.</typeparam>
        /// <param name="value">The <see cref="System.Object">object</see> to cast.</param>
        /// <returns>The value as <see cref="System.Type">T</see> or default.</returns>
        public static T CastAs<T>(this object value)
        {
            if (value == null)
            {
                return default;
            }
            return (T)value;
        }

        /// <summary>
        /// If possible, positions the pointer to the beginning of the stream without throwing an error.
        /// </summary>
        /// <param name="value">The <see cref="System.IO.Stream">System.IO.Stream</see>.</param>
        public static void GotoStart(this System.IO.Stream value)
        {
            if (value.CanSeek)
            {
                value.Seek(0, System.IO.SeekOrigin.Begin);
            }
        }

        /// <summary>
        /// Divides the long <see cref="System.String">string</see> into an <see cref="System.Array">array</see> of 
        /// <see cref="System.String">strings</see> of a length of groupOf.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> to evaluate.</param>
        /// <param name="groupsOf">The <see cref="System.UInt32">number</see> of <see cref="System.Char">characters</see> 
        /// for each <see cref="System.String">string</see> in the <see cref="System.Array">array</see>.</param>
        /// <returns>An <see cref="System.Array">array</see> of <see cref="System.String">strings</see> of a length of groupOf.</returns>
        public static string[] DivideInto(this string value, uint groupsOf)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= groupsOf)
            {
                return new string[] { value };
            }
            char[] chars = value.ToCharArray();
            System.Collections.Generic.List<string> res = new System.Collections.Generic.List<string>();
            int take = groupsOf.ToInt();
            int skip = take;
            res.Add(chars.Take(take).AsString());
            for (int i = 0; i < System.Math.Ceiling((decimal)chars.Length / take); i++)
            {
                res.Add(chars.Skip(skip).Take(take).AsString());
                skip += take;
            }
            return res.ToArray();
        }

        /// <summary>
        /// Restores an <see cref="System.Array">array</see> of <see cref="System.Char">characters</see> to a <see cref="System.String">string</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of <see cref="System.Char">characters</see> to evaluate.</param>
        /// <returns>The resulting <see cref="System.String">string</see>.</returns>
        public static string AsString(this System.Collections.Generic.IEnumerable<char> value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (char c in value)
            {
                sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns <see cref="bool">true</see> if the <see cref="byte">byte</see> has value > 0 in the second nibble.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="byte">byte</see> to evaluate.</param>
        /// <returns>The <see cref="bool">Boolean</see> result or false.</returns>
        public static bool FromPackedBool(this byte value)
        {
            if ((new byte[] { value }).FromPackedDouble(0) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the <see cref="System.Int16">short</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// A <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from a null or empty 
        /// <see cref="System.Array">array</see>.
        /// </param>
        /// <returns>The <see cref="System.Int16">System.Int16</see> resultant.</returns>
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception>
        public static short FromPackedShort(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            return System.Convert.ToInt16(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.Int32">int</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// A <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from a null or empty 
        /// <see cref="System.Array">array</see>.
        /// </param>
        /// <returns>The <see cref="System.Int32">System.Int32</see> resultant.</returns>
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception>
        public static int FromPackedInt(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            return System.Convert.ToInt32(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.Int64">long</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// A <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from a null or empty 
        /// <see cref="System.Array">array</see>.
        /// </param>
        /// <returns>The <see cref="System.Int64">System.Int64</see> resultant.</returns>
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception>
        public static long FromPackedLong(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            return System.Convert.ToInt64(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.UInt16">unsigned short</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// A <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from a null or empty 
        /// <see cref="System.Array">array</see>.
        /// </param>
        /// <returns>The <see cref="System.UInt16">System.UInt16</see> resultant.</returns>
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception>
        public static ushort FromPackedUShort(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            return System.Convert.ToUInt16(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.UInt32">unsigned int</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// A <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from a null or empty 
        /// <see cref="System.Array">array</see>.
        /// </param>
        /// <returns>The <see cref="System.UInt32">System.UInt32</see> resultant.</returns>
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception>
        public static uint FromPackedUInt(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            return System.Convert.ToUInt32(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.UInt64">unsigned long</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// A <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from a null or empty 
        /// <see cref="System.Array">array</see>.
        /// </param>
        /// <returns>The <see cref="System.UInt64">System.UInt64</see> resultant.</returns>
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception>
        public static ulong FromPackedULong(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            return System.Convert.ToUInt64(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.Double">double</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// A <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from a null or empty 
        /// <see cref="System.Array">array</see>.
        /// </param>
        /// <param name="precision">The precision of the expected resultant as a <see cref="System.Int16">short</see> integer. Default is 0. 
        /// The limitation of short is for logical reasons. 
        /// </param>
        /// <returns>The <see cref="System.Double">System.Double</see> resultant.</returns>
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception>
        public static double FromPackedDouble(this byte[] value, short precision = 0)
        {
            if (value == null || value.Length < 1)
            {
                throw new System.InvalidCastException(
                    $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} {value?.Length.ToString() ?? "null"}");
            }
            double power = System.Math.Pow(10, precision);
            string hex = System.BitConverter.ToString(value).Remove("-");
            byte[] bytes = Enumerable.Range(0, hex.Length)
                .Select(x => System.Convert.ToByte($"0{hex.Substring(x, 1)}", 16))
                .ToArray();
            long place = 1;
            double ret = 0;
            for (int i = bytes.Length - 2; i > -1; i--)
            {
                ret += (bytes[i] * place);
                place *= 10;
            }
            ret /= power;
            return (bytes.Last() & (1 << 7)) != 0 ? ret * -1 : ret;
        }

        /// <summary>
        /// Returns the <see cref="System.Single">float</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// A <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from a null or empty 
        /// <see cref="System.Array">array</see>.
        /// </param>
        /// <param name="precision">The precision of the expected resultant as a <see cref="System.Int16">short</see> integer. Default is 0. 
        /// The limitation of short is for logical reasons. 
        /// </param>
        /// <returns>The <see cref="System.Single">float</see> resultant.</returns>
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception>
        public static float FromPackedFloat(this byte[] value, short precision = 0)
        {
            return System.Convert.ToSingle(value.FromPackedDouble(precision));
        }

        /// <summary>
        /// Returns the <see cref="System.DateTime">DateTime</see><see cref="System.DateTime.Date">.Date</see> value of the packed number or null.
        /// Note: The byte <see cref="System.Array">array</see> must be 2 or 4 bytes long. On 2 byte arrays, future years are considered to be
        /// from the previous century.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate. 
        /// Length must be 2 (Short Date Format) or 4 (Long Date Format).
        /// </param>
        /// <returns>The <see cref="System.DateTime">System.DateTime</see> resultant or null.</returns>
        public static System.DateTime? FromPackedDate(this byte[] value)
        {
            if (value == null)
            {
                throw new System.InvalidCastException(
                    $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} null");
            }
            int year, month, day;
            switch (value.Length)
            {
                case 2:
                    year = (int)(new byte[] { (byte)(value[1] - ((value[0] & (1 << 7)) != 0 ? 0x80 : 0x00)) }).FromComp3();
                    year += ((System.DateTime.Today.Century() - (year > System.DateTime.Today.TwoDigitYear() ? 1 : 0)) * 100);
                    month = value[0].Bit(BitOrder.Bit1) ? 8 : 0;
                    month += value[0].Bit(BitOrder.Bit2) ? 4 : 0;
                    month += value[0].Bit(BitOrder.Bit3) ? 2 : 0;
                    month += value[0].Bit(BitOrder.Bit4) ? 1 : 0;
                    day = value[1].Bit(BitOrder.Bit8) ? 1 : 0;
                    day += value[0].Bit(BitOrder.Bit5) ? 16 : 0;
                    day += value[0].Bit(BitOrder.Bit6) ? 8 : 0;
                    day += value[0].Bit(BitOrder.Bit7) ? 4 : 0;
                    day += value[0].Bit(BitOrder.Bit8) ? 2 : 0;
                    break;
                case 4:
                    year = System.Convert.ToInt32((new byte[] { value[0], value[1] }).FromComp3());
                    month = System.Convert.ToInt32((new byte[] { value[2] }).FromComp3());
                    day = System.Convert.ToInt32((new byte[] { value[3] }).FromComp3());
                    break;
                default:
                    throw new System.InvalidCastException(
                        $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} {value.Length}");
            }
            if (year < System.DateTime.MinValue.Year || year > System.DateTime.MaxValue.Year)
            {
                return null;
            }
            if (month < 1 || month > 12)
            {
                return null;
            }
            if (day < 1 || day > System.DateTime.DaysInMonth(year, month))
            {
                return null;
            }
            return new System.DateTime(year, month, day);
        }

        /// <summary>
        /// Used internally to convert nibbles to digits and return a number without evaluating 0th nibble for flags,
        /// a.k.a., every nibble is included in the result. Critical to PackedDate conversion.
        /// Used in <see cref="FromPackedDate(byte[])">byte[].FromPackedDate()</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The resulting <see cref="System.Double">double</see> value.</returns>
        private static double FromComp3(this byte[] value)
        {
            if (value.Length < 1)
            {
                throw new System.InvalidCastException($"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} { value.Length }");
            }
            string hex = System.BitConverter.ToString(value).Remove("-");
            var bytes = Enumerable.Range(0, hex.Length)
                .Select(x => System.Convert.ToByte($"0{hex.Substring(x, 1)}", 16))
                .ToArray();
            long place = 1;
            double ret = 0;
            for (int i = bytes.Length - 1; i > -1; i--)
            {
                ret += (double)(bytes[i] * place);
                place *= 10;
            }
            return ret;
        }
        private static byte[] ToComp3(this double value)
        {
            System.Collections.Generic.List<byte> bytes =
                value.ToString().Remove(".").ToCharArray().Select(c => (byte)System.Convert.ToInt16(c.ToString())).ToList();
            if (bytes.Count % 2 != 0)
            {
                bytes.Insert(0, 0);
            }
            System.Collections.Generic.List<byte> ret = new System.Collections.Generic.List<byte>();
            for (int i = 0; i < bytes.Count; i += 2)
            {
                ret.Add(((byte)bytes[i]).AddComp3((byte)bytes[i + 1]));
            }
            return ret.ToArray();
        }
        private static byte[] ToComp3(this int value)
        {
            return ((double)value).ToComp3();
        }

        /// <summary>
        /// Returns the <see cref="System.Decimal">decimal</see> value of the packed number.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from null or empty byte 
        /// <see cref="System.Array">array</see>.
        /// </param>
        /// <param name="precision">The precision of the expected resultant as a <see cref="System.Int16">short</see> integer. Default is 0. 
        /// The limitation of short is for logical reasons. 
        /// </param>
        /// <returns>The resultant as <see cref="System.Decimal">decimal</see>.</returns>
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception>
        public static decimal FromPackedDecimal(this byte[] value, short precision = 0)
        {
            return System.Convert.ToDecimal(value.FromPackedDouble(precision));
        }

        /// <summary>
        /// Returns the <see cref="System.Array">array</see> of bytes in a COMP3 (IBMPacked) Format.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Int32">integer</see> to convert.</param>
        /// <returns>The resultant as an <see cref="System.Array">array</see> of bytes.</returns>
        public static byte[] PackComp3(this int value)
        {
            return ((double)value).PackComp3();
        }

        /// <summary>
        /// Returns the <see cref="System.Array">array</see> of bytes in a COMP3 (IBMPacked Decimal) Format.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Decimal">decimal</see> to convert.</param>
        /// <param name="precision">The precision of the converted resultant as a <see cref="short">short</see>. Default is 0. 
        /// The limitation of short is for logical reasons. 
        /// </param>
        /// <returns>The resultant as an <see cref="System.Array">array</see> of bytes.</returns>
        public static byte[] PackComp3(this decimal value, short precision = 0)
        {
            return ((double)value).PackComp3(precision);
        }

        /// <summary>
        /// Returns the <see cref="System.Array">array</see> of bytes in a COMP3 (IBMPacked Decimal) Format.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Double">double</see> to convert.</param>
        /// <param name="precision">The precision of the converted resultant as a <see cref="short">short</see>. Default is 0. 
        /// The limitation of short is for logical reasons. 
        /// </param>
        /// <returns>The resultant as an <see cref="System.Array">array</see> of bytes.</returns>
        public static byte[] PackComp3(this double value, short precision = 0)
        {
            char[] chars = value.ToString($"n{precision}").Remove(".").Remove(",").ToCharArray();
            System.Collections.Generic.List<byte> bytes = new System.Collections.Generic.List<byte>();
            for (int i = 0; i < chars.Length; i++)
            {
                if (!byte.TryParse($"{chars[i]}", out byte b))
                {
                    b = 0;
                }
                bytes.Add(b);
            }
            if (value < 0)
            {
                bytes.Add(0x30);//00110000 (bit3 for signed, bit4 for negative - converted to bits 7, 8 in AddComp3)
            }
            else
            {
                bytes.Add(0x00);
            }
            if (bytes.Count % 2 != 0)
            {
                bytes.Insert(0, 0);
            }
            System.Collections.Generic.List<byte> ret = new System.Collections.Generic.List<byte>();
            for (int i = 0; i < bytes.Count; i += 2)
            {
                ret.Add(bytes[i].AddComp3(bytes[i + 1]));
            }
            return ret.ToArray();
        }
        internal static byte AddComp3(this byte value, byte byte2)
        {
            return (byte)((value * 16) + byte2);
        }

        /// <summary>
        /// Returns the <see cref="System.Array">array</see> of bytes in a COMP3 (IBMPacked) Format.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.DateTime">System.DateTime</see> value.</param>
        /// <param name="shortFormat">
        /// If <see cref="System.Boolean">true</see>, will return a 2 <see cref="System.Byte">byte</see> 
        /// <see cref="System.Array">array</see> representing the date. If <see cref="System.Boolean">false</see>, 
        /// will return a 4 <see cref="System.Byte">byte</see> <see cref="System.Array">array</see> representing the date.
        /// Default is <see cref="System.Boolean">false</see>.
        /// </param>
        /// <returns>The resultant as an <see cref="System.Array">array</see> of bytes.</returns>
        public static byte[] PackComp3(this System.DateTime value, bool shortFormat = false)
        {
            if (shortFormat)
            {
                byte mpart = value.Month.ToComp3().FirstOrDefault().AddComp3(0);
                byte ypart = ((byte)(value.TwoDigitYear().ToComp3().FirstOrDefault()));
                int dpart = value.Day;
                if (dpart > 16)
                {
                    mpart += 8;
                    dpart -= 16;
                }
                if (dpart > 8)
                {
                    mpart += 4;
                    dpart -= 8;
                }
                if (dpart > 4)
                {
                    mpart += 2;
                    dpart -= 4;
                }
                if (dpart > 2)
                {
                    dpart -= 2;
                    mpart += 1;
                }
                if (dpart > 0)
                {
                    ypart += 128;
                }
                return new byte[] { mpart, ypart };
            }
            else
            {
                System.Collections.Generic.List<byte> ret = value.Year.ToComp3().Prefix(2).ToList();
                if (ret.Count > 2)
                {
                    throw new System.InvalidOperationException(
                        $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} {value.Year}");
                }
                ret.AddRange(value.Month.ToComp3());
                ret.AddRange(value.Day.ToComp3());
                if (ret.Count > 4)
                {
                    throw new System.InvalidOperationException(
                        $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} {value}");
                }
                return ret.ToArray();
            }
        }

        /// <summary>
        /// Returns <see cref="System.Boolean">true</see> if the requested bit (0 (most significant) through 7 (least significant)) is 1.
        /// Returns <see cref="System.Boolean">false</see> if the requested bit (0 (most significant) through 7 (least significant)) is 0.
        /// </summary>
        /// <param name="value">The <see cref="System.Byte">byte</see> to evaluate.</param>
        /// <param name="position">The bit position to return as a <see cref="System.Byte">byte</see>.
        /// If an invalid value is provided an <exception cref="System.InvalidOperationException">InvalidOperationException</exception> is thrown. 
        /// Valid values are 0 (most significant) through 7 (least significant).
        /// </param>
        /// <returns>
        /// Returns <see cref="System.Boolean">true</see> if the requested bit (0 (most significant) through 7 (least significant)) is 1.
        /// Returns <see cref="System.Boolean">false</see> if the requested bit (0 (most significant) through 7 (least significant)) is 0.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the requested bit is an invalid value. Valid values are 0 (most significant) through 7 (least significant).
        /// </exception>
        public static bool Bit(this byte value, byte position)
        {
            if (position > 7)
            {
                throw new System.InvalidOperationException(
                    $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} bit {position}");
            }
            return (value & (1 << position)) != 0;
        }

        /// <summary>
        /// Returns <see cref="System.Boolean">true</see> if the requested bit (Bit1 (most significant) through Bit8 (least significant)) is 1.
        /// Returns <see cref="System.Boolean">false</see> if the requested bit (Bit1 (most significant) through Bit8 (least significant)) is 0.
        /// </summary>
        /// <param name="value">The <see cref="System.Byte">byte</see> to evaluate.</param>
        /// <param name="bitOrder">The bit <see cref="BitOrder">position</see> to return.
        /// </param>
        /// <returns>
        /// Returns <see cref="System.Boolean">true</see> if the requested bit (Bit1 (most significant) through Bit8 (least significant)) is 1.
        /// Returns <see cref="System.Boolean">false</see> if the requested bit (Bit1 (most significant) through Bit8 (least significant)) is 0.
        /// </returns>
        public static bool Bit(this byte value, BitOrder bitOrder)
        {
            return value.Bit((byte)bitOrder);
        }

        /// <summary>
        /// Sets the specified bit in the provided <see cref="System.Byte">byte</see> to high or low.
        /// </summary>
        /// <param name="value">The <see cref="System.Byte">byte</see> being edited and returned.</param>
        /// <param name="bit">The bit <see cref="System.Byte">position</see> to return.
        /// If an invalid value is provided an <exception cref="System.InvalidOperationException">InvalidOperationException</exception> is thrown. 
        /// Valid values are 0 (most significant) through 7 (least significant).
        /// </param>
        /// <param name="high">If <see cref="System.Boolean">true</see> sets the bit to 1 at the position specified, else
        /// sets the bit to 0.</param>
        /// <returns>The <see cref="System.Byte">byte</see> being edited.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the requested bit is an invalid value. Valid values are 0 (most significant) through 7 (least significant).
        /// </exception>
        public static byte SetBit(this byte value, byte bit, bool high)
        {
            if (value.Bit(bit) == high)
            {
                return value;
            }
            switch (bit)
            {
                case 0:
                    value += (byte)(high ? 128 : -128);
                    break;
                case 1:
                    value += (byte)(high ? 64 : -64);
                    break;
                case 2:
                    value += (byte)(high ? 32 : -32);
                    break;
                case 3:
                    value += (byte)(high ? 16 : -16);
                    break;
                case 4:
                    value += (byte)(high ? 8 : -8);
                    break;
                case 5:
                    value += (byte)(high ? 4 : -4);
                    break;
                case 6:
                    value += (byte)(high ? 2 : -2);
                    break;
                case 7:
                    value += (byte)(high ? 1 : -1);
                    break;
            }
            return value;
        }

        /// <summary>
        /// Sets the specified bit in the provided <see cref="System.Byte">byte</see> to high or low.
        /// </summary>
        /// <param name="value">The <see cref="System.Byte">byte</see> being edited and returned.</param>
        /// <param name="bitOrder">The bit <see cref="BitOrder">position</see> to set.
        /// </param>
        /// <param name="high">If <see cref="System.Boolean">true</see> sets the bit to 1 at the position specified, else
        /// sets the bit to 0.</param>
        /// <returns>The <see cref="System.Byte">byte</see> being edited.</returns>
        public static byte SetBit(this byte value, BitOrder bitOrder, bool high)
        {
            return value.SetBit((byte)bitOrder, high);
        }

        /// <summary>
        /// Returns the numeric value of the bit in its position with the <see cref="System.Byte">byte</see> 
        /// if the bit is high or 0 if the bit is low.
        /// <code>
        /// byte value = 16;
        /// value.BitValue(3) = 16; // Since bit 3 is high
        /// value = 8;
        /// value.BitValue(3) = 0; // Since bit 3 is now low
        /// </code>
        /// </summary>
        /// <param name="value">The <see cref="System.Byte">byte</see> to evaluate.</param>
        /// <param name="bit">The bit <see cref="System.Byte">position</see> to return.
        /// If an invalid value is provided an <exception cref="System.InvalidOperationException">InvalidOperationException</exception> is thrown. 
        /// Valid values are 0 (most significant) through 7 (least significant).
        /// </param>
        /// <returns>The numeric value of the bit if high or 0 if low as a <see cref="System.Byte">byte</see>.</returns>
        public static byte BitValue(this byte value, byte bit)
        {
            if (!value.Bit(bit))
            {
                return 0;
            }
            return (byte)System.Math.Pow(2, System.Math.Abs(bit - 7));
        }

        /// <summary>
        /// Returns the numeric value of the bit in its position with the <see cref="System.Byte">byte</see> 
        /// if the bit is high or 0 if the bit is low.
        /// <code>
        /// byte value = 16;
        /// value.BitValue(3) = 16; // Since bit 3 is high
        /// value = 8;
        /// value.BitValue(3) = 0; // Since bit 3 is now low
        /// </code>
        /// </summary>
        /// <param name="value">The <see cref="System.Byte">byte</see> to evaluate.</param>
        /// <param name="bitOrder">The bit <see cref="BitOrder">position</see> to evaluate.
        /// </param>
        /// <returns>The numeric value of the bit if high or 0 if low as a <see cref="System.Byte">byte</see>.</returns>
        public static byte BitValue(this byte value, BitOrder bitOrder)
        {
            return value.BitValue((byte)bitOrder);
        }

        /// <summary>
        /// Similar to <see cref="System.String.PadLeft(int, char)">string.PadLeft</see>, this inserts the 
        /// <see cref="System.Byte">defaultByte</see> for the expected <see cref="System.UInt32">count</see> 
        /// at the beginning of the <see cref="System.Array">array</see> or, if the 
        /// <see cref="System.Array">array</see> length is equal to or greater than the expected 
        /// <see cref="System.UInt32">count</see>, does nothing. Will not throw an 
        /// <see cref="System.Exception">Exception</see> if the <see cref="System.Array">array</see> 
        /// exceeds the expected <see cref="System.UInt32">count</see>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="System.Array">array</see> of <see cref="System.Byte">bytes</see> to evaluate.
        /// </param>
        /// <param name="count">
        /// The minimum <see cref="System.UInt32">length</see> of the <see cref="System.Array">array</see> returned.
        /// </param>
        /// <param name="defaultByte">
        /// The "filler" <see cref="System.Byte">byte</see> to use for padding.
        /// The default value is 0.
        /// </param>
        /// <returns>
        /// An <see cref="System.Array">array</see> of <see cref="System.Byte">bytes</see> of a minimum length
        /// equal to <see cref="System.UInt32">count</see>.
        /// </returns>
        public static byte[] Prefix(this byte[] value, uint count, byte defaultByte = 0)
        {
            if (value == null)
            {
                defaultByte.NewArray(count.ToInt());
            }
            if (value.Length >= count)
            {
                return value;
            }
            System.Collections.Generic.List<byte> ret = value.ToList();
            while (ret.Count < count)
            {
                ret.Insert(0, defaultByte);
            }
            return ret.ToArray();
        }

        /// <summary>
        /// Similar to <see cref="System.String.PadRight(int, char)">string.PadRight</see>, this appends the 
        /// <see cref="System.Byte">defaultByte</see> for the expected <see cref="System.UInt32">count</see> 
        /// at the end of the <see cref="System.Array">array</see> or, if the 
        /// <see cref="System.Array">array</see> length is equal to or greater than the expected 
        /// <see cref="System.UInt32">count</see>, does nothing. Will not throw an 
        /// <see cref="System.Exception">Exception</see> if the <see cref="System.Array">array</see> 
        /// exceeds the expected <see cref="System.UInt32">count</see>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="System.Array">array</see> of <see cref="System.Byte">bytes</see> to evaluate.
        /// </param>
        /// <param name="count">
        /// The minimum <see cref="System.UInt32">length</see> of the <see cref="System.Array">array</see> returned.
        /// </param>
        /// <param name="defaultByte">
        /// The "filler" <see cref="System.Byte">byte</see> to use for padding.
        /// The default value is 0.
        /// </param>
        /// <returns>
        /// An <see cref="System.Array">array</see> of <see cref="System.Byte">bytes</see> of a minimum length
        /// equal to <see cref="System.UInt32">count</see>.
        /// </returns>
        public static byte[] Suffix(this byte[] value, uint count, byte defaultByte = 0)
        {
            if (value == null)
            {
                defaultByte.NewArray(count.ToInt());
            }
            if (value.Length >= count)
            {
                return value;
            }
            System.Collections.Generic.List<byte> ret = value.ToList();
            while (ret.Count < count)
            {
                ret.Add(defaultByte);
            }
            return ret.ToArray();
        }

        /// <summary>
        /// Creates a new <see cref="System.Array">array</see> of the specified <see cref="System.Type">Type</see> 
        /// and value of the length provided (or 1).
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Type">type</see> in the <see cref="System.Array">array</see>.</typeparam>
        /// <param name="value">The object to be utilized for instantiation.</param>
        /// <param name="length">The <see cref="System.Int32">length</see> or the <see cref="System.Array">array</see> or 1.</param>
        /// <returns>A new <see cref="System.Array">array</see> of the specified <see cref="System.Type">Type</see>.</returns>
        public static T[] NewArray<T>(this T value, int length = 1)
        {
            T[] arr = new T[length];
            for (int i = 0; i < length; i++)
            {
                arr[i] = value;
            }
            return arr;
        }

        /// <summary>
        /// Pads the <see cref="System.Type">object</see> <see cref="System.Array">array</see> to the right with the specified length 
        /// of <see cref="System.Type">objects</see> (padValue) provided (or default). If the <see cref="System.Array">array</see>
        /// is null, a new <see cref="System.Array">array</see> of the specified length is returned provided the type is explicitedly 
        /// provided (like PadRight{byte}(byteArray...)).
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Type">type</see> in the <see cref="System.Array">array</see>.</typeparam>
        /// <param name="value">The <see cref="System.Array">array</see> of <see cref="System.Type">objects</see> to evaluate.</param>
        /// <param name="length">The desired length expressed as an <see cref="System.Int32">integer</see>.</param>
        /// <param name="padValue">The value of the <see cref="System.Type">type</see> to use as fill or default.</param>
        /// <param name="trim">If <see cref="bool">false</see> will throw a 
        /// <exception cref="System.InvalidOperationException">System.InvalidOperationException</exception> if the length of the provided 
        /// <see cref="System.Array">array</see> is greater than the length specified. If <see cref="bool">true</see> the 
        /// <see cref="System.Array">array</see> will be truncated on the right to the length provided. Default is 
        /// <see cref="bool">false</see> (throw).</param>
        /// <returns>The <see cref="System.Array">array</see> of <see cref="System.Type">objects</see> at the specified length.</returns>
        /// <exception cref="System.InvalidOperationException">System.InvalidOperationException</exception>
        public static T[] PadRight<T>(this T[] value, int length, T padValue = default, bool trim = false)
        {
            if (value == null)
            {
                return padValue.NewArray(length);
            }
            else if (value.Length > length)
            {
                if (!trim)
                {
                    throw new System.InvalidOperationException(
                        $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} {value.Length}");
                }
                return value.ToList().Take(length).ToArray();
            }
            else if (value.Length == length)
            {
                return value;
            }
            else
            {
                System.Collections.Generic.List<T> ret = new System.Collections.Generic.List<T>();
                ret.AddRange(value);
                ret.AddRange(padValue.NewArray(length - value.Length));
                return ret.ToArray();
            }
        }

        /// <summary>
        /// Pads the <see cref="System.Type">object</see> <see cref="System.Array">array</see> to the left with the specified length 
        /// of <see cref="System.Type">objects</see> (padValue) provided (or default). If the <see cref="System.Array">array</see>
        /// is null, a new <see cref="System.Array">array</see> of the specified length is returned provided the type is explicitedly 
        /// provided (like PadLeft{byte}(byteArray...)).
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Type">type</see> in the <see cref="System.Array">array</see>.</typeparam>
        /// <param name="value">The <see cref="System.Array">array</see> of <see cref="System.Type">objects</see> to evaluate.</param>
        /// <param name="length">The desired length expressed as an <see cref="System.Int32">integer</see>.</param>
        /// <param name="padValue">The value of the <see cref="System.Type">type</see> to use as fill or default.</param>
        /// <param name="trim">If <see cref="bool">false</see> will throw a 
        /// <exception cref="System.InvalidOperationException">System.InvalidOperationException</exception> if the length of the provided 
        /// <see cref="System.Array">array</see> is greater than the length specified. If <see cref="bool">true</see> the 
        /// <see cref="System.Array">array</see> will be truncated on the left to the length provided. Default is 
        /// <see cref="bool">false</see> (throw).</param>
        /// <returns>The <see cref="System.Array">array</see> of <see cref="System.Type">objects</see> at the specified length.</returns>
        /// <exception cref="System.InvalidOperationException">System.InvalidOperationException</exception>
        public static T[] PadLeft<T>(this T[] value, int length, T padValue = default, bool trim = false)
        {
            if (value == null)
            {
                return padValue.NewArray(length);
            }
            if (value.Length > length)
            {
                if (!trim)
                {
                    throw new System.InvalidOperationException(
                        $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} {value.Length}");
                }
                return value.ToList().Skip(value.Length - length).Take(length).ToArray();
            }
            else if (value.Length == length)
            {
                return value;
            }
            else
            {
                System.Collections.Generic.List<T> ret = new System.Collections.Generic.List<T>();
                ret.AddRange(padValue.NewArray(length - value.Length));
                ret.AddRange(value);
                return ret.ToArray();
            }
        }

        /// <summary>
        /// Returns the <see cref="System.Decimal">decimal</see> value of the signed number.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/intfmt9.htm">IBM Signed Numeric Format</seealso>.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from a null or empty  
        /// <see cref="System.Array">array</see>.
        /// </param>
        /// <param name="precision">The precision of the expected resultant as a <see cref="byte">Byte</see>. Default is 0. 
        /// The limitation of byte is for logical reasons. 
        /// </param>
        /// <param name="negativePrecision">If true, it will apply precision * -1 as the actual precision, allowing the
        /// <see cref="byte">byte</see> value of precision to be used as a negative number.
        /// </param>
        /// <param name="sourceEncoding">
        /// The <see cref="System.Text.Encoding">Encoding</see> the anticipated input is in.
        /// Default is <see cref="Encodings.EBCDIC">EBCDIC</see>, or IBM binary and text encoding.
        /// </param>
        /// <returns>The resultant as <see cref="System.Decimal">decimal</see>.</returns>
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception>
        public static decimal FromSignedDecimal(this byte[] value, byte precision = 0
            , bool negativePrecision = false, System.Text.Encoding sourceEncoding = null)
        {
            if (value == null || value.Length < 1)
            {
                throw new System.InvalidCastException($"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} { value?.Length.ToString() ?? "null" }");
            }
            System.Text.Encoding senc = sourceEncoding ?? Encodings.EBCDIC;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            decimal power = System.Convert.ToDecimal(System.Math.Pow(10, negativePrecision ? precision * -1 : precision));
            for (int i = 0; i < value.Length; i++)
            {
                if (i + 1 < value.Length)
                {
                    sb.Append($"{(new byte[] { value[i] }).ToEncodedString(senc, System.Text.Encoding.ASCII)}");
                }
                else 
                {
                    sb.Append($"{value[i].GetNibble(true).ToString().Right(1)}");
                }
            }
            decimal ret = System.Convert.ToDecimal(sb.ToString());
            ret /= power;
            return (value.Last() & (1 << 2)) != 0 ? ret * -1 : ret;
        }

        /// <summary>
        /// Returns the right-most <see cref="System.Char">characters</see> of the specified length as a 
        /// <see cref="System.String">string</see> or the value if length is longer or value is null.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> to evaluate</param>
        /// <param name="length">The <see cref="System.Int32">length</see> to return (or less).</param>
        /// <returns>A <see cref="System.String">string</see> up to the length of value or null if the value 
        /// is null. For exact length strings, add the <see cref="System.String.PadLeft(int)">PadLeft</see> and
        /// <see cref="System.String.PadRight(int)">PadRight</see> function for <see cref="System.String">String</see>.
        /// </returns>
        /// <seealso cref="System.String.PadLeft(int)"/>
        /// <seealso cref="System.String.PadRight(int)"/>
        /// <seealso cref="Left(string, int)"/>
        public static string Right(this string value, int length)
        {
            if (length < 1)
            {
                return "";
            }
            if (string.IsNullOrEmpty(value) || value.Length <= length)
            {
                return value;
            }
            return value.Substring(startIndex: value.Length - length);
        }

        /// <summary>
        /// Returns the left-most <see cref="System.Char">characters</see> of the specified length as a 
        /// <see cref="System.String">string</see> or the value if length is longer or value is null.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> to evaluate</param>
        /// <param name="length">The <see cref="System.Int32">length</see> to return (or less).</param>
        /// <returns>A <see cref="System.String">string</see> up to the length of value or null if the value 
        /// is null. For exact length strings, add the <see cref="System.String.PadLeft(int)">PadLeft</see> and
        /// <see cref="System.String.PadRight(int)">PadRight</see> function for <see cref="System.String">String</see>.
        /// </returns>
        /// <seealso cref="System.String.PadLeft(int)"/>
        /// <seealso cref="System.String.PadRight(int)"/>
        /// <seealso cref="Right(string, int)"/>
        public static string Left(this string value, int length)
        {
            if (length < 1)
            {
                return "";
            }
            if (string.IsNullOrEmpty(value) || value.Length <= length)
            {
                return value;
            }
            return value.Substring(0, length);
        }

        /// <summary>
        /// Gets the nibble (4-bit value) as a <see cref="byte">byte</see> or the first 4 (most signficiant)
        /// or last 4 (least significant) bits.
        /// </summary>
        /// <param name="value">The <see cref="byte">byte</see> value to evaluate.</param>
        /// <param name="leastSigificant">If <see cref="bool"/></param>
        /// <returns></returns>
        public static byte GetNibble(this byte value, bool leastSigificant = true)
        {
            string hex = System.BitConverter.ToString(new byte[] { value }).Remove("-");
            return System.Convert.ToByte($"0{hex.Substring(leastSigificant ? 1 : 0, 1)}", 16);
        }

        /// <summary>
        /// Provides expected short date string formats for <see cref="System.DateTime">DateTime</see>.
        /// Ex. "010221", "01022021", "01/02/2021"
        /// </summary>
        /// <param name="value">The <see cref="System.DateTime">System.DateTime</see> value.</param>
        /// <param name="length">The <see cref="System.Int32">length</see> to return (or less).</param>
        /// <returns>A <see cref="System.String">string</see> up to the length.
        /// </returns>
        public static string ToFormattedDateString(this System.DateTime value, int length)
        {
            if (length > 10 || length < 6 || length % 2 != 0)
            {
                return value.ToString().PadRight(length, ' ').Substring(0, length);
            }
            if (length == 6)
            {
                return value.ToString("MMddyy");
            }
            if (length == 8)
            {
                return value.ToString("MMddyyyy");
            }
            return value.ToString("MM/dd/yyyy");
        }

        /// <summary>
        /// Returns the julian day number of the provided <see cref="System.DateTime">DateTime</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.DateTime">DateTime</see> to evaluate.</param>
        /// <returns>The <see cref="System.Int32">integer</see> Julian Day.</returns>
        public static int JulianDay(this System.DateTime value)
        {
            return (value - (new System.DateTime(value.Year, 1, 1))).Days + 1;
        }

        /// <summary>
        /// Returns a <see cref="System.String">string</see> representation of the Julian Date
        /// from the <see cref="System.DateTime">DateTime</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.DateTime">DateTime</see> to evaluate.</param>
        /// <param name="twoDigitYear">If <see cref="bool">true</see> will only return the
        /// last 2 digits of the year in the representation, else all 4 digits are returned.</param>
        /// <returns>A <see cref="System.String">string</see> representation of the Julian Date.</returns>
        public static string JulianDate(this System.DateTime value, bool twoDigitYear = false)
        {
            string year = twoDigitYear ? value.Year.ToString().Substring(2, 2).PadLeft(2, '0') : value.Year.ToString().PadLeft(4, '0');
            return $"{year}\\{value.JulianDay().ToString().PadLeft(3, '0')}";
        }

        /// <summary>
        /// Converts the <see cref="System.Int32">integer</see> value from a Julian Day value to a
        /// <see cref="System.DateTime">DateTime</see> using the optional year value or the current year.
        /// </summary>
        /// <param name="value">The <see cref="System.Int32">integer</see> value to evaluate.</param>
        /// <param name="year">The <see cref="System.Int32">integer</see> value for the returned
        /// <see cref="System.DateTime">DateTime's</see> Year component or null.</param>
        /// <returns>A <see cref="System.DateTime">DateTime</see> representing the number.</returns>
        public static System.DateTime FromJulianDay(this int value, int? year = null)
        {
            int yr = year ?? System.DateTime.Today.Year;
            return (new System.DateTime(yr, 1, 1)).AddDays(value - 1);
        }

        /// <summary>
        /// Converts the string value to a <see cref="System.Nullable{T}">nullable</see> 
        /// <see cref="System.DateTime">DateTme</see> assuming a Julian Date format.
        /// Expected as "{year}\{day}".
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> to evaluate.</param>
        /// <returns>A <see cref="System.Nullable{T}">nullable</see> <see cref="System.DateTime">DateTme</see></returns>
        public static System.DateTime? FromJulianDate(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            if (!value.Remove("\\").Trim().IsNumeric())
            {
                throw new System.InvalidOperationException(
                    $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("UnsupportedType")} {value}");
            }
            string[] vals = value.Trim().Split(new string[] { "\\" }, System.StringSplitOptions.RemoveEmptyEntries);
            if (vals.Length != 2)
            {
                throw new System.InvalidOperationException(
                    $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("UnsupportedType")} {value}");
            }
            int year = System.Convert.ToInt32(vals[0].Trim());
            int jday = System.Convert.ToInt32(vals[1].Trim());
            if (year < 100)
            {
                year += (System.DateTime.Today.Century() * 100);
            }
            return jday.FromJulianDay(year);
        }

        /// <summary>
        /// Reads the <see cref="System.IO.Stream">stream</see> for the specified number of bytes or null.
        /// </summary>
        /// <param name="value">The <see cref="System.IO.Stream">stream</see> to read from.</param>
        /// <param name="RecordLength">The number of bytes to read expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <returns>An <see cref="System.Array">array</see> of bytes from the stream or null.</returns>
        public static byte[] ReadNext(this System.IO.Stream value, uint RecordLength)
        {
            byte[] buffer = new byte[RecordLength];
            
            if (value.Length - value.Position >= RecordLength &&
                (value.Read(buffer, 0, RecordLength.ToInt()) > 0))
            {
                return buffer;
            }
            return null;
        }

        /// <summary>
        /// Reads the <see cref="System.IO.Stream">stream</see> up to the specified <see cref="System.Char">character</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.IO.Stream">stream</see> to read from.</param>
        /// <param name="Terminator">The end of line <see cref="System.Char">char</see>.</param>
        /// <param name="sourceEncoding">The optional destination <see cref="System.Text.Encoding">Encoding</see> or 
        /// <see cref="System.Text.Encoding.ASCII">ASCII</see>.</param>
        /// <returns>An <see cref="System.Array">array</see> of bytes from the stream or null.</returns>
        public static byte[] ReadToChar(this System.IO.Stream value, char Terminator, System.Text.Encoding sourceEncoding = null)
        {
            sourceEncoding = sourceEncoding ?? System.Text.Encoding.ASCII;
            var reader = new System.IO.StreamReader(value);
            System.Collections.Generic.List<byte> ret = new System.Collections.Generic.List<byte>();
            int i = reader.Read();
            bool found = false;
            while (i > 0 && !found)
            {
                byte b = (byte)i;
                if (sourceEncoding != System.Text.Encoding.ASCII)
                {
                    b = ((byte)i).Convert(sourceEncoding, System.Text.Encoding.ASCII);
                }
                if ((char)b != Terminator)
                {
                    ret.Add((byte)i);
                }
                i = reader.Read();
            }
            if (ret.Count < 1)
            {
                return null;
            }
            return ret.ToArray();
        }

        /// <summary>
        /// Creates a <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="IFileRecord">records</see> 
        /// from the <see cref="System.Data.DataTable">DataTable</see> using the <see cref="IFileLayout">layout</see> provided.
        /// Note: Column and field names much match. Any unmatched fields will be added to the Exceptions collection of the record.
        /// Any unmatched columns will be ignored.
        /// </summary>
        /// <param name="table">The <see cref="System.Data.DataTable">DataTable</see> to be exported.</param>
        /// <param name="layout">The <see cref="IFileLayout">layout</see> to implement.</param>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of 
        /// <see cref="IFileRecord">IFileRecord</see>.</returns>
        public static System.Collections.Generic.IEnumerable<IFileRecord> ToFileRecords(this System.Data.DataTable table, IFileLayout layout)
        {
            return table.ToFileRecords(layout.MasterFields);
        }

        /// <summary>
        /// Creates a <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="IFileRecord">records</see> 
        /// from the <see cref="System.Data.DataTable">DataTable</see> using the <see cref="IFileLayout">layout</see> provided.
        /// Note: Column and field names much match. Any unmatched fields will be added to the Exceptions collection of the record.
        /// Any unmatched columns will be ignored. Useful for building Conditional records for 3D files.
        /// </summary>
        /// <param name="table">The <see cref="System.Data.DataTable">DataTable</see> to be exported.</param>
        /// <param name="conditional">The <see cref="IFileRecord">record</see> to implement as master.</param>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of 
        /// <see cref="IFileRecord">IFileRecord</see>.</returns>
        public static System.Collections.Generic.IEnumerable<IFileRecord> ToFileRecords(this System.Data.DataTable table, IFileRecord conditional)
        {
            return table.ToFileRecords(conditional.Fields);
        }

        /// <summary>
        /// Creates a <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="IFileRecord">records</see> 
        /// from the <see cref="System.Data.DataTable">DataTable</see> using the <see cref="IFileLayout">layout</see> provided.
        /// Note: Column and field names much match. Any unmatched fields will be added to the Exceptions collection of the record.
        /// Any unmatched columns will be ignored. Useful for building Conditional records for 3D files.
        /// </summary>
        /// <param name="table">The <see cref="System.Data.DataTable">DataTable</see> to be exported.</param>
        /// <param name="fields">
        /// The <see cref="System.Array">collection</see> of <see cref="IFileField">fields</see> to implement as master.
        /// </param>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of 
        /// <see cref="IFileRecord">IFileRecord</see>.</returns>
        public static System.Collections.Generic.IEnumerable<IFileRecord> ToFileRecords(this System.Data.DataTable table, IFileField[] fields)
        {
            System.Collections.Generic.List<IFileRecord> records = new System.Collections.Generic.List<IFileRecord>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                FileRecord rec = new FileRecord(fields);
                foreach (var field in rec.Fields)
                {
                    try
                    {
                        var val = row[field.Name];
                        if (val != null && val != System.DBNull.Value)
                        {
                            field.Value = val;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        rec.AddException(new FieldException(ex, field));
                    }
                }
                records.Add(rec);
            }
            return records;
        }

        /// <summary>
        /// Imports a <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="IFileRecord">records</see> 
        /// from the <see cref="System.Data.DataTable">DataTable</see> using the <see cref="IFileLayout">layout</see> of the 
        /// <see cref="IFileParser">parser</see>.
        /// </summary>
        /// <param name="value">The <see cref="IFileParser">parser</see> being affected.</param>
        /// <param name="table">The <see cref="System.Data.DataTable">DataTable</see> to be imported.</param>
        /// <returns>The same instance of the <see cref="IFileParser">IFileParser</see>.</returns>
        public static IFileParser ImportDataTable(this IFileParser value, System.Data.DataTable table)
        {
            value.AddRecords(table.ToFileRecords(value.Layout));
            return value;
        }

    }
}
