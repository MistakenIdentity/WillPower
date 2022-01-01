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

namespace WillPower
{
    /// <summary>
    /// A concrete class for file parsing. Cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class FileParser : FileParserBase, IFileParser, IDisposable
    {

        private readonly List<IFileRecord> records;

        /// <summary>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="IFileRecord">records</see> read from the file.
        /// It is suggested that this collection not be altered directly, but rather assigned to using the 
        /// <see cref="AddRecord(IFileRecord)">AddRecord</see> and <see cref="AddRecords(IEnumerable{IFileRecord})">AddRecords</see> methods.
        /// </summary>
        public override IEnumerable<IFileRecord> Records => records.ToArray();

        /// <summary>
        /// .ctor. Creates a new instance of FileParser using the default <see cref="FileParserEncoder">FileParserEncoder</see>.
        /// </summary>
        /// <param name="write">
        /// If <see cref="System.Boolean">true</see>, this will use the default ASCII to EBCDIC 
        /// <see cref="IFileParserEncoder">IFileParserEncoder</see> instance for writing binary EBCDIC fields.
        /// If <see cref="System.Boolean">false</see>, this will use the default EBCDIC to ASCII 
        /// <see cref="IFileParserEncoder">IFileParserEncoder</see> instance for reading binary EBCDIC fields.
        /// Default is <see cref="System.Boolean">false</see>.
        /// </param>
        public FileParser(bool write = false) : this(FileParserEncoder.GetDefaultEncoder(write))
        {
            records = new List<IFileRecord>();
        }
        /// <summary>
        /// .ctor. Creates a new instance of FileParser.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        public FileParser(IFileParserEncoder encoder) : base(encoder)
        {
            records = new List<IFileRecord>();
        }
        /// <summary>
        /// .ctor. Creates a new instance of FileParser.
        /// </summary>
        /// <param name="layout">The <see cref="IFileLayout">layout</see> instance to use for processing.</param>
        public FileParser(IFileLayout layout) : base(layout)
        {
            records = new List<IFileRecord>();
        }

        /// <summary>
        /// Load the provided <see cref="System.IO.Stream">stream</see> into the parser.
        /// </summary>
        /// <param name="stream">The <see cref="System.IO.Stream">stream</see> to load.</param>
        public override void LoadStream(System.IO.Stream stream)
        {
            if ((!Layout.OpenAsText && Layout.RecordLength < 1)
                || Layout.MasterFields == null || Layout.MasterFields.Length < 1)
            {
                return;
            }
            UpdateEncoders();
            if (Layout.OpenAsText)
            {
                stream.GotoStart();
                byte[] bytes = stream.ReadToChar(Layout.TextLineTerminator);
                if (Layout.HeaderRecord != null && Layout.HeaderRecord.Fields?.Length > 0)
                {
                    TaskManager.StartAction(delegate
                    {
                        Layout.HeaderRecord.ReadRecord(bytes);
                    });
                }
                else
                {
                    ReadRecord(bytes);
                }
                while (bytes != null)
                {
                    bytes = stream.ReadToChar(Layout.TextLineTerminator, Layout.Encoder.SourceEncoding);
                    if ((stream.Position >= stream.Length - 2 || stream.Position < 0) 
                        && Layout.FooterRecord != null && Layout.FooterRecord.Fields?.Length > 0)
                    {
                        TaskManager.StartAction(delegate
                        {
                            Layout.FooterRecord.ReadRecord(bytes);
                        });
                        bytes = null;
                    }
                    else
                    {
                        ReadRecord(bytes);
                    }
                }
            }
            else if (stream.Length % Layout.RecordLength != 0)
            {
                throw new InvalidOperationException(
                    $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} ({stream.Length}/{Layout.RecordLength})");
            }
            else
            {
                stream.GotoStart();
                byte[] bytes = stream.ReadNext(Layout.RecordLength);
                if (Layout.HeaderRecord != null && Layout.HeaderRecord.Fields.Length > 0)
                {
                    TaskManager.StartAction(delegate
                    {
                        Layout.HeaderRecord.ReadRecord(bytes);
                    });
                }
                else
                {
                    ReadRecord(bytes);
                }
                while (bytes != null)
                {
                    bytes = stream.ReadNext(Layout.RecordLength);
                    if ((stream.Position >= stream.Length - 2 || stream.Position < 0)
                        && Layout.FooterRecord != null && Layout.FooterRecord.Fields.Length > 0)
                    {
                        TaskManager.StartAction(delegate
                        {
                            Layout.FooterRecord.ReadRecord(bytes);
                        });
                        bytes = null;
                    }
                    else
                    {
                        ReadRecord(bytes);
                    }
                }
            }
            TaskManager.AwaitAllThenClean();
        }

        /// <summary>
        /// Packs the <see cref="Records">Records</see> and their <see cref="IFileRecord.Fields">Fields</see> 
        /// using the properties provided.
        /// Optionally writes to the provided <see cref="System.String">outputFilename</see> as a binary.
        /// </summary>
        /// <param name="outputFilename">If not <see cref="System.Nullable">null</see>, will execute
        /// <see cref="SaveAs(string)">SaveAs</see> after performing the Pack using the 
        /// <see cref="System.String">value</see> provided.</param>
        public override void Pack(string outputFilename = null)
        {
            if (!string.IsNullOrWhiteSpace(outputFilename))
            {
                fStream = new System.IO.FileStream(outputFilename, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            }
            else
            {
                fStream = null;
            }
            if (Layout.HeaderRecord != null && Layout.HeaderRecord.Fields.Length > 0)
            {
                WriteToStream(Layout.HeaderRecord.Pack(Layout.RecordLength, Layout.FillByte));
            }
            foreach (var rec in Records)
            {
                PackRecord(rec);
            }
            TaskManager.AwaitAllThenClean();
            if (Layout.FooterRecord != null && Layout.FooterRecord.Fields.Length > 0)
            {
                WriteToStream(Layout.FooterRecord.Pack(Layout.RecordLength, Layout.FillByte));
            }
            if (!string.IsNullOrWhiteSpace(outputFilename))
            {
                fStream.Close();
                fStream.Dispose();
                fStream = null;
            }
        }
        private System.IO.FileStream fStream = null;

        /// <summary>
        /// Saves the Packed <see cref="Records">Records</see> to the provided <see cref="System.String">outputFilename</see> as a binary.
        /// </summary>
        /// <param name="outputFilename">The <see cref="System.String">name</see> of the output file.</param>
        public override void SaveAs(string outputFilename)
        {
            System.IO.FileStream fs = new System.IO.FileStream(outputFilename, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            if (Layout.HeaderRecord != null && Layout.HeaderRecord.ByteValue != null && Layout.HeaderRecord.ByteValue.Length > 0)
            {
                fs.Write(Layout.HeaderRecord.ByteValue, 0, Layout.RecordLength.ToInt());
            }
            foreach (IFileRecord rec in records)
            {
                fs.Write(rec.ByteValue, 0, Layout.RecordLength.ToInt());
            }
            if (Layout.FooterRecord != null && Layout.FooterRecord.ByteValue != null && Layout.FooterRecord.ByteValue.Length > 0)
            {
                fs.Write(Layout.FooterRecord.ByteValue, 0, Layout.RecordLength.ToInt());
            }
            fs.Close();
        }

        /// <summary>
        /// Adds the provided <see cref="IFileRecord">record</see> to the <see cref="Records">Records</see> collection.
        /// </summary>
        /// <param name="record">The <see cref="IFileRecord">IFileRecord</see> instance to add.</param>
        public override void AddRecord(IFileRecord record)
        {
            records.Add(record);
        }

        /// <summary>
        /// Adds the provided <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of 
        /// <see cref="IFileRecord">records</see> to the <see cref="Records">Records</see> collection.
        /// </summary>
        /// <param name="fileRecords">The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of 
        /// <see cref="IFileRecord">IFileRecord</see> instances to add.</param>
        public override void AddRecords(System.Collections.Generic.IEnumerable<IFileRecord> fileRecords)
        {
            records.AddRange(fileRecords);
        }

        private void ReadRecord(byte[] data)
        {
            TaskManager.StartAction(delegate
            {
                if (base.IsConditional)
                {
                    IFileRecord record = new FileRecord(Layout.MasterFields, data);
                    foreach (IFileConditional condition in Layout.Conditions)
                    {
                        if (condition.Condition(record))
                        {
                            records.Add(new FileRecord(record, condition, data));
                        }
                    }
                }
                else
                {
                    records.Add(new FileRecord(Layout.MasterFields, data));
                }
            });
        }

        private void PackRecord(IFileRecord record)
        {
            WriteToStream(record.Pack(Layout.RecordLength, Layout.FillByte));
        }

        private void WriteToStream(byte[] bytes)
        {
            if (fStream == null)
            {
                return;
            }
            try
            {
                mut.WaitOne();
                fStream.Write(bytes, 0, Layout.RecordLength.ToInt());
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }
        private static readonly System.Threading.Mutex mut = new System.Threading.Mutex();

    }
}
