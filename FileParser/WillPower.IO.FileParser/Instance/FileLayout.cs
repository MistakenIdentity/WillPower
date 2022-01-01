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
    /// The <see cref="IFileLayout">layout</see> of the file, including any <see cref="IFileLayout.HeaderRecord">header</see>, 
    /// <see cref="IFileLayout.FooterRecord">footer</see>, or <see cref="IFileLayout.Conditions">conditional</see> rows.
    /// For binary files the <see cref="IFileLayout.RecordLength">RecordLength</see> must be greater than 0.
    /// For text files the default <see cref="IFileLayout.TextLineTerminator">TextLineTerminator</see> is '\n' (nextline), but can be set at any time.
    /// Implements <see cref="IFileLayout">IFileLayout</see>. Cannot be inherited.
    /// </summary>

    [System.Serializable]
    public sealed class FileLayout : IFileLayout
    {
        /// <summary>
        /// The <see cref="System.String">name</see> of this layout, for reference.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.
        /// </summary>
        public IFileParserEncoder Encoder { get; set; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="IFileConditional">conditions</see> to evaulate each 
        /// <see cref="IFileRecord">record</see> for (and apply those fields if true), if any.
        /// </summary>
        public IFileConditional[] Conditions { get; set; }
        /// <summary>
        /// The Header <see cref="IFileRecord">record</see> template, if any.
        /// </summary>
        public IFileRecord HeaderRecord { get; set; }
        /// <summary>
        /// The Footer <see cref="IFileRecord">record</see> template, if any.
        /// </summary>
        public IFileRecord FooterRecord { get; set; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="IFileField">fields</see> containing either master key fields (3D file), 
        /// or all fields (2D file).
        /// </summary>
        public IFileField[] MasterFields { get; set; }
        /// <summary>
        /// The length of each record expressed as an <see cref="System.UInt32">unsigned integer</see>.
        /// </summary>
        public uint RecordLength { get; set; }
        /// <summary>
        /// The <see cref="System.Byte">byte</see>, in Source Encoding, to use as filler for a record (spaces between fields).
        /// This or <see cref="FillCharacter">FillCharacter</see> must be set, but not both. Changing one 
        /// will affect the other. Used for writing.
        /// </summary>
        public byte FillByte { get; set; }
        /// <summary>
        /// The <see cref="System.Char">character</see>, in Source Encoding, to use as filler for a record (spaces between fields).
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
        /// If <see cref="System.Boolean">true</see>, open using character-based methods. If <see cref="System.Boolean">false</see>, 
        /// open using binary methods.
        /// </summary>
        public bool OpenAsText
        {
            get
            {
                return openAsText;
            }
            set
            {
                openAsText = value;
                if (openAsText && Encoder.SourceEncoding == Encodings.EBCDIC)
                {
                    Encoder.SourceEncoding = System.Text.Encoding.UTF8;
                }
            }
        }
        private bool openAsText = false;
        /// <summary>
        /// The <see cref="System.Char">character</see> used to determine end of line when 
        /// <see cref="IFileLayout.OpenAsText">OpenAsText</see> is <see cref="System.Boolean">true</see>.
        /// For text files the default is '\n' (nextline), but can be set at any time.
        /// </summary>
        public char TextLineTerminator 
        {
            get
            {
                if (!txtTerminator.HasValue)
                {
                    txtTerminator = '\n';
                }
                return txtTerminator.Value;
            }
            set
            {
                txtTerminator = value;
            }
        }
        private char? txtTerminator = null;

        /// <summary>
        /// .ctor. Creates a new instance of FileLayout.
        /// </summary>
        public FileLayout()
        {
            Encoder = new FileParserEncoder();
        }
        /// <summary>
        /// .ctor. Creates a new instance of FileLayout.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.</param>
        public FileLayout(IFileParserEncoder encoder)
        {
            Encoder = encoder;
        }

    }
}
