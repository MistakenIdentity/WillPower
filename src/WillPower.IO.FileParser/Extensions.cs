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
        /// <exception cref="System.InvalidCastException">System.InvalidCastException</exception> results from null or empty byte <see cref="System.Array">array</see>.
        /// </param>
        /// <param name="precision">The precision of the expected resultant as <see cref="System.Byte"/>Byte. Default is 0. The limitation is from logical reasons. 
        /// Obviously negative values are not supported and should be handled through the decimal.Raise(double) extension method or your own code (10 raised to precision * value).
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
            ret += value[^1] > 127 ? ((value[^1] - 127) * place) : ((value[^1]) * place);
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
            sourceEncoding ??= System.Text.Encoding.ASCII;
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
