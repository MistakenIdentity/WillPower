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

using System.Linq;

namespace WillPower
{
    /// <summary>
    /// A container for serializing in and out of json and xml for <see cref="FileLayout">file layouts</see>.
    /// Note: Only valid for 2D layouts at this time.
    /// </summary>
    [System.Serializable]
    public class SerializableLayout
    {

        /// <summary>
        /// The <see cref="System.String">name</see> of this layout, for reference.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="SerializableField">fields</see> used for the header record, if any.
        /// </summary>
        public SerializableField[] HeaderFields { get; set; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="SerializableField">fields</see> used for the footer record, if any.
        /// </summary>
        public SerializableField[] FooterFields { get; set; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="SerializableField">fields</see> containing all fields (2D file).
        /// </summary>
        public SerializableField[] MasterFields { get; set; }
        /// <summary>
        /// The length of each record expressed as an <see cref="System.UInt32">unsigned integer</see>.
        /// </summary>
        public uint RecordLength { get; set; }
        /// <summary>
        /// The <see cref="System.Byte">byte</see>, in Source Encoding, to use as filler for a record (spaces between fields).
        /// </summary>
        public byte FillByte { get; set; }
        /// <summary>
        /// The <see cref="System.Char">character</see>, in Source Encoding, to use as filler for a record (spaces between fields).
        /// </summary>
        public char FillCharacter { get; set; }
        /// <summary>
        /// If <see cref="System.Boolean">true</see>, open using character-based methods. If <see cref="System.Boolean">false</see>, 
        /// open using binary methods.
        /// </summary>
        public bool OpenAsText { get; set; }
        /// <summary>
        /// The <see cref="System.Char">character</see> used to determine end of line when 
        /// <see cref="IFileLayout.OpenAsText">OpenAsText</see> is <see cref="System.Boolean">true</see>.
        /// For text files the default is '\n' (nextline), but can be set at any time.
        /// </summary>
        public char TextLineTerminator { get; set; }

        /// <summary>
        /// .ctor.  Creates a default instance of this object.
        /// </summary>
        public SerializableLayout()
        { }
        /// <summary>
        /// .ctor. Creates an instance of this object using the <see cref="IFileLayout">layout</see> provided.
        /// </summary>
        /// <param name="layout">The <see cref="IFileLayout">layout</see> to model this instance from.</param>
        public SerializableLayout(IFileLayout layout) 
        {
            FillByte = layout.FillByte;
            FillCharacter = layout.FillCharacter;
            Name = layout.Name;
            OpenAsText = layout.OpenAsText;
            RecordLength = layout.RecordLength;
            TextLineTerminator = layout.TextLineTerminator;
            if (layout.MasterFields != null && layout.MasterFields.Length > 0)
            {
                System.Collections.Generic.List<SerializableField> fields = new System.Collections.Generic.List<SerializableField>();
                foreach (IFileField field in layout.MasterFields.OrderBy(x => x.StartPosition))
                {
                    fields.Add(new SerializableField(field));
                }
                MasterFields = fields.ToArray();
            }
            if (layout.HeaderRecord != null && layout.HeaderRecord.Fields != null && layout.HeaderRecord.Fields.Length > 0)
            {
                System.Collections.Generic.List<SerializableField> fields = new System.Collections.Generic.List<SerializableField>();
                foreach (IFileField field in layout.HeaderRecord.Fields.OrderBy(x => x.StartPosition))
                {
                    fields.Add(new SerializableField(field));
                }
                HeaderFields = fields.ToArray();
            }
            if (layout.FooterRecord != null && layout.FooterRecord.Fields != null && layout.FooterRecord.Fields.Length > 0)
            {
                System.Collections.Generic.List<SerializableField> fields = new System.Collections.Generic.List<SerializableField>();
                foreach (IFileField field in layout.FooterRecord.Fields.OrderBy(x => x.StartPosition))
                {
                    fields.Add(new SerializableField(field));
                }
                FooterFields = fields.ToArray();
            }
        }

        /// <summary>
        /// Gets the <see cref="IFileLayout">layout</see> relevant to this object.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">encoder</see> to use. If null, the default (EBCDIC 2 ASCII) is used.</param>
        /// <returns>A <see cref="IFileLayout">layout</see> matching this object's properties.</returns>
        public IFileLayout GetFileLayout(IFileParserEncoder encoder = null)
        {
            IFileParserEncoder defEncoder = encoder ?? FileParserEncoder.GetDefaultEncoder();
            IFileLayout result = new FileLayout(defEncoder)
            {
                Name = Name,
                OpenAsText = OpenAsText,
                RecordLength = RecordLength
            };
            if (OpenAsText)
            {
                result.FillCharacter = FillCharacter;
                result.TextLineTerminator = TextLineTerminator;
            }
            else
            {
                result.FillByte = FillByte;
            }
            if (HeaderFields != null && HeaderFields.Length > 0)
            {
                System.Collections.Generic.List<IFileField> fields = new System.Collections.Generic.List<IFileField>();
                foreach (SerializableField field in HeaderFields.OrderBy(x => x.StartPosition))
                {
                    fields.Add(field.GetField(defEncoder));
                }
                result.HeaderRecord = new FileRecord(fields.ToArray());
            }
            if (FooterFields != null && FooterFields.Length > 0)
            {
                System.Collections.Generic.List<IFileField> fields = new System.Collections.Generic.List<IFileField>();
                foreach (SerializableField field in FooterFields.OrderBy(x => x.StartPosition))
                {
                    fields.Add(field.GetField(defEncoder));
                }
                result.FooterRecord = new FileRecord(fields.ToArray());
            }
            if (MasterFields != null && MasterFields.Length > 0)
            {
                System.Collections.Generic.List<IFileField> fields = new System.Collections.Generic.List<IFileField>();
                foreach (SerializableField field in MasterFields.OrderBy(x => x.StartPosition))
                {
                    fields.Add(field.GetField(defEncoder));
                }
                result.MasterFields = fields.ToArray();
            }
            return result;
        }

        /// <summary>
        /// Gets a <see cref="IFileParser">file parser</see> object extracted from this layouts properties.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">encoder</see> to use. If null, the default (EBCDIC 2 ASCII) is used.</param>
        /// <returns>A <see cref="IFileParser">parser</see> derived from this object's properties.</returns>
        public IFileParser GetParser(IFileParserEncoder encoder = null)
        {
            return new FileParser(GetFileLayout(encoder));
        }

    }

}
