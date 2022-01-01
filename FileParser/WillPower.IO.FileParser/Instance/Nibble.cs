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
    /// A common use container for half a byte (or 4 bits), referred to as a "Nibble"
    /// </summary>
    public class Nibble
    {
        /// <summary>
        /// From Left to Right, the 1st, most significant bit as a <see cref="System.Boolean">boolean</see>.
        /// </summary>
        public bool Bit1
        {
            get
            {
                return bit1;
            }
            set
            {
                bit1 = value;
                Bits2Value();
            }
        }

        private bool bit1;
        /// <summary>
        /// From Left to Right, the 2nd bit as a <see cref="System.Boolean">boolean</see>.
        /// </summary>
        public bool Bit2
        {
            get
            {
                return bit2;
            }
            set
            {
                bit2 = value;
                Bits2Value();
            }
        }

        private bool bit2;
        /// <summary>
        /// From Left to Right, the 3rd bit as a <see cref="System.Boolean">boolean</see>.
        /// </summary>
        public bool Bit3
        {
            get
            {
                return bit3;
            }
            set
            {
                bit3 = value;
                Bits2Value();
            }
        }

        private bool bit3;
        /// <summary>
        /// From Left to Right, the 4th or last, least significant bit as a <see cref="System.Boolean">boolean</see>.
        /// </summary>
        public bool Bit4 
        {
            get
            {
                return bit4;
            }
            set
            {
                bit4 = value;
                Bits2Value();
            }
        }
        private bool bit4;
        /// <summary>
        /// The <see cref="System.Byte">byte</see> value of the nibble. Will never exceed 15. 
        /// Any values assigned discard the 4 most significant bits.
        /// </summary>
        public byte Value 
        {
            get
            {
                return bval;
            }
            set
            {
                bval = value;
                Value2Bits();
            }
        }
        private byte bval;

        /// <summary>
        /// .ctor. Creates a new instance of Nibble.
        /// </summary>
        public Nibble()
        { }
        /// <summary>
        /// .ctor. Creates a new instance of Nibble.
        /// </summary>
        /// <param name="value">The <see cref="System.Byte">value</see> to assign to the <see cref="Value">Value</see> property.</param>
        public Nibble(byte value)
        {
            bval = value;
            Value2Bits();
        }
        /// <summary>
        /// .ctor. Creates a new instance of Nibble.
        /// </summary>
        /// <param name="b1">If <see cref="System.Boolean">true</see> sets the <see cref="Bit1">Bit1</see> property to true.</param>
        /// <param name="b2">If <see cref="System.Boolean">true</see> sets the <see cref="Bit2">Bit2</see> property to true.</param>
        /// <param name="b3">If <see cref="System.Boolean">true</see> sets the <see cref="Bit3">Bit3</see> property to true.</param>
        /// <param name="b4">If <see cref="System.Boolean">true</see> sets the <see cref="Bit4">Bit4</see> property to true.</param>
        public Nibble(bool b1, bool b2, bool b3, bool b4)
        {
            bit1 = b1;
            bit2 = b2;
            bit3 = b3;
            bit4 = b4;
            Bits2Value();
        }

        /// <summary>
        /// Combine this <see cref="Nibble">nibble</see> <see cref="System.Byte">value</see> as the high-order nibble 
        /// with the provided low-order <see cref="Nibble">nibble</see> <see cref="System.Byte">value</see>.
        /// </summary>
        /// <param name="nibble"></param>
        /// <returns></returns>
        public byte Append(Nibble nibble)
        {
            return bval.AddComp3(nibble.Value);
        }

        private void Bits2Value()
        {
            byte val = 0x0;
            val += bit1 ? ((byte)BitValue.Bit5) : (byte)0;
            val += bit2 ? ((byte)BitValue.Bit6) : (byte)0;
            val += bit3 ? ((byte)BitValue.Bit7) : (byte)0;
            val += bit4 ? ((byte)BitValue.Bit8) : (byte)0;
            bval = val;
        }
        private void Value2Bits()
        {
            bit1 = bval.Bit(BitOrder.Bit5);
            bit2 = bval.Bit(BitOrder.Bit6);
            bit3 = bval.Bit(BitOrder.Bit7);
            bit4 = bval.Bit(BitOrder.Bit8);
            Bits2Value();
        }

    }
}
