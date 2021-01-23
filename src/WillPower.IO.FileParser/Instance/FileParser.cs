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
        /// </summary>
        public override IEnumerable<IFileRecord> Records => records.ToArray();

        /// <summary>
        /// .ctor. Creates a new instance of FileParser using the default <see cref="FileParserEncoder">FileParserEncoder</see>.
        /// </summary>
        public FileParser() : this(new FileParserEncoder())
        { }
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
        { }

        /// <summary>
        /// Load the provided <see cref="System.IO.Stream">stream</see> into the parser.
        /// </summary>
        /// <param name="stream">The <see cref="System.IO.Stream">stream</see> to load.</param>
        public override void LoadStream(System.IO.Stream stream)
        {
            if (!this.Layout.OpenAsText && this.Layout.RecordLength < 1)
            {
                return;
            }
            if (this.Layout.MasterFields == null || this.Layout.MasterFields.Length < 1)
            {
                return;
            }
            UpdateEncoders();
            if (this.Layout.OpenAsText)
            {
                stream.GotoStart();
                byte[] bytes = stream.ReadToChar(this.Layout.TextLineTerminator);
                if (this.Layout.HeaderRecord != null && this.Layout.HeaderRecord.Fields?.Length > 0)
                {
                    TaskManager.StartAction(delegate
                    {
                        this.Layout.HeaderRecord.ReadRecord(bytes);
                    });
                }
                else
                {
                    ReadRecord(bytes);
                }
                while (bytes != null)
                {
                    bytes = stream.ReadToChar(this.Layout.TextLineTerminator, this.Layout.Encoder.SourceEncoding);
                    if ((stream.Position >= stream.Length - 2 || stream.Position < 0) 
                        && this.Layout.FooterRecord != null && this.Layout.FooterRecord.Fields?.Length > 0)
                    {
                        TaskManager.StartAction(delegate
                        {
                            this.Layout.FooterRecord.ReadRecord(bytes);
                        });
                        bytes = null;
                    }
                    else
                    {
                        ReadRecord(bytes);
                    }
                }
            }
            else if (stream.Length % this.Layout.RecordLength != 0)
            {
                throw new InvalidOperationException(
                    $"{IO.FileParser.Properties.Resources.ResourceManager.GetString("InvalidLength")} Length: {stream.Length} RecordLength: {this.Layout.RecordLength}");
            }
            else
            {
                stream.GotoStart();
                byte[] bytes = stream.ReadNext(this.Layout.RecordLength);
                if (this.Layout.HeaderRecord != null && this.Layout.HeaderRecord.Fields.Length > 0)
                {
                    TaskManager.StartAction(delegate
                    {
                        this.Layout.HeaderRecord.ReadRecord(bytes);
                    });
                }
                else
                {
                    ReadRecord(bytes);
                }
                while (bytes != null)
                {
                    bytes = stream.ReadNext(this.Layout.RecordLength);
                    if ((stream.Position >= stream.Length - 2 || stream.Position < 0)
                        && this.Layout.FooterRecord != null && this.Layout.FooterRecord.Fields.Length > 0)
                    {
                        TaskManager.StartAction(delegate
                        {
                            this.Layout.FooterRecord.ReadRecord(bytes);
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

        private void ReadRecord(byte[] data)
        {
            TaskManager.StartAction(delegate
            {
                if (base.IsConditional)
                {
                    IFileRecord record = new FileRecord(Layout.MasterFields, data);
                    foreach (IFileConditional condition in this.Layout.Conditions)
                    {
                        if (condition.Condition(record))
                        {
                            records.Add(new FileRecord(record, condition, data));
                        }
                    }
                }
                else
                {
                    records.Add(new FileRecord(this.Layout.MasterFields, data));
                }
            });
        }

    }
}
