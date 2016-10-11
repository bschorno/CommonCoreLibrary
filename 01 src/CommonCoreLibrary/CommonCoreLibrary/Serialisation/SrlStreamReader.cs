using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace CommonCoreLibrary.Serialisation
{
    internal class SrlStreamReader : BinaryReader
    {
        private BitArray _bitBufferArray;
        private byte     _bitBufferCount;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream">Stream</param>
        internal SrlStreamReader(Stream stream)
            : base(stream)
        {
            if (!stream.CanRead)
            {
                throw new SrlException("Stream isn't readable");
            }

            this._bitBufferArray = new BitArray(8);
            this._bitBufferCount = 0;
        }

        /// <summary>
        /// Reads bit from stream
        /// </summary>
        /// <param name="count">Number of bits to read</param>
        /// <returns>BitArray</returns>
        private BitArray ReadBitArray(int count)
        {
            BitArray var1 = new BitArray(count);
            BitArray var2 = new BitArray(this._bitBufferArray);

            if (count > this._bitBufferCount)
            {
                var2.Length = count + this._bitBufferCount + (8 - (count % 8 == 0 ? 8 : count % 8));

                int var3 = count >> 3;
                if (var3 == 0)
                    var3 = 1;
                byte[] var5 = this.ReadBytes(var3);

                for (int var6 = 0; var6 < var5.Length; var6++)
                {
                    int var7 = this._bitBufferCount + (var6 * 8);
                    for (int var8 = 0; var8 < 8; var8++)
                        var2[var7 + var8] = ((var5[var6] & (1 << (7 - var8))) != 0);
                }

                this._bitBufferArray = new BitArray(8);
                this._bitBufferCount = 0;
                for (int var6 = 0; var6 < var2.Count; var6++)
                {
                    if (var6 < count)
                        var1[var6] = var2[var6];
                    else
                        this._bitBufferArray.Set(this._bitBufferCount++, var2[var6]);
                }
            }
            else
            {
                var2.Length = this._bitBufferCount;

                this._bitBufferArray = new BitArray(8);
                this._bitBufferCount = 0;
                for (int var3 = 0; var3 < var2.Count; var3++)
                {
                    if (var3 < count)
                        var1[var3] = var2[var3];
                    else
                        this._bitBufferArray.Set(this._bitBufferCount++, var2[var3]);
                }
            }

            return var1;
        }

        /// <summary>
        /// Read bytes from stream
        /// </summary>
        /// <param name="count">Number of bytes to read</param>
        /// <returns></returns>
        internal byte[] ReadByte(int count)
        {
            return this.ReadBitArray(count).ToByteArray();
        }

        /// <summary>
        /// Read SrlType from stream
        /// </summary>
        /// <returns>SrlType</returns>
        internal SrlType ReadType()
        {
            return (SrlType)this.ReadBitArray(4).ToByteArray()[0];
        }        

        /// <summary>
        /// Read bits from stream
        /// </summary>
        /// <param name="count">Number of bits to read</param>
        /// <returns></returns>
        internal bool[] ReadBit(int count)
        {
            bool[] var1 = new bool[count];
            this.ReadBitArray(count).CopyTo(var1, 0);
            return var1;
        }
    }
}
