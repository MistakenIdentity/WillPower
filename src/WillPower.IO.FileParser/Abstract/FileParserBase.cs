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
    /// A common base class for a file parsing engine. Must be inherited.
    /// </summary>
    [Serializable]
    public abstract class FileParserBase : IFileParser, IDisposable
    {
        /// <summary>
        /// The <see cref="WillPower.TaskManager">TaskManager</see> instance managing threads.
        /// </summary>
        public TaskManager TaskManager { get; set; }

        /// <summary>
        /// Gets a quick determination on whether a <see cref="System.Array">collection</see> of <see cref="IFileConditional">conditions</see> exist.
        /// </summary>
        protected bool IsConditional
        {
            get
            {
                return Layout.Conditions != null && Layout.Conditions.Length > 0;
            }
        }

        /// <summary>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="IFileRecord">records</see> read from the file.
        /// Must be implemented.
        /// </summary>
        public abstract IEnumerable<IFileRecord> Records { get; }

        /// <summary>
        /// The <see cref="IFileLayout">layout</see> of the file, including any <see cref="IFileLayout.HeaderRecord">header</see>, <see cref="IFileLayout.FooterRecord">footer</see>, or <see cref="IFileLayout.Conditions">conditional</see> rows.
        /// </summary>
        public IFileLayout Layout { get; set; }

        /// <summary>
        /// private .ctor.
        /// </summary>
        private FileParserBase()
        {
            this.TaskManager = new TaskManager
            {
                TimeOutAll = TimeSpan.FromHours(4)
            };
        }
        /// <summary>
        /// .ctor. Must be inherited.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        public FileParserBase(IFileParserEncoder encoder) : this()
        {
            this.Layout = new FileLayout(encoder);
        }
        /// <summary>
        /// .ctor. Must be inherited.
        /// </summary>
        /// <param name="layout">The <see cref="IFileLayout">layout</see> instance to use for processing.</param>
        public FileParserBase(IFileLayout layout) : this()
        {
            this.Layout = layout;
        }

        /// <summary>
        /// Load the provided file as specified by <see cref="System.String">fileName</see> into the parser.
        /// </summary>
        /// <param name="fileName">The <see cref="System.String">name</see> of the file to load.</param>
        public void LoadFile(string fileName)
        {
            LoadStream(new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read));
        }

        /// <summary>
        /// Load the provided <see cref="System.IO.Stream">stream</see> into the parser.
        /// </summary>
        /// <param name="stream">The <see cref="System.IO.Stream">stream</see> to load.</param>
        public abstract void LoadStream(System.IO.Stream stream);

        /// <summary>
        /// Updates any <see cref="IFileParserEncoder">encoders</see> within the system
        /// </summary>
        protected void UpdateEncoders()
        {
            foreach (IFileField field in this.Layout.MasterFields)
            {
                field.Encoder = this.Layout.Encoder;
            }
            if (this.IsConditional)
            {
                foreach (IFileConditional condition in this.Layout.Conditions)
                {
                    foreach (IFileField field in condition.Fields)
                    {
                        field.Encoder = this.Layout.Encoder;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a <see cref="System.Data.DataSet">DataSet</see> containing <see cref="System.Data.DataTable">DataTables</see> 
        /// for each conditional record type, header, footer, and/or master recordset.
        /// </summary>
        /// <returns>A <see cref="System.Data.DataSet">DataSet</see> of <see cref="System.Data.DataTable">DataTables</see> with resulting data.</returns>
        public System.Data.DataSet ToDataSet()
        {
            System.Data.DataSet ds = new System.Data.DataSet(this.Layout.Name);
            if (this.Layout.Conditions != null)
            {
                foreach (IFileConditional condition in this.Layout.Conditions)
                {
                    ds.Tables.Add(GetTable(this.Records.Where(x => condition.Condition(x)).ToList(), condition.Name));
                }
                ds.Tables.Add(GetTable(this.Records.Where(x => !this.Layout.Conditions.Any(y => y.Condition(x))).ToList()));
            }
            else
            {
                ds.Tables.Add(GetTable(this.Records.ToList()));
            }
            return ds;
        }

        /// <summary>
        /// Safely dispose of this instance.
        /// </summary>
        public void Dispose()
        {
            this.TaskManager.Dispose();
        }

        private System.Data.DataTable GetTable(List<IFileRecord> records, string name = null)
        {
            if (records == null || records.Count < 1)
            {
                return null;
            }
            System.Data.DataTable dt = new System.Data.DataTable(name);
            foreach (IFileField field in records[0].Fields)
            {
                if (field.Type == typeof(FileTable) || field.Type.IsAssignableFrom(typeof(FileTable)))
                {
                    dt.Columns.Add(field.Name, typeof(System.Data.DataTable));
                }
                else
                {
                    dt.Columns.Add(field.Name, field.Type);
                }
            }
            foreach (IFileRecord rec in records)
            {
                System.Data.DataRow dr = dt.NewRow();
                foreach (IFileField field in rec.Fields)
                {
                    if (field.Type == typeof(FileTable) || field.Type.IsAssignableFrom(typeof(FileTable)))
                    {
                        dr[field.Name] = (field as FileTable).ToDataTable();
                    }
                    else
                    {
                        dr[field.Name] = field.Value;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

    }
}
