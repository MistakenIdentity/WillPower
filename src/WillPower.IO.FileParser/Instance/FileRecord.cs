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
using System.Collections.Generic;
using System.Linq;

namespace WillPower
{
    /// <summary>
    /// A concrete class for record-level data, their <see cref="IFileField">fields</see>, and any possible <see cref="FieldException">Exceptions</see>.
    /// </summary>
    [Serializable]
    public sealed class FileRecord : FileRecordBase, IFileRecord
    {
        readonly List<FieldException> invalids;

        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="FieldException">Exceptions</see>, if any.
        /// </summary>
        public override FieldException[] Exceptions
        {
            get
            {
                return invalids.ToArray();
            }
        }

        /// <summary>
        /// .ctor. Creates a new instance of FileRecord.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to utilize for processing.</param>
        public FileRecord(IFileParserEncoder encoder) : base(encoder)
        {
            invalids = new List<FieldException>();
        }
        /// <summary>
        /// .ctor. Creates a new instance of FileRecord. Will use the <see cref="IFileParserEncoder">IFileParserEncoder</see> 
        /// instance of the first <see cref="IFileField">field</see> (Fields[0]).
        /// </summary>
        /// <param name="fields">
        /// The <see cref="System.Array">collection</see> of <see cref="IFileField">fields</see> belonging to this <see cref="IFileRecord">record</see>.
        /// </param>
        public FileRecord(IFileField[] fields) : this(fields[0].Encoder)
        {
            List<IFileField> myfields = new List<IFileField>();
            foreach (var field in fields)
            {
                myfields.Add(new FileField(field));
            }
            this.Fields = myfields.ToArray();
        }
        /// <summary>
        /// .ctor. Creates a new instance of FileRecord. Will use the <see cref="IFileParserEncoder">IFileParserEncoder</see> 
        /// instance of the first <see cref="IFileField">field</see> (Fields[0]).
        /// </summary>
        /// <param name="fields">
        /// The <see cref="System.Array">collection</see> of <see cref="IFileField">fields</see> belonging to this <see cref="IFileRecord">record</see>.
        /// </param>
        /// <param name="data">The <see cref="System.Array">array</see> of bytes to <see cref="IFileRecord.ReadRecord(byte[])">read</see> into this instance.</param>
        public FileRecord(IFileField[] fields, byte[] data) : this(fields)
        {
            ReadRecord(data);
        }
        /// <summary>
        /// .ctor. Creates a new instance of FileRecord as a dereferenced copy of the <see cref="IFileRecord">clone</see> provided.
        /// </summary>
        /// <param name="clone">The <see cref="IFileRecord">record</see> to copy as a template for this record (or clone).</param>
        /// <param name="condition"></param>
        /// <param name="data"></param>
        public FileRecord(IFileRecord clone, IFileConditional condition, byte[] data) : base(clone.Encoder)
        {
            List<IFileField> fields = clone.Fields.ToList();
            foreach (IFileField field in condition.Fields.Where(x => !fields.Any(y => y.Name == x.Name)))
            {
                fields.Add(new FileField(field));
            }
            this.Fields = fields.ToArray();
            ReadRecord(data);
        }

        /// <summary>
        /// Reads an <see cref="System.Array">array</see> of bytes into this instance.
        /// </summary>
        /// <param name="data">The <see cref="System.Array">array</see> of bytes to read.</param>
        public override void ReadRecord(byte[] data)
        {
            foreach (IFileField field in this.Fields.Where(x => x.Value == null))
            {
                try
                {
                    field.Compute(data.ToList().GetRange(field.StartPosition.ToInt(), field.Length.ToInt()).ToArray());
                }
                catch (Exception ex)
                {
                    invalids.Add(new FieldException(ex, field));
                }
            }
        }

    }
}
