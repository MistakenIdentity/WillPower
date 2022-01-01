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

namespace WillPower
{
    /// <summary>
    /// A common base class containing the necessary properties and methods for processing a single field.
    /// Must be inherited.
    /// </summary>
    [Serializable]
    public abstract class FileFieldBase : IFileField
    {
        /// <summary>
        /// The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.
        /// </summary>
        public IFileParserEncoder Encoder { get; set; }
        /// <summary>
        /// The starting position of this field in the <see cref="IFileRecord">record</see> expressed as an 
        /// <see cref="System.UInt32">unsigned integer</see>.
        /// </summary>
        public uint StartPosition { get; set; }
        /// <summary>
        /// The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.
        /// </summary>
        public uint Length { get; set; }
        /// <summary>
        /// The <see cref="System.String">name</see> of the field.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The <see cref="FileFieldDataFormat">format</see> of the field.
        /// String = 0, Comp3 (IBM Packed Number) = 1, Raw (binary) = 2.
        /// </summary>
        public FileFieldDataFormat DataFormat { get; set; }
        /// <summary>
        /// The <see cref="IFileField.Compute(byte[])">computed</see> value boxed as an <see cref="System.Object">object</see>.
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// The <see cref="System.Type">type</see> of data the field contains.
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// The precision of the value if decimal.
        /// </summary>
        public short Precision { get; set; }
        /// <summary>
        /// If <see cref="System.Boolean">true</see> and the <see cref="IFileField.DataFormat">DataFormat</see> is set to String (0), 
        /// will attempt to read the date value with year at the beginning.
        /// </summary>
        public bool YearFirst { get; set; }
        /// <summary>
        /// The <see cref="System.Byte">byte</see>, in Source Encoding, to use as filler for this field.
        /// This or <see cref="FillCharacter">FillCharacter</see> must be set, but not both. Changing one 
        /// will affect the other. Used for writing.
        /// </summary>
        public byte FillByte { get; set; }
        /// <summary>
        /// The <see cref="System.Char">character</see>, in Source Encoding, to use as filler for this field.
        /// This or <see cref="FillByte">FillByte</see> must be set, but not both. Changing one 
        /// will affect the other. Used for writing.
        /// </summary>
        public char FillCharacter 
        {
            get
            {
                return (new byte[] { FillByte }).ToEncodedString(Encoder.SourceEncoding, Encoder.SourceEncoding).ToCharArray()[0];
            }
            set
            {
                FillByte = value.ToString().ToByteArray(Encoder.SourceEncoding)[0];
            }
        }
        /// <summary>
        /// If <see cref="System.Boolean">true</see> the field will pad the <see cref="FillByte">FillByte</see> 
        /// or <see cref="FillCharacter">FillCharacter</see> to the left.
        /// If <see cref="System.Boolean">false</see> the field will pad the <see cref="FillByte">FillByte</see> 
        /// or <see cref="FillCharacter">FillCharacter</see> to the right.
        /// </summary>
        public bool RightAlign { get; set; }
        /// <summary>
        /// The original, source <see cref="System.Text.Encoding">Encoded</see> <see cref="System.Array">array</see> of bytes belonging to this field.
        /// </summary>
        public abstract byte[] ByteValue { get; set; }
        /// <summary>
        /// The <see cref="IFileField.Compute(byte[])">computed</see> result as a <see cref="System.String">string</see> or 
        /// the value to be written.
        /// </summary>
        public abstract string StringValue { get; set; }
        /// <summary>
        /// Identifies how a <see cref="System.Boolean">boolean</see> value is represented as a <see cref="System.String">string</see>.
        /// Length of 1 will only return the first <see cref="System.Char">character</see> of the expected <see cref="System.String">string</see>.
        /// </summary>
        public BooleanStringRepresentation BoolAsString { get; set; }

