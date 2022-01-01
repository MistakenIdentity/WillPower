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
        /// It is suggested that this collection not be altered directly, but rather assigned to using the 
        /// <see cref="AddRecord(IFileRecord)">AddRecord</see> and 
        /// <see cref="AddRecords(System.Collections.Generic.IEnumerable{IFileRecord})">AddRecords</see> methods.
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

        /// <summary>
        /// Creates a <see cref="System.Data.DataSet">DataSet</see> containing <see cref="System.Data.DataTable">DataTables</see> 
        /// for each conditional record type, header, footer, and/or master recordset.
        /// </summary>
        /// <returns>A <see cref="System.Data.DataSet">DataSet</see> of <see cref="System.Data.DataTable">DataTables</see> with resulting data.</returns>
        System.Data.DataSet ToDataSet();

        /// <summary>
        /// Packs the <see cref="Records">Records</see> and their <see cref="IFileRecord.Fields">Fields</see> 
        /// using the properties provided.
        /// Optionally writes to the provided <see cref="System.String">outputFilename</see> as a binary.
        /// </summary>
        /// <param name="outputFilename">If not <see cref="System.Nullable">null</see>, will execute
        /// <see cref="SaveAs(string)">SaveAs</see> after performing the Pack using the 
        /// <see cref="System.String">value</see> provided.</param>
        void Pack(string outputFilename = null);

        /// <summary>
        /// Saves the Packed <see cref="Records">Records</see> to the provided <see cref="System.String">outputFilename</see> as a binary.
        /// </summary>
        /// <param name="outputFilename">The <see cref="System.String">name</see> of the output file.</param>
        void SaveAs(string outputFilename);

        /// <summary>
        /// Adds the provided <see cref="IFileRecord">record</see> to the <see cref="Records">Records</see> collection.
        /// </summary>
        /// <param name="record">The <see cref="IFileRecord">IFileRecord</see> instance to add.</param>
        void AddRecord(IFileRecord record);

        /// <summary>
        /// Adds the provided <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of 
        /// <see cref="IFileRecord">records</see> to the <see cref="Records">Records</see> collection.
        /// </summary>
        /// <param name="fileRecords">The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of 
        /// <see cref="IFileRecord">IFileRecord</see> instances to add.</param>
        void AddRecords(System.Collections.Generic.IEnumerable<IFileRecord> fileRecords);
    }
}
