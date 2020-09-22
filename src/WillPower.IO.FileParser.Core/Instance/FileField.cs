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
    /// A concreate class containing the necessary properties and methods for processing a single field.
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
        byte[] byteValue;
        /// <summary>
        /// The <see cref="IFileField.Compute(byte[])">computed</see> result as a <see cref="System.String">string</see>.
        /// </summary>
        public override string StringValue
        {
            get
            {
                return this.ByteValue.ToEncodedString(this.Encoder.SourceEncoding, this.Encoder.DestinationEncoding);
            }
        }

        /// <summary>
        /// .ctor. Creates a new instance of FileField.
        /// </summary>
        public FileField() : base()
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
            this.ByteValue = data;
        }
        /// <summary>
        /// .ctor. Creates a new instance of FileField.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        /// <param name="type">The <see cref="System.Type">type</see> of data the field contains.</param>
        /// <param name="name">The <see cref="System.String">name</see> of the field.</param>
        /// <param name="startPosition">The starting position of this field in the <see cref="IFileRecord">record</see> expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="length">The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="format">The <see cref="FileFieldDataFormat">format</see> of the field.</param>
        public FileField(IFileParserEncoder encoder, Type type, string name, uint startPosition, uint length, FileFieldDataFormat format)
        {
            this.Encoder = encoder;
            this.Type = type;
            this.Name = name;
            this.StartPosition = startPosition;
            this.Length = length;
            this.DataFormat = format;
        }
        /// <summary>
        /// .ctor. Creates a new instance of FileField. Used for numeric <see cref="System.Decimal">(decimal)</see> <see cref="System.Type">types</see>.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        /// <param name="name">The <see cref="System.String">name</see> of the field.</param>
        /// <param name="startPosition">The starting position of this field in the <see cref="IFileRecord">record</see> expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="length">The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="format">The <see cref="FileFieldDataFormat">format</see> of the field.</param>
        /// <param name="precision">The precision of the expected resultant as <see cref="System.Byte"/>Byte. Default is 0. The limitation is from logical reasons.</param>
        /// <param name="required">If <see cref="System.Boolean">true</see>, throw a <see cref="FieldException"></see> on a null result.</param>
        public FileField(FileParserEncoder encoder, string name, uint startPosition, uint length, FileFieldDataFormat format, byte precision, bool required = false)
            : base(encoder, name, startPosition, length, format, precision, required)
        { }
        /// <summary>
        /// .ctor. Creates a new instance of FileField. Used for <see cref="System.DateTime">Date</see> <see cref="System.Type">types</see>.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        /// <param name="name">The <see cref="System.String">name</see> of the field.</param>
        /// <param name="startPosition">The starting position of this field in the <see cref="IFileRecord">record</see> expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="length">The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="format">The <see cref="FileFieldDataFormat">format</see> of the field.</param>
        /// <param name="yearFirst">If <see cref="System.Boolean">true</see> it assumes the year is the first part of the string. Default is <see cref="System.Boolean">false</see>.
        /// Note: If the <see cref="System.String">string</see> contains a parsable date character ( / or - ) it will attempt to use 
        /// <see cref="System.DateTime.TryParse(string, out System.DateTime)">TryParse</see> and return any successful result. 
        /// If <see cref="System.DateTime.TryParse(string, out System.DateTime)">TryParse</see> fails it proceeds on to <see cref="System.String">string</see> parsing.
        /// </param>
        /// <param name="required">If <see cref="System.Boolean">true</see>, throw a <see cref="FieldException"></see> on a null result.</param>
        public FileField(FileParserEncoder encoder, string name, uint startPosition, uint length, FileFieldDataFormat format, bool yearFirst, bool required = false)
            : base(encoder, name, startPosition, length, format, yearFirst, required)
        { }

        /// <summary>
        /// Computes this provided <see cref="System.Array">array</see> of bytes using the properties provided.
        /// </summary>
        /// <param name="data">The <see cref="System.Array">array</see> of bytes to process.</param>
        public override void Compute(byte[] data = null)
        {
            if (data != null && data.Length > 0)
            {
                this.ByteValue = data;
                return;
            }
            if (this.ByteValue.Length < 1)
            {
                return;
            }
            if (this.Type == typeof(string))
            {
                this.Value = this.StringValue;
                return;
            }
            if (this.Type == typeof(byte) || this.Type == typeof(byte?))
            {
                switch (this.DataFormat)
                {
                    case FileFieldDataFormat.Comp3:
                        this.Value = this.ByteValue.First();
                        return;
                    case FileFieldDataFormat.Raw:
                        this.Value = this.ByteValue.First();
                        return;
                    case FileFieldDataFormat.String:
                        this.Value = byte.Parse(this.StringValue);
                        return;
                }
            }
            if (this.Type == typeof(DateTime) || this.Type == typeof(DateTime?))
            {
                switch (this.DataFormat)
                {
                    case FileFieldDataFormat.Comp3:
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            this.Value = this.ByteValue.FromPackedDate();
                        }
                        else
                        {
                            this.Value = this.ByteValue.FromPackedDate().Value;
                        }
                        return;
                    case FileFieldDataFormat.Raw:
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            this.Value = this.ByteValue.Convert(this.Encoder.SourceEncoding, this.Encoder.DestinationEncoding).FromRawDate();
                        }
                        else
                        {
                            this.Value = this.ByteValue.Convert(this.Encoder.SourceEncoding, this.Encoder.DestinationEncoding).FromRawDate().Value;
                        }
                        return;
                    case FileFieldDataFormat.String:
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            this.Value = this.StringValue.ToDateTime(this.YearFirst);
                        }
                        else
                        {
                            this.Value = this.StringValue.ToDateTime(this.YearFirst).Value;
                        }
                        return;
                }
            }
            if (this.Type == typeof(bool) || this.Type == typeof(bool?))
            {
                switch (this.DataFormat)
                {
                    case FileFieldDataFormat.Comp3:
                        this.Value = this.ByteValue.Last() > 0;
                        return;
                    case FileFieldDataFormat.Raw:
                        this.Value = this.ByteValue.Last() > 0;
                        return;
                    case FileFieldDataFormat.String:
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            this.Value = this.StringValue.ToBool();
                        }
                        else
                        {
                            this.Value = this.StringValue.ToBool().Value;
                        }
                        return;
                }
            }
            if (this.Type == typeof(short) || this.Type == typeof(short?))
            {
                switch (this.DataFormat)
                {
                    case FileFieldDataFormat.Comp3:
                        this.Value = this.ByteValue.FromPackedShort();
                        return;
                    case FileFieldDataFormat.Raw:
                        this.Value = this.ByteValue.FromRawShort();
                        return;
                    case FileFieldDataFormat.String:
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (short.TryParse(this.StringValue.Trim(), out short ret))
                            {
                                this.Value = ret;
                            }
                            else
                            {
                                this.Value = null;
                            }
                        }
                        else
                        {
                            this.Value = short.Parse(this.StringValue.Trim());
                        }
                        return;
                }
            }
            if (this.Type == typeof(int) || this.Type == typeof(int?))
            {
                switch (this.DataFormat)
                {
                    case FileFieldDataFormat.Comp3:
                        this.Value = this.ByteValue.FromPackedInt();
                        return;
                    case FileFieldDataFormat.Raw:
                        this.Value = this.ByteValue.FromRawInt();
                        return;
                    case FileFieldDataFormat.String:
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (int.TryParse(this.StringValue.Trim(), out int ret))
                            {
                                this.Value = ret;
                            }
                            else
                            {
                                this.Value = null;
                            }
                        }
                        else
                        {
                            this.Value = int.Parse(this.StringValue.Trim());
                        }
                        return;
                }
            }
            if (this.Type == typeof(long) || this.Type == typeof(long?))
            {
                switch (this.DataFormat)
                {
                    case FileFieldDataFormat.Comp3:
                        this.Value = this.ByteValue.FromPackedLong();
                        return;
                    case FileFieldDataFormat.Raw:
                        this.Value = this.ByteValue.FromRawLong();
                        return;
                    case FileFieldDataFormat.String:
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (long.TryParse(this.StringValue.Trim(), out long ret))
                            {
                                this.Value = ret;
                            }
                            else
                            {
                                this.Value = null;
                            }
                        }
                        else
                        {
                            this.Value = long.Parse(this.StringValue.Trim());
                        }
                        return;
                }
            }
            if (this.Type == typeof(double) || this.Type == typeof(double?))
            {
                switch (this.DataFormat)
                {
                    case FileFieldDataFormat.Comp3:
                        this.Value = this.ByteValue.FromPackedDouble();
                        return;
                    case FileFieldDataFormat.Raw:
                        this.Value = this.ByteValue.FromRawDouble();
                        return;
                    case FileFieldDataFormat.String:
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (double.TryParse(this.StringValue.Trim(), out double ret))
                            {
                                this.Value = ret;
                            }
                            else
                            {
                                this.Value = null;
                            }
                        }
                        else
                        {
                            this.Value = long.Parse(this.StringValue.Trim());
                        }
                        return;
                }
            }
            if (this.Type == typeof(double) || this.Type == typeof(double?))
            {
                switch (this.DataFormat)
                {
                    case FileFieldDataFormat.Comp3:
                        this.Value = this.ByteValue.FromPackedDecimal(this.Precision);
                        return;
                    case FileFieldDataFormat.Raw:
                        this.Value = this.ByteValue.FromRawDecimal(this.Precision);
                        return;
                    case FileFieldDataFormat.String:
                        decimal ret;
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (!decimal.TryParse(this.StringValue.Trim(), out ret))
                            {
                                this.Value = null;
                                return;
                            }
                         }
                        else
                        {
                            ret = decimal.Parse(this.StringValue.Trim());
                        }
                        if (this.Precision != 0 && !this.StringValue.Contains("."))
                        {
                            ret /= (decimal)Math.Pow(10, this.Precision);
                        }
                        this.Value = ret;
                        return;
                }
            }
            if (this.Type == typeof(ushort) || this.Type == typeof(ushort?))
            {
                switch (this.DataFormat)
                {
                    case FileFieldDataFormat.Comp3:
                        this.Value = this.ByteValue.FromPackedUShort();
                        return;
                    case FileFieldDataFormat.Raw:
                        this.Value = this.ByteValue.FromRawUShort();
                        return;
                    case FileFieldDataFormat.String:
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (ushort.TryParse(this.StringValue.Trim(), out ushort ret))
                            {
                                this.Value = ret;
                            }
                            else
                            {
                                this.Value = null;
                            }
                        }
                        else
                        {
                            this.Value = ushort.Parse(this.StringValue.Trim());
                        }
                        return;
                }
            }
            if (this.Type == typeof(uint) || this.Type == typeof(uint?))
            {
                switch (this.DataFormat)
                {
                    case FileFieldDataFormat.Comp3:
                        this.Value = this.ByteValue.FromPackedUInt();
                        return;
                    case FileFieldDataFormat.Raw:
                        this.Value = this.ByteValue.FromRawUInt();
                        return;
                    case FileFieldDataFormat.String:
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (uint.TryParse(this.StringValue.Trim(), out uint ret))
                            {
                                this.Value = ret;
                            }
                            else
                            {
                                this.Value = null;
                            }
                        }
                        else
                        {
                            this.Value = uint.Parse(this.StringValue.Trim());
                        }
                        return;
                }
            }
            if (this.Type == typeof(ulong) || this.Type == typeof(ulong?))
            {
                switch (this.DataFormat)
                {
                    case FileFieldDataFormat.Comp3:
                        this.Value = this.ByteValue.FromPackedULong();
                        return;
                    case FileFieldDataFormat.Raw:
                        this.Value = this.ByteValue.FromRawULong();
                        return;
                    case FileFieldDataFormat.String:
                        if (this.Type.IsAssignableFrom(typeof(Nullable)))
                        {
                            if (ulong.TryParse(this.StringValue.Trim(), out ulong ret))
                            {
                                this.Value = ret;
                            }
                            else
                            {
                                this.Value = null;
                            }
                        }
                        else
                        {
                            this.Value = ulong.Parse(this.StringValue.Trim());
                        }
                        return;
                }
            }
            this.Value = this.ByteValue.Convert(this.Encoder.SourceEncoding, this.Encoder.DestinationEncoding).ToObject();
        }

    }
}
