using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CommonCoreLibrary.Serialisation
{
    internal static class SrlHelper
    {
        /// <summary>
        /// Connverts BitArray to array of bytes
        /// </summary>
        /// <param name="array">BitArray</param>
        /// <returns>array of bytes</returns>
        internal static byte[] ToByteArray(this BitArray array)
        {
            BitArray var5 = array.Normalize();

            int var1 = var5.Count / 8;

            byte[] var2 = new byte[var1];
            int var3Byte = 0;
            int var3Bit = 0;

            for (int var4 = 0; var4 < var5.Count; var4++)
            {
                if (var5[var4])
                    var2[var3Byte] |= (byte)(1 << (var3Bit));
                else
                    var2[var3Byte] &= (byte)~(1 << (var3Bit));

                if (++var3Bit == 8)
                {
                    var3Bit = 0;
                    var3Byte++;
                }
            }

            return var2;
        }

        /// <summary>
        /// Normalize BitArray -> until a byte is complete
        /// </summary>
        /// <param name="array">BitArray</param>
        /// <returns>BitArray</returns>
        internal static BitArray Normalize(this BitArray array)
        {
            if (array.Count % 8 == 0)
                return new BitArray(array);

            BitArray var1 = new BitArray(array);
            var1.Length += array.Count % 8;

            return var1;
        }
    }
}
