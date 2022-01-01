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

namespace WillPower
{
    /// <summary>
    /// A class containing the necessary properties for processing a single field.
    /// </summary>
    [System.Serializable]
    public class SerializableField
    {
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
        /// The <see cref="System.Type">type</see> of data the field contains represented as a <see cref="System.String">string</see>.
        /// </summary>
        public string Type { get; set; }
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
        public char FillCharacter { get; set; }
        /// <summary>
        /// If <see cref="System.Boolean">true</see> the field will pad the <see cref="FillByte">FillByte</see> 
        /// or <see cref="FillCharacter">FillCharacter</see> to the left.
        /// If <see cref="System.Boolean">false</see> the field will pad the <see cref="FillByte">FillByte</see> 
        /// or <see cref="FillCharacter">FillCharacter</see> to the right.
        /// </summary>
        public bool RightAlign { get; set; }
        /// <summary>
        /// Identifies how a <see cref="System.Boolean">boolean</see> value is represented as a <see cref="System.String">string</see>.
        /// Length of 1 will only return the first <see cref="System.Char">character</see> of the expected <see cref="System.String">string</see>.
        /// </summary>
        public BooleanStringRepresentation BoolAsString { get; set; }

        /// <summary>
        /// .ctor. Creates a default instance of this object.
        /// </summary>
        public SerializableField()
        { }
        /// <summary>
        /// .ctor. Create an instance of this object using the <see cref="IFileField">field</see> layout provided.
        /// </summary>
        /// <param name="field"></param>
        public SerializableField(IFileField field)
        {
            Name = field.Name;
            Precision = field.Precision;
            StartPosition = field.StartPosition;
            Length = field.Length;
            BoolAsString = field.BoolAsString;
            DataFormat = field.DataFormat;
            RightAlign = field.RightAlign;
            BoolAsString = field.BoolAsString;
            FillByte = field.FillByte;
            FillCharacter = field.FillCharacter;
            YearFirst = field.YearFirst;
            Type = field.Type.ToString();
        }

        /// <summary>
        /// Gets the <see cref="IFileField">IFileField</see> layout definition for this object.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">encoder</see> to use. If null, the default (EBCDIC 2 ASCII) is used.</param>
        /// <returns>An <see cref="IFileField">IFileField</see> object utilizing this object's layout.</returns>
        public IFileField GetField(IFileParserEncoder encoder = null)
        {
            if (DataFormat == FileFieldDataFormat.Table)
            {
                throw new System.InvalidOperationException(
                    $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("UnsupportedType")}");
            }
            IFileParserEncoder defEncoder = encoder ?? FileParserEncoder.GetDefaultEncoder();
            System.Type type = System.Type.GetType(Type);
            if (DataFormat == FileFieldDataFormat.String)
            {
                return new FileField(defEncoder, type, Name, StartPosition, Length, DataFormat)
                {
                    RightAlign = RightAlign,
                    BoolAsString = BoolAsString,
                    FillCharacter = FillCharacter
                };
            }
            else if (type == typeof(System.DateTime) || type == typeof(System.DateTime?))
            {
                return new FileField(defEncoder, Name, StartPosition, Length, DataFormat, YearFirst)
                {
                    RightAlign = RightAlign,
                    BoolAsString = BoolAsString,
                    FillByte = FillByte,
                    YearFirst = YearFirst
                };
            }
            else
            {
                return new FileField(defEncoder, Name, StartPosition, Length, DataFormat, Precision)
                {
                    RightAlign = RightAlign,
                    BoolAsString = BoolAsString,
                    FillByte = FillByte
                };
            }
        }

    }

}
