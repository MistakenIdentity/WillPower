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
    /// The layout of the file, including any <see cref="IFileLayout.HeaderRecord">header</see>, 
    /// <see cref="IFileLayout.FooterRecord">footer</see>, or <see cref="IFileLayout.Conditions">conditional</see> rows.
    /// For binary files the <see cref="IFileLayout.RecordLength">RecordLength</see> must be greater than 0.
    /// For text files the default <see cref="IFileLayout.TextLineTerminator">TextLineTerminator</see> is typically '\n' (nextline), 
    /// but can be set at any time.
    /// </summary>
    public interface IFileLayout
    {
        /// <summary>
        /// The <see cref="System.String">name</see> of this layout, for reference.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.
        /// </summary>
        IFileParserEncoder Encoder { get; set; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="IFileConditional">conditions</see> to evaulate each 
        /// <see cref="IFileRecord">record</see> for (and apply those fields if true), if any.
        /// </summary>
        IFileConditional[] Conditions { get; set; }
        /// <summary>
        /// The Header <see cref="IFileRecord">record</see> template, if any.
        /// </summary>
        IFileRecord HeaderRecord { get; set; }
        /// <summary>
        /// The Footer <see cref="IFileRecord">record</see> template, if any.
        /// </summary>
        IFileRecord FooterRecord { get; set; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="IFileField">fields</see> containing either master key fields (3D file), 
        /// or all fields (2D file).
        /// </summary>
        IFileField[] MasterFields { get; set; }
        /// <summary>
        /// The length of each record expressed as an <see cref="System.UInt32">unsigned integer</see>.
        /// </summary>
        uint RecordLength { get; set; }
        /// <summary>
        /// If <see cref="System.Boolean">true</see>, open using character-based methods. If <see cref="System.Boolean">false</see>, 
        /// open using binary methods.
        /// </summary>
        bool OpenAsText { get; set; }
        /// <summary>
        /// The <see cref="System.Char">character</see> used to determine end of line when <see cref="IFileLayout.OpenAsText">OpenAsText</see> 
        /// is <see cref="System.Boolean">true</see>.
        /// </summary>
        char TextLineTerminator { get; set; }
        /// <summary>
        /// The <see cref="System.Byte">byte</see>, in Source Encoding, to use as filler for a record (spaces between fields).
        /// This or <see cref="FillCharacter">FillCharacter</see> must be set, but not both. Changing one 
        /// will affect the other. Used for writing.
        /// </summary>
        byte FillByte { get; set; }
        /// <summary>
        /// The <see cref="System.Char">character</see>, in Source Encoding, to use as filler for a record (spaces between fields).
        /// This or <see cref="FillByte">FillByte</see> must be set, but not both. Changing one 
        /// will affect the other. Used for writing.
        /// </summary>
        char FillCharacter { get; set; }

    }
}
