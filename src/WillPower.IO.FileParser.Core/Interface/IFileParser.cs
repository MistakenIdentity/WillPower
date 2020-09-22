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
    /// A common Interface for a file parsing engine.
    /// </summary>
    public interface IFileParser
    {
        /// <summary>
        /// The <see cref="IFileLayout">layout</see> of the file, including any <see cref="IFileLayout.HeaderRecord">header</see>, 
        /// <see cref="IFileLayout.FooterRecord">footer</see>, or <see cref="IFileLayout.Conditions">conditional</see> rows.
        /// For binary files the <see cref="IFileLayout.RecordLength">RecordLength</see> must be greater than 0.
        /// For text files the default <see cref="IFileLayout.TextLineTerminator">TextLineTerminator</see> is typically '\n' (nextline), but can be set at any time.
        /// </summary>
        IFileLayout Layout { get; set; }
        /// <summary>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="IFileRecord">records</see> read from the file.
        /// </summary>
        System.Collections.Generic.IEnumerable<IFileRecord> Records { get; }
        /// <summary>
        /// The <see cref="WillPower.TaskManager">TaskManager</see> instance managing threads.
        /// </summary>
        TaskManager TaskManager { get; set; }

        /// <summary>
        /// Load the provided file as specified by <see cref="System.String">fileName</see> into the parser.
        /// </summary>
        /// <param name="fileName">The <see cref="System.String">name</see> of the file to load.</param>
        void LoadFile(string fileName);

        /// <summary>
        /// Load the provided <see cref="System.IO.Stream">stream</see> into the parser.
        /// </summary>
        /// <param name="stream">The <see cref="System.IO.Stream">stream</see> to load.</param>
        void LoadStream(System.IO.Stream stream);

    }
}
