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
    /// A common interface for record-level data, their <see cref="IFileField">fields</see>, and any possible 
    /// <see cref="FieldException">Exceptions</see>.
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
        /// The packed and destination <see cref="System.Text.Encoding">Encoded</see> <see cref="System.Array">array</see> 
        /// of bytes belonging to this <see cref="IFileRecord">record</see>. Not used for file read.
        /// </summary>
        byte[] ByteValue { get; set; }

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

        /// <summary>
        /// Packs the record <see cref="Fields">Fields</see> to their <see cref="IFileField.ByteValue">ByteValues</see> 
        /// using the properties provided.
        /// </summary>
        /// <param name="recordLength">The length of the record expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="fillByte">        
        /// The <see cref="System.Byte">byte</see>, in Source Encoding, from <see cref="IFileLayout">IFileLayout</see>.
        /// </param>
        byte[] Pack(uint recordLength, byte fillByte);

        /// <summary>
        /// Set the value of the specified <see cref="System.String">field</see> to the provided <see cref="System.Object">value</see>.
        /// </summary>
        /// <param name="fieldName">The <see cref="System.String">Name</see> of the field to apply the value to.</param>
        /// <param name="value">The <see cref="System.Object">value</see> to assign to the field.</param>
        void SetValue(string fieldName, object value);

    }
}
