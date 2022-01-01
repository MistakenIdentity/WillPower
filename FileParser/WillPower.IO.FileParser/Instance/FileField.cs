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

using System;
using System.Linq;

namespace WillPower
{
    /// <summary>
    /// A concrete class containing the necessary properties and methods for processing a single field.
    /// Cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class FileField : FileFieldBase, IFileField
    {
        /// <summary>
        /// The original, source <see cref="System.Text.Encoding">Encoded</see> <see cref="System.Array">array</see> of bytes belonging to this field.
        /// </summary>
        public override byte[] ByteValue
        {
            get
            {
                return byteValue;
            }
            set
            {
                byteValue = value;
                Compute();
            }
        }
        private byte[] byteValue;
        /// <summary>
        /// The <see cref="IFileField.Compute(byte[])">computed</see> result as a <see cref="System.String">string</see> or 
        /// the value to be written.
        /// </summary>
        public override string StringValue
        {
            get
            {
                return ByteValue.ToEncodedString(Encoder.SourceEncoding, Encoder.DestinationEncoding);
            }
            set
            {
                Value = value;
            }
        }

        /// <summary>
        /// .ctor. Creates a new instance of FileField.
        /// </summary>
        /// <param name="write">
        /// If <see cref="System.Boolean">true</see>, this will use the default ASCII to EBCDIC 
        /// <see cref="IFileParserEncoder">IFileParserEncoder</see> instance for writing binary EBCDIC fields.
        /// If <see cref="System.Boolean">false</see>, this will use the default EBCDIC to ASCII 
        /// <see cref="IFileParserEncoder">IFileParserEncoder</see> instance for reading binary EBCDIC fields.
        /// Default is <see cref="System.Boolean">false</see>.
        /// </param>
        public FileField(bool write = false) : base(write)
        { }
        /// <summary>
        /// .ctor. Creates a new instance of FileField.
        /// </summary>
        /// <param name="clone">The source <see cref="IFileField">field</see> to clone.</param>
        public FileField(IFileField clone) : base(clone)
        { }
        /// <summary>
        /// .ctor. Creates a new instance of FileField.
        /// </summary>
        /// <param name="clone">The source <see cref="IFileField">field</see> to clone.</param>
        /// <param name="data">The <see cref="System.Array">array</see> of bytes to read.</param>
        public FileField(IFileField clone, byte[] data) : this(clone)
        {
            ByteValue = data;
        }
        /// <summary>
        /// .ctor. Creates a new instance of FileField.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        /// <param name="type">The <see cref="System.Type">type</see> of data the field contains.</param>
        /// <param name="name">The <see cref="System.String">name</see> of the field.</param>
        /// <param name="startPosition">The starting position of this field in the <see cref="IFileRecord">record</see> 
        /// expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="length">The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="format">The <see cref="FileFieldDataFormat">format</see> of the field.</param>
        public FileField(IFileParserEncoder encoder, Type type, string name, uint startPosition, uint length, FileFieldDataFormat format)
        {
            if (format == FileFieldDataFormat.Table)
            {
                throw new InvalidOperationException($"{IO.FileParser.Properties.Resources.ResourceManager.GetString("UnsupportedType")} : {format}");
            }
            Encoder = encoder;
            Type = type;
            Name = name;
            StartPosition = startPosition;
            Length = length;
            DataFormat = format;
        }
        /// <summary>
        /// .ctor. Creates a new instance of FileField. Used for numeric <see cref="System.Decimal">(decimal)</see> <see cref="System.Type">types</see>.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        /// <param name="name">The <see cref="System.String">name</see> of the field.</param>
        /// <param name="startPosition">The starting position of this field in the <see cref="IFileRecord">record</see> 
        /// expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="length">The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="format">The <see cref="FileFieldDataFormat">format</see> of the field.</param>
        /// <param name="precision">The precision of the expected resultant as <see cref="System.Int16"/>short. Default is 0. 
        /// The limitation is from logical reasons.</param>
        /// <param name="required">If <see cref="System.Boolean">true</see>, throw a <see cref="FieldException"></see> on a null result.</param>
        public FileField(IFileParserEncoder encoder, string name, uint startPosition, uint length, FileFieldDataFormat format, 
            short precision, bool required = false)
            : base(encoder, name, startPosition, length, format, precision, required)
        { }
        /// <summary>
        /// .ctor. Creates a new instance of FileField. Used for <see cref="System.DateTime">Date</see> <see cref="System.Type">types</see>.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        /// <param name="name">The <see cref="System.String">name</see> of the field.</param>
        /// <param name="startPosition">The starting position of this field in the <see cref="IFileRecord">record</see> 
        /// expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="length">The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="format">The <see cref="FileFieldDataFormat">format</see> of the field.</param>
        /// <param name="yearFirst">If <see cref="System.Boolean">true</see> it assumes the year is the first part of the string. 
        /// Default is <see cref="System.Boolean">false</see>.
        /// Note: If the <see cref="System.String">string</see> contains a parsable date character ( / or - ) it will attempt to use 
        /// <see cref="System.DateTime.TryParse(string, out System.DateTime)">TryParse</see> and return any successful result. 
        /// If <see cref="System.DateTime.TryParse(string, out System.DateTime)">TryParse</see> fails it proceeds on to 
        /// <see cref="System.String">string</see> parsing.
        /// </param>
        /// <param name="required">If <see cref="System.Boolean">true</see>, throw a <see cref="FieldException"></see> on a null result.</param>
        public FileField(IFileParserEncoder encoder, string name, uint startPosition, uint length, FileFieldDataFormat format, 
            bool yearFirst, bool required = false)
            : base(encoder, name, startPosition, length, format, yearFirst, required)
        { }
        /// <summary>
        /// .ctor. Must be inherited. User for <see cref="System.Boolean">boolean</see> types with a <see cref="System.String">string</see> output.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        /// <param name="name">The <see cref="System.String">name</see> of the field.</param>
        /// <param name="startPosition">The starting position of this field in the <see cref="IFileRecord">record</see> expressed as an 
        /// <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="length">The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="showAs">
        /// Identifies how the <see cref="System.Boolean">boolean</see> value will be represented as a <see cref="System.String">string</see>.
        /// Length of 1 will only return the first <see cref="System.Char">character</see> of the expected <see cref="System.String">string</see>.
        /// </param>
        public FileField(IFileParserEncoder encoder, string name, uint startPosition, uint length, BooleanStringRepresentation showAs)
            : base(encoder, name, startPosition, length, showAs)
        { }

        /// <summary>
        /// Computes this provided <see cref="System.Array">array</see> of bytes using the properties provided.
        /// </summary>
        /// <param name="data">The <see cref="System.Array">array</see> of bytes to process.</param>
        public override void Compute(byte[] data = null)
        {
            if (data != null && data.Length > 0)
            {
                byteValue = data;
            }
            if (ByteValue == null || ByteValue.Length < 1)
            {
                return;
            }
            if (Type == typeof(string))
            {
                Value = StringValue;
                return;
            }
            else if (DataFormat == FileFieldDataFormat.Raw)
            {
                Value = ByteValue.ToObject();
                return;
            }
            else if (Type == typeof(byte) || Type == typeof(byte?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        Value = ByteValue.First();
                        return;
                    case FileFieldDataFormat.String:
                        Value = byte.Parse(StringValue);
                        return;
                }
            }
            else if (Type == typeof(DateTime) || Type == typeof(DateTime?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            Value = ByteValue.FromPackedDate();
                        }
                        else
                        {
                            Value = ByteValue.FromPackedDate().Value;
                        }
                        return;
                    case FileFieldDataFormat.String:
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            Value = StringValue.ToDateTime(YearFirst);
                        }
                        else
                        {
                            Value = StringValue.ToDateTime(YearFirst).Value;
                        }
                        return;
                }
            }
            else if (Type == typeof(bool) || Type == typeof(bool?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        Value = ByteValue.Last() > 0;
                        return;
                    case FileFieldDataFormat.String:
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            Value = StringValue.ToBool();
                        }
                        else
                        {
                            Value = StringValue.ToBool().Value;
                        }
                        return;
                }
            }
            else if (Type == typeof(short) || Type == typeof(short?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        Value = ByteValue.FromPackedShort();
                        return;
                    case FileFieldDataFormat.String:
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (short.TryParse(StringValue?.Trim(), out short ret))
                            {
                                Value = ret;
                            }
                            else
                            {
                                Value = null;
                            }
                        }
                        else
                        {
                            Value = short.Parse(StringValue.Trim());
                        }
                        return;
                }
            }
            else if (Type == typeof(int) || Type == typeof(int?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        Value = ByteValue.FromPackedInt();
                        return;
                    case FileFieldDataFormat.String:
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (int.TryParse(StringValue?.Trim(), out int ret))
                            {
                                Value = ret;
                            }
                            else
                            {
                                Value = null;
                            }
                        }
                        else
                        {
                            Value = int.Parse(StringValue.Trim());
                        }
                        return;
                }
            }
            else if (Type == typeof(long) || Type == typeof(long?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        Value = ByteValue.FromPackedLong();
                        return;
                    case FileFieldDataFormat.String:
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (long.TryParse(StringValue?.Trim(), out long ret))
                            {
                                Value = ret;
                            }
                            else
                            {
                                Value = null;
                            }
                        }
                        else
                        {
                            Value = long.Parse(StringValue.Trim());
                        }
                        return;
                }
            }
            else if (Type == typeof(double) || Type == typeof(double?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        Value = ByteValue.FromPackedDouble(Precision);
                        return;
                    case FileFieldDataFormat.String:
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (double.TryParse(StringValue?.Trim(), out double ret))
                            {
                                Value = ret;
                            }
                            else
                            {
                                Value = null;
                            }
                        }
                        else
                        {
                            Value = long.Parse(StringValue.Trim());
                        }
                        return;
                }
            }
            else if (Type == typeof(decimal) || Type == typeof(decimal?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        Value = ByteValue.FromPackedDecimal(Precision);
                        return;
                    case FileFieldDataFormat.String:
                        decimal ret;
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (!decimal.TryParse(StringValue?.Trim(), out ret))
                            {
                                Value = null;
                                return;
                            }
                        }
                        else
                        {
                            ret = decimal.Parse(StringValue.Trim());
                        }
                        if (Precision != 0 && !StringValue.Contains("."))
                        {
                            ret /= (decimal)Math.Pow(10, Precision);
                        }
                        Value = ret;
                        return;
                }
            }
            else if (Type == typeof(float) || Type == typeof(float?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        Value = ByteValue.FromPackedFloat(Precision);
                        return;
                    case FileFieldDataFormat.String:
                        float ret;
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (!float.TryParse(StringValue?.Trim(), out ret))
                            {
                                Value = null;
                                return;
                            }
                        }
                        else
                        {
                            ret = float.Parse(StringValue.Trim());
                        }
                        if (Precision != 0 && !StringValue.Contains("."))
                        {
                            ret /= (float)Math.Pow(10, Precision);
                        }
                        Value = ret;
                        return;
                }
            }
            else if (Type == typeof(ushort) || Type == typeof(ushort?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        Value = ByteValue.FromPackedUShort();
                        return;
                    case FileFieldDataFormat.String:
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (ushort.TryParse(StringValue?.Trim(), out ushort ret))
                            {
                                Value = ret;
                            }
                            else
                            {
                                Value = null;
                            }
                        }
                        else
                        {
                            Value = ushort.Parse(StringValue.Trim());
                        }
                        return;
                }
            }
            else if (Type == typeof(uint) || Type == typeof(uint?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        Value = ByteValue.FromPackedUInt();
                        return;
                    case FileFieldDataFormat.String:
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (uint.TryParse(StringValue?.Trim(), out uint ret))
                            {
                                Value = ret;
                            }
                            else
                            {
                                Value = null;
                            }
                        }
                        else
                        {
                            Value = uint.Parse(StringValue.Trim());
                        }
                        return;
                }
            }
            else if (Type == typeof(ulong) || Type == typeof(ulong?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        Value = ByteValue.FromPackedULong();
                        return;
                    case FileFieldDataFormat.String:
                        if (Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (ulong.TryParse(StringValue?.Trim(), out ulong ret))
                            {
                                Value = ret;
                            }
                            else
                            {
                                Value = null;
                            }
                        }
                        else
                        {
                            Value = ulong.Parse(StringValue.Trim());
                        }
                        return;
                }
            }
            Value = ByteValue.Convert(Encoder.SourceEncoding, Encoder.DestinationEncoding).ToObject();
        }

        /// <summary>
        /// Packs the field <see cref="IFileField.Value">Value</see> to the <see cref="ByteValue">ByteValue</see> using the properties provided.
        /// </summary>
        public override byte[] Pack()
        {
            if (Value == null)
            {
                return Pad();
            }
            if (Type == typeof(byte) || Type == typeof(byte?))
            {
                byteValue = new byte[] { (byte)Value };
            }
            else if (DataFormat == FileFieldDataFormat.Raw)
            {
                byteValue = Value.ToByteArray(Encoder.DestinationEncoding);
            }
            else if (Type == typeof(DateTime) || Type == typeof(DateTime?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        byteValue = ((DateTime)Value).PackComp3(Length < 4);
                        break;
                    case FileFieldDataFormat.String:
                        byteValue = ((DateTime)Value).ToFormattedDateString(Length.ToInt()).ToByteArray(Encoder.DestinationEncoding);
                        break;
                }
            }
            else if (Type == typeof(bool) || Type == typeof(bool?))
            {
                switch (DataFormat)
                {
                    case FileFieldDataFormat.IBMPacked:
                        byteValue = new byte[] { (byte)(((bool)Value) ? 1 : 0) };
                        break;
                    case FileFieldDataFormat.String:
                        if (Value == null)
                        {
                            byteValue = new byte[] { FillByte };
                        }
                        else
                        {
                            string sVal = ((bool)Value).ToString(BoolAsString);
                            if (sVal.Length > Length)
                            {
                                sVal = sVal.Substring(0, Length.ToInt());
                            }
                            byteValue = sVal.ToByteArray(Encoder.DestinationEncoding);
                        }
                        break;
                }
            }
            else if (Type == typeof(string) || DataFormat == FileFieldDataFormat.String)
            {
                byteValue = Value?.ToString().ToByteArray(Encoder.DestinationEncoding);
            }
            else if (DataFormat == FileFieldDataFormat.IBMPacked)
            {
                if (Type == typeof(double) || Type == typeof(double?) || Type == typeof(float) || Type == typeof(float?))
                {
                    double dVal = Convert.ToDouble(Value);
                    byteValue = dVal.PackComp3(Precision);
                    this.Type = this.Type;
                }
                else
                {
                    decimal dVal;
                    if (Value == null)
                    {
                        dVal = FillByte;
                    }
                    else if (Type == typeof(short) || Type == typeof(short?))
                    {
                        dVal = Convert.ToInt16(Value);
                    }
                    else if (Type == typeof(int) || Type == typeof(int?))
                    {
                        dVal = Convert.ToInt32(Value);
                    }
                    else if (Type == typeof(long) || Type == typeof(long?))
                    {
                        dVal = Convert.ToInt64(Value);
                    }
                    else if (Type == typeof(ushort) || Type == typeof(ushort?))
                    {
                        dVal = Convert.ToUInt16(Value);
                    }
                    else if (Type == typeof(uint) || Type == typeof(uint?))
                    {
                        dVal = Convert.ToUInt32(Value);
                    }
                    else if (Type == typeof(ulong) || Type == typeof(ulong?))
                    {
                        dVal = Convert.ToUInt64(Value);
                    }
                    else
                    {
                        dVal = Convert.ToDecimal(Value);
                    }
                    byteValue = dVal.PackComp3(Precision);
                }
            }
            return Pad();
        }

        private byte[] Pad()
        {
            if (Value == null)
            {
                byteValue = FillByte.NewArray(Length.ToInt()).Convert(Encoder.SourceEncoding, Encoder.DestinationEncoding);
            }
            if (byteValue.Length > Length)
            {
                throw new InvalidCastException(
                    $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} {Value}");
            }
            if (byteValue.Length == Length)
            {
                return byteValue;
            }
            byte fb = FillByte.Convert(Encoder.SourceEncoding, Encoder.DestinationEncoding);
            if (DataFormat == FileFieldDataFormat.IBMPacked)
            {
                byteValue = byteValue.PadLeft(Length.ToInt(), fb, true);
            }
            else if (RightAlign)
            {
                byteValue = byteValue.PadLeft(Length.ToInt(), fb, true);
            }
            else
            {
                byteValue = byteValue.PadRight(Length.ToInt(), fb, true);
            }
            return byteValue;
        }

    }
}
