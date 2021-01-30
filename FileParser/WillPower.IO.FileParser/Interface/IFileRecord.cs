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
    /// A common interface for record-level data, their <see cref="IFileField">fields</see>, and any possible <see cref="FieldException">Exceptions</see>.
    /// </summary>
    public interface IFileRecord
    {
        /// <summary>
        /// The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to utilize for processing.
        /// </summary>
        IFileParserEncoder Encoder { get; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="IFileField">fields</see> belonging to this <see cref="IFileRecord">record</see>.
        /// </summary>
        IFileField[] Fields { get; set; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="FieldException">Exceptions</see>, if any.
        /// </summary>
        FieldException[] Exceptions { get; }

        /// <summary>
        /// Reads an <see cref="System.Array">array</see> of bytes into this instance.
        /// </summary>
        /// <param name="data">The <see cref="System.Array">array</see> of bytes to read.</param>
        void ReadRecord(byte[] data);

        /// <summary>
        /// Returns the value of the <see cref="IFileField">field</see> having the provided <see cref="System.String">name</see> 
        /// as an <see cref="System.Object">object</see>.
        /// </summary>
        /// <param name="fieldName">The <see cref="System.String">name</see> of the <see cref="IFileField">field</see>.</param>
        /// <returns>The value of the <see cref="IFileField">field</see> expressed as an <see cref="System.Object">object</see>.</returns>
        object Get(string fieldName);

        /// <summary>
        /// Returns the value of the <see cref="IFileField">field</see> having the provided <see cref="System.String">name</see> 
        /// (fieldName) as <see cref="System.Type">T</see>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName">The <see cref="System.String">name</see> of the <see cref="IFileField">field</see>.</param>
        /// <returns>The value of the <see cref="IFileField">field</see> expressed as <see cref="System.Type">T</see>.</returns>
        T Get<T>(string fieldName);

    }
}
