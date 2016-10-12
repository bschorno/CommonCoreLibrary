using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public enum SrlType : byte
    {
        /// <summary>
        /// Undefine Tag
        /// </summary>
        Undefine = 0x00,

        /// <summary>
        /// Byte : 8bit
        /// </summary>
        Byte        = 0x01,

        /// <summary>
        /// Short : 16bit
        /// </summary>
        Int16       = 0x02,

        /// <summary>
        /// Int : 32bit
        /// </summary>
        Int32       = 0x03,

        /// <summary>
        /// Long : 64bit
        /// </summary>
        Int64       = 0x04,

        /// <summary>
        /// Float : 32bit
        /// </summary>
        Float       = 0x05,

        /// <summary>
        /// Double : 64bit
        /// </summary>
        Double      = 0x06,

        /// <summary>
        /// String : max. 2,147,483,647 chars
        /// </summary>
        String      = 0x07,

        /// <summary>
        /// List (same Tag) : unlimited
        /// </summary>
        List        = 0x08,

        /// <summary>
        /// Compound (different Tag) : unlimited
        /// </summary>
        Compound    = 0x09,

        /// <summary>
        /// Object : unlimited
        /// </summary>
        Object      = 0x0A,

        /// <summary>
        /// Char : 16bit
        /// </summary>
        Char        = 0x0B,
        
        /// <summary>
        /// UShort : 16bit
        /// </summary>
        UInt16      = 0x0C,
        
        /// <summary>
        /// UInt : 32bit
        /// </summary>
        UInt32      = 0x0D,
        
        /// <summary>
        /// ULong : 64bit
        /// </summary>
        UInt64      = 0x0E,

        /// <summary>
        /// Boolean : 1bit
        /// </summary>
        Boolean     = 0x0F,
    }
}
