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
    /// A common base class for record-level data, their <see cref="IFileField">fields</see>, and any possible 
    /// <see cref="FieldException">Exceptions</see>.
    /// </summary>
    [System.Serializable]
    public abstract class FileRecordBase : IFileRecord
    {
        /// <summary>
        /// The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to utilize for processing.
        /// </summary>
        public IFileParserEncoder Encoder { get; set; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="IFileField">fields</see> belonging to this 
        /// <see cref="IFileRecord">record</see>.
        /// </summary>
        public IFileField[] Fields { get; set; }
        /// <summary>
        /// The packed and destination <see cref="System.Text.Encoding">Encoded</see> <see cref="System.Array">array</see> 
        /// of bytes belonging to this <see cref="IFileRecord">record</see>. Not used for file read.
        /// </summary>
        public byte[] ByteValue { get; set; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="FieldException">Exceptions</see>, if any.
        /// </summary>
        public abstract FieldException[] Exceptions { get; }

        /// <summary>
        /// Reads an <see cref="System.Array">array</see> of bytes into this instance.
        /// </summary>
        /// <param name="data">The <see cref="System.Array">array</see> of bytes to read.</param>
        public abstract void ReadRecord(byte[] data);

        /// <summary>
        /// Packs the record <see cref="Fields">Fields</see> to their <see cref="IFileField.ByteValue">ByteValues</see> 
        /// using the properties provided.
        /// </summary>
        /// <param name="recordLength">The length of the record expressed as an <see cref="System.UInt32">unsigned integer</see>.</param>
        /// <param name="fillByte">        
        /// The <see cref="System.Byte">byte</see>, in Source Encoding, from <see cref="IFileLayout">IFileLayout</see>.
        /// </param>
        public abstract byte[] Pack(uint recordLength, byte fillByte);

        /// <summary>
        /// .ctor. Must be inherited.
        /// </summary>
        public FileRecordBase()
        {
            Encoder = FileParserEncoder.GetDefaultEncoder();
        }
        /// <summary>
        /// .ctor. Must be inherited.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to utilize for processing.</param>
        public FileRecordBase(IFileParserEncoder encoder)
        {
            Encoder = encoder;
        }

        /// <summary>
        /// Returns the value of the <see cref="IFileField">field</see> having the provided <see cref="System.String">name</see> as an 
        /// <see cref="System.Object">object</see>.
        /// </summary>
        /// <param name="fieldName">The <see cref="System.String">name</see> of the <see cref="IFileField">field</see>.</param>
        /// <returns>The value of the <see cref="IFileField">field</see> expressed as an <see cref="System.Object">object</see>.</returns>
        public object Get(string fieldName)
        {
            return GetField(fieldName)?.Value;
        }

        /// <summary>
        /// Returns the value of the <see cref="IFileField">field</see> having the provided <see cref="System.String">name</see> as 
        /// <see cref="System.Type">T</see>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName">The <see cref="System.String">name</see> of the <see cref="IFileField">field</see>.</param>
        /// <returns>The value of the <see cref="IFileField">field</see> expressed as <see cref="System.Type">T</see>.</returns>
        public T Get<T>(string fieldName)
        {
            IFileField field = GetField(fieldName);
            if (field.Type == typeof(FileTable))
            {
                return (T)field; 
            }
            if (field?.Value == null)
            {
                return default;
            }
            if (typeof(FileTable) == field.Type || field.Type.IsAssignableFrom(typeof(FileTable)))
            {
                if (typeof(FileTable) == typeof(T))
                {
                    return (T)field;
                }
                else if (typeof(System.Data.DataTable) == typeof(T))
                {
                    return (T)((field as FileTable).ToDataTable() as object);
                }                        
            }
            return (T)field.Value;
        }

        /// <summary>
        /// Set the value of the specified <see cref="System.String">fieldName</see> to the provided <see cref="System.Object">value</see>.
        /// </summary>
        /// <param name="fieldName">The <see cref="System.String">Name</see> of the field to apply the value to.</param>
        /// <param name="value">The <see cref="System.Object">value</see> to assign to the field.</param>
        public void SetValue(string fieldName, object value)
        {
            GetField(fieldName).SetValue(value);
        }

        private IFileField GetField(string fieldName)
        {
            return Fields.FirstOrDefault(x => x.Name == fieldName);
        }
    }
}
