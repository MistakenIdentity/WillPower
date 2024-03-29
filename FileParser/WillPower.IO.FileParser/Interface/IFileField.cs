﻿//
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
    /// A common Interface containing the necessary properties and methods for processing a single field.
    /// </summary>
    public interface IFileField
    {
        /// <summary>
        /// The <see cref="IFileParserEncoder">IFileParserEncoder</see> instance to use for encoding.
        /// </summary>
        IFileParserEncoder Encoder { get; set; }
        
        /// <summary>
        /// The starting position of this field in the <see cref="IFileRecord">record</see> expressed as an 
        /// <see cref="System.UInt32">unsigned integer</see>.
        /// </summary>
        uint StartPosition { get; set; }
        
        /// <summary>
        /// The field length expressed as an <see cref="System.UInt32">unsigned integer</see>.
        /// </summary>
        uint Length { get; set; }
        
        /// <summary>
        /// The <see cref="System.String">name</see> of the field.
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// The <see cref="FileFieldDataFormat">format</see> of the field.
        /// String = 0, Comp3 (IBM Packed Number) = 1, Raw (binary) = 2, Table = 3.
        /// </summary>
        FileFieldDataFormat DataFormat { get; set; }
        
        /// <summary>
        /// The <see cref="IFileField.Compute(byte[])">computed</see> value boxed as an <see cref="System.Object">object</see>.
        /// </summary>
        object Value { get; set; }
        
        /// <summary>
        /// The <see cref="System.Type">type</see> of data the field contains.
        /// </summary>
        System.Type Type { get; set; }
        
        /// <summary>
        /// The precision of the value if decimal.
        /// </summary>
        short Precision { get; set; }
        
        /// <summary>
        /// If <see cref="System.Boolean">true</see> and the <see cref="IFileField.DataFormat">DataFormat</see> is set to String (0), 
        /// will attempt to read the date value as year, month, day.
        /// </summary>
        bool YearFirst { get; set; }
        
        /// <summary>
        /// The original, source <see cref="System.Text.Encoding">Encoded</see> <see cref="System.Array">array</see> of bytes belonging to this field.
        /// </summary>
        byte[] ByteValue { get; set; }
        
        /// <summary>
        /// The <see cref="IFileField.Compute(byte[])">computed</see> result as a <see cref="System.String">string</see> or 
        /// the value to be written.
        /// </summary>
        string StringValue { get; set; }
        
        /// <summary>
        /// The <see cref="System.Byte">byte</see>, in Source Encoding, to use as filler for this field.
        /// This or <see cref="FillCharacter">FillCharacter</see> must be set, but not both. Changing one 
        /// will affect the other. Used for writing.
        /// </summary>
        byte FillByte { get; set; }
        
        /// <summary>
        /// The <see cref="System.Char">character</see>, in Source Encoding, to use as filler for this field.
        /// This or <see cref="FillByte">FillByte</see> must be set, but not both. Changing one 
        /// will affect the other. Used for writing.
        /// </summary>
        char FillCharacter { get; set; }
        
        /// <summary>
        /// If <see cref="System.Boolean">true</see> the field will pad the <see cref="FillByte">FillByte</see> 
        /// or <see cref="FillCharacter">FillCharacter</see> to the left.
        /// If <see cref="System.Boolean">false</see> the field will pad the <see cref="FillByte">FillByte</see> 
        /// or <see cref="FillCharacter">FillCharacter</see> to the right.
        /// </summary>
        bool RightAlign { get; set; }

        /// <summary>
        /// Computes the provided <see cref="System.Array">array</see> of bytes using the properties provided.
        /// </summary>
        /// <param name="data">The <see cref="System.Array">array</see> of bytes to process.</param>
        void Compute(byte[] data = null);

        /// <summary>
        /// Packs the field <see cref="Value">Value</see> to the <see cref="ByteValue">ByteValue</see> using the properties provided.
        /// </summary>
        byte[] Pack();
        
        /// <summary>
        /// Identifies how a <see cref="System.Boolean">boolean</see> value is represented as a <see cref="System.String">string</see>.
        /// Length of 1 will only return the first <see cref="System.Char">character</see> of the expected <see cref="System.String">string</see>.
        /// </summary>
        BooleanStringRepresentation BoolAsString { get; set; }

        /// <summary>
        /// Set the value of this field to the provided <see cref="System.Object">value</see>.
        /// </summary>
        /// <param name="value">The <see cref="System.Object">value</see> to assign to this field.</param>
        void SetValue(object value);
    }
}
