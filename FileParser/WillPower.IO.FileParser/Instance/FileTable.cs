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

using System.Collections.Generic;
using System.Linq;

namespace WillPower
{
    /// <summary>
    /// A table field that functions more or less like a miniature file reader within a record.
    /// Can be inherited.
    /// </summary>
    [System.Serializable]
    public class FileTable : IFileField
    {
        private readonly List<IFileRecord> records;

        /// <summary>
        /// The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.
        /// </summary>
        public IFileParserEncoder Encoder { get; set; }
        /// <summary>
        /// The starting position of this field in the <see cref="IFileRecord">record</see> expressed as an <see cref="System.UInt32">unsigned integer</see>.
        /// </summary>
        public uint StartPosition { get; set; }
        /// <summary>
        /// The table length expressed as an <see cref="System.UInt32">unsigned integer</see>.
        /// </summary>
        public uint Length { get; set; }
        /// <summary>
        /// The <see cref="System.String">name</see> of the table.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The <see cref="FileFieldDataFormat">format</see> of Table (3).
        /// </summary>
        public FileFieldDataFormat DataFormat { get { return FileFieldDataFormat.Table; } set { } }
        /// <summary>
        /// The <see cref="System.Byte">byte</see>, in Source Encoding, to use as filler for this field.
        /// This or <see cref="FillCharacter">FillCharacter</see> must be set, but not both. Changing one 
        /// will affect the other. Not needed on <see cref="FileTable">File Table</see>.
        /// </summary>
        public byte FillByte { get; set; }
        /// <summary>
        /// The <see cref="System.Char">character</see>, in Source Encoding, to use as filler for this field.
        /// This or <see cref="FillByte">FillByte</see> must be set, but not both. Changing one 
        /// will affect the other. Not needed on <see cref="FileTable">File Table</see>.
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
        /// Not needed on <see cref="FileTable">File Table</see>.
        /// </summary>
        public bool RightAlign { get; set; }
        /// <summary>
        /// Identifies how a <see cref="System.Boolean">boolean</see> value is represented as a <see cref="System.String">string</see>.
        /// Not used on this instance.
        /// </summary>
        public BooleanStringRepresentation BoolAsString { get; set; }
        /// <summary>
        /// The <see cref="IFileLayout">layout</see> of the table.
        /// </summary>
        public IFileLayout Layout { get; set; }
        /// <summary>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="IFileRecord">records</see> read from the table.
        /// It is suggested that this collection not be altered directly, but rather assigned to using the 
        /// <see cref="AddRecord(IFileRecord)">AddRecord</see> and <see cref="AddRecords(IFileRecord[])">AddRecords</see> methods.
        /// </summary>
        public IEnumerable<IFileRecord> Records 
        { 
            get 
            { 
                return records.ToArray(); 
            }
            set 
            {
                records.Clear();
                records.AddRange(value);
            }
        }
        /// <summary>
        /// The <see cref="IFileField.Compute(byte[])">computed</see> value boxed as an <see cref="System.Object">object</see>.
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// The <see cref="System.Type">type</see> of this instance.
        /// </summary>
        public System.Type Type
        {
            get
            {
                return GetType();
            }
            set { }
        }
        /// <summary>
        /// Not used.
        /// </summary>
        public short Precision { get; set; }
        /// <summary>
        /// Not used.
        /// </summary>
        public bool YearFirst { get; set; }
        /// <summary>
        /// The original, source <see cref="System.Text.Encoding">Encoded</see> <see cref="System.Array">array</see> of 
        /// <see cref="System.Byte">bytes</see> belonging to this field.
        /// </summary>
        public byte[] ByteValue { get; set; }
        /// <summary>
        /// The <see cref="IFileField.Compute(byte[])">computed</see> result as a <see cref="System.String">string</see>.
        /// For this object, this field will not set or return viable results.
        /// </summary>
        public string StringValue { get; set; }

        /// <summary>
        /// .ctor. Creates a new instance of FileTable.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        public FileTable(IFileParserEncoder encoder)
        {
            records = new List<IFileRecord>();
            Encoder = encoder;
        }
        /// <summary>
        /// .ctor. Creates a new instance of FileTable.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        /// <param name="name">The <see cref="System.String">name</see> of the table.</param>
        /// <param name="startPosition">The starting position of this table in the <see cref="IFileRecord">record</see> 
        /// expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="length">The field (table) length expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        public FileTable(IFileParserEncoder encoder, string name, uint startPosition, uint length) : this(encoder)
        {
            StartPosition = startPosition;
            Length = length;
            Name = name;
        }

