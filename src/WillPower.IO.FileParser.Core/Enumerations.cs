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
        /// The value is packed using the <see href="https://www.ibm.com/support/knowledgecenter/ssw_ibm_i_74/rzasd/padecfo.htm">IBM Packed Data Format</see>.
        /// </summary>
        Comp3 = 1,
        /// <summary>
        /// The value is raw binary.
        /// </summary>
        Raw = 2
    }

}
