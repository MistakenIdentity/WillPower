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
    /// A concrete class for containing the necessary <see cref="System.Text.Encoding">Encoding</see> properties to parse a file.
    /// </summary>
    [System.Serializable]
    public sealed class FileParserEncoder : IFileParserEncoder
    {
        /// <summary>
        /// The <see cref="System.Text.Encoding">Encoding</see> the anticipated input is in.
        /// Default is <see cref="IFileParserEncoder.EBCDIC">EBCDIC</see>, or IBM AS/400 binary and text encoding.
        /// </summary>
        public System.Text.Encoding SourceEncoding
        {
            get
            {
                return sourceEncoding ?? this.EBCDIC;
            }
            set
            {
                sourceEncoding = value;
            }
        }
        System.Text.Encoding sourceEncoding;

        /// <summary>
        /// The <see cref="System.Text.Encoding">Encoding</see> the expected output should be in.
        /// Default is <see cref="System.Text.Encoding.UTF8">UTF8</see>.
        /// </summary>
        public System.Text.Encoding DestinationEncoding 
        {
            get
            {
                return destinationEncoding ?? System.Text.Encoding.UTF8;
            }
            set
            {
                destinationEncoding = value;
            }
        }
        System.Text.Encoding destinationEncoding;

        /// <summary>
        /// The EBCDIC <see cref="System.Text.Encoding">Encoding</see> for easy reference. 
        /// This is usually the <see href="https://www.ibm.com/support/knowledgecenter/SSEQ5Y_5.9.0/com.ibm.pcomm.doc/reference/pdf/hcp_referenceV58.pdf">IBM 037</see> code page in .Net.
        /// Default is CodePage IBM037.
        /// </summary>
        public System.Text.Encoding EBCDIC
        {
            get
            {
                if (ebcdicEncoding == null)
                {
                    ebcdicEncoding = System.Text.Encoding.GetEncoding("IBM037");
                }
                return ebcdicEncoding;
            }
            set 
            {
                ebcdicEncoding = value;
            }
        }
        System.Text.Encoding ebcdicEncoding = null;

    }
}