        /// <summary>
        /// Assigns the provided collection of <see cref="IFileRecord">records</see> to the table.
        /// </summary>
        /// <param name="rows">The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of 
        /// <see cref="IFileRecord">records</see> to assign to this table.</param>
        public void SetRecords(IFileRecord[] rows)
        {
            records.Clear();
            records.AddRange(rows);
        }

        /// <summary>
        /// Adds the provided collection of <see cref="IFileRecord">records</see> to the <see cref="Records">Records</see> collection.
        /// </summary>
        /// <param name="rows">The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of 
        /// <see cref="IFileRecord">records</see> to add to this table.</param>
        public void AddRecords(IFileRecord[] rows)
        {
            records.AddRange(rows);
        }

        /// <summary>
        /// Adds the provided <see cref="IFileRecord">record</see> to the <see cref="Records">Records</see> collection.
        /// </summary>
        /// <param name="row">The <see cref="IFileRecord">record</see> to add to this table.</param>
        public void AddRecord(IFileRecord row)
        {
            records.Add(row);
        }

        /// <summary>
        /// Computes the provided <see cref="System.Array">array</see> of bytes using the properties provided.
        /// </summary>
        /// <param name="data">The <see cref="System.Array">array</see> of bytes to process.</param>
        public void Compute(byte[] data = null)
        {
            ByteValue = data;
            if (data == null || data.Length < 1)
            {
                Value = null;
                return;
            }
            var bytes = data.ToList();
            int skip = 0;
            while (skip < data.Length - 1)
            {
                records.Add(new FileRecord(Layout.MasterFields, bytes.Skip(skip).Take(Layout.RecordLength.ToInt()).ToArray()));
                skip += Layout.RecordLength.ToInt();
            }
            Value = this;
        }

        /// <summary>
        /// Creates a <see cref="System.Data.DataTable">DataTable</see> from the resulting <see cref="IFileRecord">records</see>.
        /// </summary>
        /// <returns>A <see cref="System.Data.DataTable">DataTable</see> of the <see cref="IFileRecord">records</see> computed.</returns>
        public System.Data.DataTable ToDataTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable(Name);
            foreach (IFileField fld in Layout.MasterFields)
            {
                dt.Columns.Add(fld.Name, fld.Type);
            }
            foreach (IFileRecord rec in Records)
            {
                System.Data.DataRow dr = dt.NewRow();
                foreach (IFileField fld in rec.Fields)
                {
                    dr[fld.Name] = fld.Value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// Packs the <see cref="Records">Records</see> and their <see cref="IFileRecord.Fields">Fields</see> 
        /// using the properties provided.
        /// </summary>
        public byte[] Pack()
        {
            List<byte> bytes = new List<byte>();
            if (Layout.HeaderRecord?.Fields?.Length > 0)
            {
                bytes.AddRange(Layout.HeaderRecord.Pack(Length, FillByte));
            }
            foreach (var record in Records)
            {
                bytes.AddRange(record.Pack(Length, FillByte));
            }
            if (Layout.FooterRecord?.Fields?.Length > 0)
            {
                bytes.AddRange(Layout.FooterRecord.Pack(Length, FillByte));
            }
            if (Length > bytes.Count)
            {
                bytes = bytes.ToArray().PadRight(Length.ToInt(), 
                    FillByte.Convert(Encoder.SourceEncoding, Encoder.DestinationEncoding), true).ToList();
            }
            ByteValue = bytes.ToArray();
            return ByteValue;
        }

        /// <summary>
        /// Assign values using the <see cref="Records">Records</see> collection of this object. 
        /// Do not use this method directly. 
        /// </summary>
        /// <param name="value">Not Used.</param>
        /// <exception cref="System.ArgumentException">Thrown Always.</exception>
        [System.Obsolete("Do not use this method on this object.")]
        public void SetValue(object value)
        {
            throw new System.ArgumentException();
        }

    }
}
