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
        /// The <see cref="IFileLayout">layout</see> of the table.
        /// </summary>
        public IFileLayout Layout { get; set; }
        /// <summary>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="IFileRecord">records</see> read from the table.
        /// </summary>
        public IEnumerable<IFileRecord> Records { get { return records.ToArray(); } }
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
                return this.GetType();
            }
        }
        /// <summary>
        /// Not used.
        /// </summary>
        public byte Precision { get; set; }
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
        /// For this object, this field will not return viable results.
        /// </summary>
        public string StringValue { get; private set; }

        /// <summary>
        /// .ctor. Creates a new instance of FileTable.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        public FileTable(IFileParserEncoder encoder)
        {
            records = new List<IFileRecord>();
            this.Encoder = encoder;
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

            this.StartPosition = startPosition;
            this.Length = length;
            this.Name = name;
        }

        /// <summary>
        /// Computes the provided <see cref="System.Array">array</see> of bytes using the properties provided.
        /// </summary>
        /// <param name="data">The <see cref="System.Array">array</see> of bytes to process.</param>
        public void Compute(byte[] data = null)
        {
            this.ByteValue = data;
            if (data == null || data.Length < 1)
            {
                this.Value = null;
                return;
            }
            var bytes = data.ToList();
            int skip = 0;
            while (skip < data.Length - 1)
            {
                records.Add(new FileRecord(this.Layout.MasterFields, bytes.Skip(skip).Take(this.Layout.RecordLength.ToInt()).ToArray()));
                skip += this.Layout.RecordLength.ToInt();
            }
            this.Value = this;
        }

        /// <summary>
        /// Creates a <see cref="System.Data.DataTable">DataTable</see> from the resulting <see cref="IFileRecord">records</see>.
        /// </summary>
        /// <returns>A <see cref="System.Data.DataTable">DataTable</see> of the <see cref="IFileRecord">records</see> computed.</returns>
        public System.Data.DataTable ToDataTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable(this.Name);
            foreach (IFileField fld in this.Layout.MasterFields)
            {
                dt.Columns.Add(fld.Name, fld.Type);
            }
            foreach (IFileRecord rec in this.Records)
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
    }
}
