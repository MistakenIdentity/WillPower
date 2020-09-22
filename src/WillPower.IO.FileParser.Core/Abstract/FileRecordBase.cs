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
    /// A common base class for record-level data, their <see cref="IFileField">fields</see>, and any possible <see cref="FieldException">Exceptions</see>.
    /// </summary>
    [System.Serializable]
    public abstract class FileRecordBase : IFileRecord
    {
        /// <summary>
        /// The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to utilize for processing.
        /// </summary>
        public IFileParserEncoder Encoder { get; set; }
        /// <summary>
        /// The <see cref="System.Array">collection</see> of <see cref="IFileField">fields</see> belonging to this <see cref="IFileRecord">record</see>.
        /// </summary>
        public IFileField[] Fields { get; set; }
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
        /// .ctor. Must be inherited.
        /// </summary>
        /// <param name="encoder">The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to utilize for processing.</param>
        public FileRecordBase(IFileParserEncoder encoder)
        {
            this.Encoder = encoder;
        }

        /// <summary>
        /// Returns the value of the <see cref="IFileField">field</see> having the provided <see cref="System.String">name</see> as an <see cref="System.Object">object</see>.
        /// </summary>
        /// <param name="fieldName">The <see cref="System.String">name</see> of the <see cref="IFileField">field</see>.</param>
        /// <returns>The value of the <see cref="IFileField">field</see> expressed as an <see cref="System.Object">object</see>.</returns>
        public object Get(string fieldName)
        {
            return this.Fields.ToList().FirstOrDefault(x => x.Name == fieldName)?.Value;
        }

        /// <summary>
        /// Returns the value of the <see cref="IFileField">field</see> having the provided <see cref="System.String">name</see> as <see cref="System.Type">T</see>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName">The <see cref="System.String">name</see> of the <see cref="IFileField">field</see>.</param>
        /// <returns>The value of the <see cref="IFileField">field</see> expressed as <see cref="System.Type">T</see>.</returns>
        public T Get<T>(string fieldName)
        {
            object result = Get(fieldName);
            if (result == null)
            {
                return default;
            }
            return (T)result;
        }
    }
}
