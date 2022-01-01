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

using System.Text;

namespace WillPower
{
    /// <summary>
    /// A concrete class for containing the necessary <see cref="System.Text.Encoding">Encoding</see> properties to parse a file.
    /// </summary>
    [System.Serializable]
    public sealed class FileParserEncoder : IFileParserEncoder
    {
        /// <summary>
        /// The <see cref="System.Text.Encoding">Encoding</see> the anticipated input is in.
        /// Default is <see cref="Encodings.EBCDIC">EBCDIC</see>, or IBM binary and text encoding.
        /// </summary>
        public Encoding SourceEncoding
        {
            get
            {
                return sourceEncoding ?? Encodings.EBCDIC;
            }
            set
            {
                sourceEncoding = value;
            }
        }
        private Encoding sourceEncoding = null;

        /// <summary>
        /// The <see cref="System.Text.Encoding">Encoding</see> the expected output should be in.
        /// Default is <see cref="System.Text.Encoding.ASCII">ASCII</see>.
        /// </summary>
        public Encoding DestinationEncoding 
        {
            get
            {
                return destinationEncoding ?? System.Text.Encoding.ASCII;
            }
            set
            {
                destinationEncoding = value;
            }
        }
        private Encoding destinationEncoding = null;

        /// <summary>
        /// Factory method for providing a default EBCDIC to ASCII (write = <see cref="System.Boolean">false</see>) or 
        /// ASCII to EBCDIC (write = <see cref="System.Boolean">true</see>) <see cref="IFileParserEncoder">IFileParserEncoder</see>.
        /// </summary>
        /// <param name="write">
        /// If <see cref="System.Boolean">true</see>, this will return the default ASCII to EBCDIC 
        /// <see cref="IFileParserEncoder">IFileParserEncoder</see> instance for writing binary EBCDIC files.
        /// If <see cref="System.Boolean">false</see>, this will return the default EBCDIC to ASCII 
        /// <see cref="IFileParserEncoder">IFileParserEncoder</see> instance for reading binary EBCDIC files.
        /// Default is <see cref="System.Boolean">false</see>.
        /// </param>
        /// <returns>A new default instance of <see cref="FileParserEncoder">FileParserEncoder</see>.</returns>
        public static IFileParserEncoder GetDefaultEncoder(bool write = false)
        {
            if (!write)
            {
                return new FileParserEncoder();
            }
            return new FileParserEncoder
            {
                SourceEncoding = System.Text.Encoding.ASCII,
                DestinationEncoding = Encodings.EBCDIC
            };
        }

    }
}
