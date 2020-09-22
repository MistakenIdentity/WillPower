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
    /// An interface to encapsulate expected functionality to determine an action.
    /// </summary>
    public interface IFileConditional
    {
        /// <summary>
        /// The collection of fields <see cref="IFileField">fields</see> containing clones of the fields to evaluate.
        /// </summary>
        IFileField[] Fields { get; set; }
        /// <summary>
        /// <see cref="System.Func{T, TResult}">Func<![CDATA[<]]></see><see cref="IFileRecord">IFileRecord</see>
        /// <see cref="System.Func{T, TResult}">, bool<![CDATA[>]]></see>: A method to evaluate a <see cref="IFileRecord">record</see> 
        /// and return <see cref="System.Boolean">true</see> or <see cref="System.Boolean">false</see> to determine some action.
        /// <example>
        /// <![CDATA[
        /// delegate:
        /// 
        ///     MyFileConditional.Condition = CheckForField3C;
        /// ...
        ///     private bool CheckForField3C(IFileRecord record)
        ///     {
        ///         return record.Get<string>("Field3") == "C";
        ///     }
        ///     
        /// inline:
        /// 
        ///     MyFileConditional.Condition = record => record.Get<string>("Field3") == "C";
        /// 
        /// ]]>
        /// </example>
        /// </summary>
        System.Func<IFileRecord, bool> Condition { get; set; }

        /// <summary>
        /// Evaluates the provided <see cref="IFileRecord">record</see> against the <see cref="IFileConditional.Condition">Condition</see> property.
        /// </summary>
        /// <param name="record">The <see cref="IFileRecord">record</see> to evaluate against the <see cref="IFileConditional.Condition">Condition</see> property.</param>
        /// <returns>True if the <see cref="IFileRecord">record</see> meets the <see cref="IFileConditional.Condition">Condition</see> property.</returns>
        bool IsConditional(IFileRecord record);
    }
}