        /// <summary>
        /// .ctor. Must be inherited.
        /// </summary>
        /// <param name="write">
        /// If <see cref="System.Boolean">true</see>, this will use the default ASCII to EBCDIC 
        /// <see cref="IFileParserEncoder">IFileParserEncoder</see> instance for writing binary EBCDIC fields.
        /// If <see cref="System.Boolean">false</see>, this will use the default EBCDIC to ASCII 
        /// <see cref="IFileParserEncoder">IFileParserEncoder</see> instance for reading binary EBCDIC fields.
        /// Default is <see cref="System.Boolean">false</see>.
        /// </param>
        public FileFieldBase(bool write = false)
        {
            Encoder = FileParserEncoder.GetDefaultEncoder(write);
        }
        /// <summary>
        /// .ctor. Must be inherited.
        /// </summary>
        /// <param name="clone">The source <see cref="IFileField">field</see> to clone.</param>
        public FileFieldBase(IFileField clone)
        {
            Encoder = clone.Encoder;
            Length = clone.Length;
            Name = clone.Name;
            Precision = clone.Precision;
            StartPosition = clone.StartPosition;
            DataFormat = clone.DataFormat;
            Type = clone.Type;
            DataFormat = clone.DataFormat;
            RightAlign = clone.RightAlign;
            FillByte = clone.FillByte;
            BoolAsString = clone.BoolAsString;
        }
        /// <summary>
        /// .ctor. Must be inherited.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        /// <param name="type">The <see cref="System.Type">type</see> of data the field contains.</param>
        /// <param name="name">The <see cref="System.String">name</see> of the field.</param>
        /// <param name="startPosition">The starting position of this field in the <see cref="IFileRecord">record</see> expressed as an 
        /// <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="length">The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="format">The <see cref="FileFieldDataFormat">format</see> of the field.</param>
        public FileFieldBase(IFileParserEncoder encoder, Type type, string name, uint startPosition, uint length, FileFieldDataFormat format)
        {
            Encoder = encoder;
            Type = type;
            Name = name;
            StartPosition = startPosition;
            Length = length;
            DataFormat = format;
        }
        /// <summary>
        /// .ctor. Must be inherited. Used for numeric <see cref="System.Decimal">(decimal)</see> <see cref="System.Type">types</see>.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        /// <param name="name">The <see cref="System.String">name</see> of the field.</param>
        /// <param name="startPosition">The starting position of this field in the <see cref="IFileRecord">record</see> expressed as an 
        /// <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="length">The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="format">The <see cref="FileFieldDataFormat">format</see> of the field.</param>
        /// <param name="precision">The precision of the expected resultant as <see cref="System.Int16"/>short. Default is 0. 
        /// The limitation is from logical reasons.</param>
        /// <param name="required">If <see cref="System.Boolean">true</see>, throw a <see cref="FieldException"></see> on a null result.</param>
        public FileFieldBase(IFileParserEncoder encoder, string name, uint startPosition, uint length, FileFieldDataFormat format, 
            short precision, bool required = false) 
            : this(encoder, required ? typeof(decimal) : typeof(decimal?), name, startPosition, length, format)
        {
            Precision = precision;
        }
        /// <summary>
        /// .ctor. Must be inherited. Used for <see cref="System.DateTime">Date</see> <see cref="System.Type">types</see>.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        /// <param name="name">The <see cref="System.String">name</see> of the field.</param>
        /// <param name="startPosition">The starting position of this field in the <see cref="IFileRecord">record</see> expressed as an 
        /// <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="length">The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="format">The <see cref="FileFieldDataFormat">format</see> of the field.</param>
        /// <param name="yearFirst">If <see cref="System.Boolean">true</see> it assumes the year is the first part of the string. Default is 
        /// <see cref="System.Boolean">false</see>.
        /// Note: If the <see cref="System.String">string</see> contains a parsable date character ( / or - ) it will attempt to use 
        /// <see cref="System.DateTime.TryParse(string, out System.DateTime)">TryParse</see> and return any successful result. 
        /// If <see cref="System.DateTime.TryParse(string, out System.DateTime)">TryParse</see> fails it proceeds on to 
        /// <see cref="System.String">string</see> parsing.
        /// </param>
        /// <param name="required">If <see cref="System.Boolean">true</see>, throw a <see cref="FieldException"></see> on a null result.</param>
        public FileFieldBase(IFileParserEncoder encoder, string name, uint startPosition, uint length, FileFieldDataFormat format, 
            bool yearFirst, bool required = false)
            : this(encoder, required ? typeof(DateTime) : typeof(DateTime?), name, startPosition, length, format)
        {
            YearFirst = yearFirst;
        }
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
        public FileFieldBase(IFileParserEncoder encoder, string name, uint startPosition, uint length, BooleanStringRepresentation showAs)
            : this(encoder, typeof(bool?), name, startPosition, length, FileFieldDataFormat.String)
        {
            BoolAsString = showAs;
        }

        /// <summary>
        /// Computes this provided <see cref="System.Array">array</see> of bytes using the properties provided.
        /// </summary>
        /// <param name="data">The <see cref="System.Array">array</see> of bytes to process.</param>
        public abstract void Compute(byte[] data = null);

        /// <summary>
        /// Packs the field <see cref="Value">Value</see> to the <see cref="ByteValue">ByteValue</see> using the properties provided.
        /// </summary>
        public abstract byte[] Pack();

        /// <summary>
        /// Set the value of this field to the provided <see cref="System.Object">value</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.Object">value</see> to assign to this field.</param>
        public void SetValue(object value)
        {
            Value = value;
        }

    }
}
