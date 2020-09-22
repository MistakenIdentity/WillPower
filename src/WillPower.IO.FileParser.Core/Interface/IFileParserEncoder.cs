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
    /// An Interface for containing the necessary <see cref="System.Text.Encoding">Encoding</see> properties to parse a file.
    /// </summary>
    public interface IFileParserEncoder
    {
        /// <summary>
        /// The <see cref="System.Text.Encoding">Encoding</see> the anticipated input is in.
        /// </summary>
        System.Text.Encoding SourceEncoding { get; set; }
        /// <summary>
        /// The <see cref="System.Text.Encoding">Encoding</see> the expected output should be in.
        /// </summary>
        System.Text.Encoding DestinationEncoding { get; set; }
        /// <summary>
        /// The EBCDIC <see cref="System.Text.Encoding">Encoding</see> for easy reference. 
        /// This is usually the <see href="https://www.ibm.com/support/knowledgecenter/SSEQ5Y_5.9.0/com.ibm.pcomm.doc/reference/pdf/hcp_referenceV58.pdf">IBM 037</see> code page in .Net.
        /// </summary>
        System.Text.Encoding EBCDIC { get; set; }

    }
}
