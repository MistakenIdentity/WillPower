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
    /// <see cref="System.Enum">Enumeration</see> for <see cref="IFileField">field</see> data format.
    /// </summary>
    public enum FileFieldDataFormat
    {
        /// <summary>
        /// The value is an encoded <see cref="System.String">string</see>.
        /// </summary>
        String = 0,
        /// <summary>
        /// The value is packed using the 
        /// <see href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Data Format</see>, 
        /// which is an implementation of <see href="http://www.3480-3590-data-conversion.com/article-packed-fields.html">Cobol COMP3</see>.
        /// </summary>
        IBMPacked = 1,
        /// <summary>
        /// The value is raw binary.
        /// </summary>
        Raw = 2,
        /// <summary>
        /// The value is a collection of values in a subset which may include multiple rows within the main recordset.
        /// </summary>
        Table = 3,
        ///// <summary>
        ///// The value is packed using the 
        ///// <see href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/intfmt9.htm">IBM Signed Numeric Format</see>, 
        ///// which is an implementation of 
        ///// <see href="http://www.3480-3590-data-conversion.com/article-signed-fields.html">IBM Cobol</see> for no good reason.
        ///// </summary>
        //IBMSigned = 4
    }

    /// <summary>
    /// enum for bits within a byte by order
    /// </summary>
    public enum BitOrder
    {
        /// <summary>
        /// From Left to Right, the 1st, most significant bit
        /// </summary>
        Bit1 = 0,
        /// <summary>
        /// From Left to Right, the 2nd bit
        /// </summary>
        Bit2 = 1,
        /// <summary>
        /// From Left to Right, the 3rd bit
        /// </summary>
        Bit3 = 2,
        /// <summary>
        /// From Left to Right, the 4th bit
        /// </summary>
        Bit4 = 3,
        /// <summary>
        /// From Left to Right, the 5th bit
        /// </summary>
        Bit5 = 4,
        /// <summary>
        /// From Left to Right, the 6th bit
        /// </summary>
        Bit6 = 5,
        /// <summary>
        /// From Left to Right, the 7th bit
        /// </summary>
        Bit7 = 6,
        /// <summary>
        /// From Left to Right, the 8th or last, least significant bit
        /// </summary>
        Bit8 = 7
    }

    /// <summary>
    /// enum for bits within a byte by value
    /// </summary>
    [System.Flags]
    public enum BitValue
    {
        /// <summary>
        /// From Left to Right, the 1st, most significant bit
        /// </summary>
        Bit1 = 0x128,
        /// <summary>
        /// From Left to Right, the 2nd bit
        /// </summary>
        Bit2 = 0x64,
        /// <summary>
        /// From Left to Right, the 3rd bit
        /// </summary>
        Bit3 = 0x32,
        /// <summary>
        /// From Left to Right, the 4th bit
        /// </summary>
        Bit4 = 0x16,
        /// <summary>
        /// From Left to Right, the 5th bit
        /// </summary>
        Bit5 = 0x8,
        /// <summary>
        /// From Left to Right, the 6th bit
        /// </summary>
        Bit6 = 0x4,
        /// <summary>
        /// From Left to Right, the 7th bit
        /// </summary>
        Bit7 = 0x2,
        /// <summary>
        /// From Left to Right, the 8th or last, least significant bit
        /// </summary>
        Bit8 = 0x1
    }

    /// <summary>
    /// Identifies how a <see cref="System.Boolean">boolean</see> value is represented as a <see cref="System.String">string</see>.
    /// Length of 1 will only return the first <see cref="System.Char">charcter</see> of the expected <see cref="System.String">string</see>.
    /// </summary>
    public enum BooleanStringRepresentation
    {
        /// <summary>
        /// Yes, No, Y or N
        /// </summary>
        Yes_No = 0,
        /// <summary>
        /// True, False, T or F
        /// </summary>
        True_False = 1,
        /// <summary>
        /// yes, no, y or n
        /// </summary>
        yes_no = 3,
        /// <summary>
        /// true, false, t or f
        /// </summary>
        true_false = 4,
        /// <summary>
        /// 1 (true) or 0 (false)
        /// </summary>
        One_Zero = 5,
        /// <summary>
        /// 0 (true) or -1 (false)
        /// </summary>
        Zero_Minus1 = 6
    }

}
