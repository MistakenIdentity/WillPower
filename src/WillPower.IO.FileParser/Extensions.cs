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
    public static class Extensions
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
        /// Determines if the <see cref="System.Object">object</see> is of a simple <see cref="System.Type">type</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.Object">object</see> to evaluate.</param>
        /// <returns><see cref="System.Boolean">True</see> if the <see cref="System.Object">object</see> is not of a complex 
        /// <see cref="System.Type">type</see>.</returns>
        public static bool IsSimpleType(this object value)
        {
            System.Type t = value.GetType();
            return !(t != typeof(string)
                && t != typeof(bool) && t != typeof(bool?)
                && t != typeof(byte) && t != typeof(byte?)
                && t != typeof(short) && t != typeof(short?)
                && t != typeof(int) && t != typeof(int?)
                && t != typeof(long) && t != typeof(long?)
                && t != typeof(System.Single) && t != typeof(System.Single?)
                && t != typeof(System.DateTime) && t != typeof(System.DateTime?)
                && t != typeof(double) && t != typeof(double?)
                && t != typeof(decimal) && t != typeof(decimal?)
                && t != typeof(float) && t != typeof(float?));
        }

        /// <summary>
        /// Returns a <see cref="System.String">string</see> containing only the numbers and, optionally, one decimal 
        /// (.) (the first encountered) from the value.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> to parse.</param>
        /// <param name="allowDecimal">If <see cref="System.Boolean">true</see> will allow a single '.' (the first encountered). 
        /// Default is <see cref="System.Boolean">false</see>.</param>
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
        /// Returns <see cref="System.Boolean">true</see> if all characters in the string are digits and,
        /// if allowDecimal is <see cref="System.Boolean">true</see>, only one '.' exists.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> to evaluate.</param>
        /// <param name="allowDecimal">If <see cref="System.Boolean">true</see> will allow a single '.' (the first encountered). 
        /// Default is <see cref="System.Boolean">false</see>.</param>
        /// <returns>
        /// <see cref="System.Boolean">True</see> if all characters are numbers and, optionally, only one decimal (.) exists.
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
            if (sourceEncoding == (destinationEncoding ?? System.Text.Encoding.UTF8))
            {
                return value;
            }
            return System.Text.Encoding.Convert(sourceEncoding,
                destinationEncoding ?? System.Text.Encoding.UTF8, value);
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
            return value * (decimal)System.Math.Pow(10, exponent);
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
        /// </summary>
        /// <param name="value">The <see cref="System.DateTime">System.DateTime</see> value</param>
        /// <returns>The Century as an <see cref="System.Int32">int</see>.</returns>
        public static int Century(this System.DateTime value)
        {
            return System.Convert.ToInt32(System.Math.Floor((double)value.Year / 100));
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
            return $"{ret.Substring(0, 1).ToUpperInvariant()}{ret.Substring(1).ToLowerInvariant()}";
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
                return null;
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
        /// Converts the string to a <see cref="System.Boolean">bool</see> or null.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> value to evaluate.</param>
        /// <returns><see cref="System.Boolean">Boolean</see> or null.</returns>
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
        /// Converts the string to a <see cref="System.DateTime">System.DateTime</see> or null.
        /// </summary>
        /// <param name="value">The <see cref="System.String">string</see> value to evaluate.</param>
        /// <param name="yearFirst">If <see cref="System.Boolean">true</see> it assumes the year is the first part of the string. 
        /// Default is <see cref="System.Boolean">false</see>.
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
            if (value.Length != 6 && value.Length != 8)
            {
                return null;
            }
            if (!int.TryParse(value.Substring(yearFirst ? 0 : 4, value.Length == 8 ? 4 : 2), out int year)
                || year < System.DateTime.MinValue.Year || year > System.DateTime.MaxValue.Year)
            {
                return null;
            }
            if (!int.TryParse(value.Substring(yearFirst ? (value.Length == 8 ? 4 : 2) : 0, 2), out int month)
                || month < 1 || month > 12)
            {
                return null;
            }
            if (!int.TryParse(value.Substring(yearFirst ? (value.Length == 8 ? 6 : 4) : 2, 2), out int day)
                || day < 1 || day > System.DateTime.DaysInMonth(year, month))
            {
                return null;
            }
            return new System.DateTime(year, month, day);
        }

        /// <summary>
        /// Converts an <see cref="System.UInt32">unsigned integer</see> to a <see cref="System.Int32">signed integer</see>.
        /// If the <see cref="System.UInt32">unsigned integer</see> is greater than <see cref="System.Int32.MaxValue">int.MaxValue</see> it will return
        /// a negative <see cref="System.Int32">integer</see> starting at -1 for each value greater than <see cref="System.Int32.MaxValue">int.MaxValue</see>.
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
        /// <returns>The resulting <see cref="System.Int32">signed integer</see>.</returns>
        public static int ToInt(this uint value)
        {
            if (value > int.MaxValue)
            {
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
        /// <returns>The resulting <see cref="System.UInt32">unsigned integer</see>.</returns>
        public static uint ToUInt(this int value)
        {
            return value < 0 ? System.Convert.ToUInt32(int.MaxValue + System.Math.Abs(value)) : System.Convert.ToUInt32(value);
        }

        /// <summary>
        /// Returns the default <see cref="System.Threading.Tasks.TaskScheduler">TaskScheduler</see> of the <see cref="System.Threading.Tasks.TaskFactory"> 
        /// TaskFactory's</see> <see cref="System.Threading.Tasks.TaskScheduler.MaximumConcurrencyLevel">MaximumConcurrencyLevel</see> 
        /// as an <see cref="System.UInt32">unsigned integer</see>.
        /// <code>
        /// public static uint MaximumConcurrency(this System.Threading.Tasks.TaskFactory value)
        /// {
        ///     return (value?.<see cref="System.Threading.Tasks.TaskFactory.Scheduler">Scheduler</see>?
        ///         .MaximumConcurrencyLevel ?? 0).<see cref="ToUInt(int)">ToUInt()</see>;
        /// }
        /// </code>
        /// </summary>
        /// <param name="value">The <see cref="System.Threading.Tasks.TaskFactory">TaskFactory</see> to evaluate.</param>
        /// <returns>
        /// The resulting <see cref="System.Int32">signed integer</see> as an <see cref="System.UInt32">unsigned integer</see>.
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
        /// Returns <see cref="System.Boolean">true</see> if the <see cref="System.Byte">byte</see> has value > 0 in the second nibble.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Byte">byte</see> to evaluate.</param>
        /// <returns>The <see cref="System.Boolean">Boolean</see> result or false.</returns>
        public static bool FromPackedBool(this byte value)
        {
            if ((new byte[] { value }).FromPackedDecimal(0) > 0)
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
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.Int16">System.Int16</see> resultant.</returns>
        public static short FromPackedShort(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            if (newvalue > short.MaxValue || newvalue < short.MinValue)
            {
                return 0;
            }
            return System.Convert.ToInt16(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.Int32">int</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.Int32">System.Int32</see> resultant.</returns>
        public static int FromPackedInt(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            if (newvalue > int.MaxValue || newvalue < int.MinValue)
            {
                return 0;
            }
            return System.Convert.ToInt32(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.Int64">long</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.Int64">System.Int64</see> resultant.</returns>
        public static long FromPackedLong(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            if (newvalue > long.MaxValue || newvalue < long.MinValue)
            {
                return 0;
            }
            return System.Convert.ToInt64(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.UInt16">unsigned short</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.UInt16">System.UInt16</see> resultant.</returns>
        public static ushort FromPackedUShort(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            if (newvalue > ushort.MaxValue || newvalue < ushort.MinValue)
            {
                return 0;
            }
            return System.Convert.ToUInt16(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.UInt32">unsigned int</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.UInt32">System.UInt32</see> resultant.</returns>
        public static uint FromPackedUInt(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            if (newvalue > uint.MaxValue || newvalue < uint.MinValue)
            {
                return 0;
            }
            return System.Convert.ToUInt32(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.UInt64">unsigned long</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.UInt64">System.UInt64</see> resultant.</returns>
        public static ulong FromPackedULong(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            if (newvalue > ulong.MaxValue || newvalue < ulong.MinValue)
            {
                return 0;
            }
            return System.Convert.ToUInt64(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.Double">double</see> value of the packed number or 0.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.Double">System.Double</see> resultant.</returns>
        public static double FromPackedDouble(this byte[] value)
        {
            decimal newvalue = value.FromPackedDecimal(0).Round();
            if ((double)newvalue > double.MaxValue || (double)newvalue < double.MinValue)
            {
                return 0;
            }
            return System.Convert.ToDouble(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.DateTime">DateTime</see> value of the packed number or null.
        /// Note: The byte <see cref="System.Array">array</see> must be 2 or 4 bytes long.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate. 
        /// Length must be 2 (Short Date Format) or 4 (Long Date Format).
        /// </param>
        /// <returns>The <see cref="System.DateTime">System.DateTime</see> resultant or null.</returns>
        public static System.DateTime? FromPackedDate(this byte[] value)
        {
            int year, month, day;
            switch (value.Length)
            {
                case 2:
                    if ((value[1] & (1 << 0)) != 0)
                    {
                        year = (value[1] - 64) + (System.DateTime.Today.Century() * 100);
                    }
                    else
                    {
                        year = System.DateTime.Today.Century() * 100;
                    }
                    month = (value[0] & (1 << 3)) != 0 ? 1 : 0;
                    month += (value[0] & (1 << 2)) != 0 ? 2 : 0;
                    month += (value[0] & (1 << 1)) != 0 ? 4 : 0;
                    month += (value[0] & (1 << 0)) != 0 ? 8 : 0;
                    day = (value[1] & (1 << 0)) != 0 ? 1 : 0;
                    day += (value[0] & (1 << 7)) != 0 ? 2 : 0;
                    day += (value[0] & (1 << 6)) != 0 ? 4 : 0;
                    day += (value[0] & (1 << 5)) != 0 ? 8 : 0;
                    day += (value[0] & (1 << 4)) != 0 ? 16 : 0;
                    break;
                case 4:
                    year = System.Convert.ToInt32((new byte[] { value[0], value[1] }).FromComp3());
                    month = System.Convert.ToInt32((new byte[] { value[2] }).FromComp3());
                    day = System.Convert.ToInt32((new byte[] { value[3] }).FromComp3());
                    break;
                default:
                    throw new System.InvalidCastException($"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} { value.Length }");
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
        /// <returns>The resulting <see cref="System.UInt64">unsigned long</see>.</returns>
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
            ulong ret = 0;
            for (int i = bytes.Length - 2; i > -1; i--)
            {
                ret += (ulong)(bytes[i] * place);
                place *= 10;
            }
            return ret;
        }

        /// <summary>
        /// Returns the <see cref="System.Decimal">decimal</see> value of the packed number.
        /// <seealso href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Decimal Format.</seealso>
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from null or empty byte <see cref="System.Array">array</see>.
        /// </param>
        /// <param name="precision">The precision of the expected resultant as <see cref="System.Byte"/>Byte. Default is 0. The limitation is from logical reasons. 
        /// Obviously negative values are not supported and should be handled through the decimal.Raise(double) extension method or your own code (10 raised to precision * value).
        /// </param>
        /// <returns>The resultant as <see cref="System.Decimal">decimal</see>.</returns>
        public static decimal FromPackedDecimal(this byte[] value, byte precision = 0)
        {
            if (value.Length < 1)
            {
                throw new System.InvalidCastException($"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} { value.Length }");
            }
            decimal power = System.Convert.ToDecimal(System.Math.Pow(10, precision));
            string hex = System.BitConverter.ToString(value).Remove("-");
            var bytes = Enumerable.Range(0, hex.Length)
                .Select(x => System.Convert.ToByte($"0{hex.Substring(x, 1)}", 16))
                .ToArray();
            long place = 1;
            decimal ret = 0;
            for (int i = bytes.Length - 2; i > -1; i--)
            {
                ret += (bytes[i] * place);
                place *= 10;
            }
            ret /= power;
            return (bytes.Last() & (1 << 7)) != 0 ? ret * -1 : ret;
        }

        /// <summary>
        /// Returns the <see cref="System.DateTime">DateTime</see> value of the byte <see cref="System.Array">array</see> or null.
        /// It is assumed the bytes store a <see cref="System.DateTime">DateTime</see> value encoded directly.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate. 
        /// </param>
        /// <returns>The <see cref="System.DateTime">System.DateTime</see> resultant or null.</returns>
        public static System.DateTime? FromRawDate(this byte[] value)
        {
            return value.ToObject<System.DateTime?>();
        }

        /// <summary>
        /// Returns the <see cref="System.Decimal">decimal</see> value of the byte <see cref="System.Array">array</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from null or 
        /// empty byte <see cref="System.Array">array</see>.
        /// </param>
        /// <param name="precision">The precision of the expected resultant as <see cref="System.Byte"/>Byte. Default is 0. 
        /// The limitation is from logical reasons. 
        /// Obviously negative values are not supported and should be handled through the decimal.Raise(double) 
        /// extension method or your own code (10 raised to precision * value).
        /// </param>
        /// <returns>The resultant as <see cref="System.Decimal">decimal</see>.</returns>
        public static decimal FromRawDecimal(this byte[] value, byte precision = 0)
        {
            if (value.Length < 1)
            {
                throw new System.InvalidCastException($"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} { value.Length }");
            }
            decimal power = System.Convert.ToDecimal(System.Math.Pow(10, precision));
            long place = 1;
            decimal ret = 0;
            for (int i = value.Length - 2; i > -1; i--)
            {
                ret += (value[i] * place);
                place *= 256;
            }
            ret += value.Last() > 127 ? ((value.Last() - 127) * place) : ((value.Last()) * place);
            ret /= power;
            return (value[0] & (1 << 0)) != 0 ? ret * -1 : ret;
        }

        /// <summary>
        /// Returns the <see cref="System.Int16">short</see> value of the byte <see cref="System.Array">array</see> or 0.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.Int16">System.Int16</see> resultant.</returns>
        public static short FromRawShort(this byte[] value)
        {
            decimal newvalue = value.FromRawDecimal(0).Round();
            if (newvalue > short.MaxValue || newvalue < short.MinValue)
            {
                return 0;
            }
            return System.Convert.ToInt16(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.Int32">int</see> value of the byte <see cref="System.Array">array</see> or 0.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.Int32">System.Int32</see> resultant.</returns>
        public static int FromRawInt(this byte[] value)
        {
            decimal newvalue = value.FromRawDecimal(0).Round();
            if (newvalue > int.MaxValue || newvalue < int.MinValue)
            {
                return 0;
            }
            return System.Convert.ToInt32(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.Int64">long</see> value of the byte <see cref="System.Array">array</see> or 0.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.Int64">System.Int64</see> resultant.</returns>
        public static long FromRawLong(this byte[] value)
        {
            decimal newvalue = value.FromRawDecimal(0).Round();
            if (newvalue > long.MaxValue || newvalue < long.MinValue)
            {
                return 0;
            }
            return System.Convert.ToInt64(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.Double">double</see> value of the byte <see cref="System.Array">array</see> or 0.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.Double">System.Double</see> resultant.</returns>
        public static double FromRawDouble(this byte[] value)
        {
            decimal newvalue = value.FromRawDecimal(0).Round();
            if ((double)newvalue > double.MaxValue || (double)newvalue < double.MinValue)
            {
                return 0;
            }
            return System.Convert.ToDouble(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.UInt16">unsigned short</see> value of the byte <see cref="System.Array">array</see> or 0.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.UInt16">System.UInt16</see> resultant.</returns>
        public static ushort FromRawUShort(this byte[] value)
        {
            decimal newvalue = value.FromRawDecimal(0).Round();
            if (newvalue > ushort.MaxValue || newvalue < ushort.MinValue)
            {
                return 0;
            }
            return System.Convert.ToUInt16(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.UInt32">unsigned int</see> value of the byte <see cref="System.Array">array</see> or 0.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.UInt32">System.UInt32</see> resultant.</returns>
        public static uint FromRawUInt(this byte[] value)
        {
            decimal newvalue = value.FromRawDecimal(0).Round();
            if (newvalue > uint.MaxValue || newvalue < uint.MinValue)
            {
                return 0;
            }
            return System.Convert.ToUInt32(newvalue);
        }

        /// <summary>
        /// Returns the <see cref="System.UInt64">unsigned long</see> value of the byte <see cref="System.Array">array</see> or 0.
        /// </summary>
        /// <param name="value">The <see cref="System.Array">array</see> of bytes to evaluate.</param>
        /// <returns>The <see cref="System.UInt64">System.UInt64</see> resultant.</returns>
        public static ulong FromRawULong(this byte[] value)
        {
            decimal newvalue = value.FromRawDecimal(0).Round();
            if (newvalue > ulong.MaxValue || newvalue < ulong.MinValue)
            {
                return 0;
            }
            return System.Convert.ToUInt64(newvalue);
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
            if (value.Read(buffer, (int)value.Position, RecordLength.ToInt()) > 0)
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
                    b = (new byte[] { (byte)i }).Convert(sourceEncoding, System.Text.Encoding.ASCII)[0];
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

    }
}
