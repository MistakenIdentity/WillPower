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
    /// <see cref="System.Exception">System.Exception</see> inheritor.
    /// An object for containing exception data occurring during field read. Can be inherited.
    /// </summary>
    [System.Serializable]
    public class FieldException : System.Exception
    {
        /// <summary>
        /// The specific field <see cref="IFileField">(IFileField)</see> that generated the <see cref="System.Exception">Exception</see> or null.
        /// </summary>
        public IFileField Field { get; set; }

        /// <summary>
        /// .ctor. Use as you would <see cref="System.Exception">System.Exception</see>.
        /// </summary>
        /// <param name="message">The <see cref="System.String">message</see> for the exception.</param>
        public FieldException(string message) : base(message)
        { }
        /// <summary>
        /// .ctor. Use as you would <see cref="System.Exception">System.Exception</see>.
        /// </summary>
        /// <param name="message">The <see cref="System.String">message</see> for the exception.</param>
        /// <param name="field">The specific field <see cref="IFileField">(IFileField)</see> that generated the <see cref="System.Exception">Exception</see>.</param>
        public FieldException(string message, IFileField field) : base(message)
        {
            Field = field;
        }
        /// <summary>
        /// .ctor. Use as you would <see cref="System.Exception">System.Exception</see>.
        /// </summary>
        /// <param name="ex">The original <see cref="System.Exception">Exception</see> generated during field processing, passed to <see cref="System.Exception.InnerException">InnerException</see>.</param>
        /// <param name="field">The specific field <see cref="IFileField">(IFileField)</see> that generated the <see cref="System.Exception">Exception</see>.</param>
        public FieldException(System.Exception ex, IFileField field) : base(ex.Message, ex)
        {
            Field = field;
        }
    }
}
