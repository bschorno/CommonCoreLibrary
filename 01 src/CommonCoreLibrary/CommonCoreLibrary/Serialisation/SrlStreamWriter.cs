using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace CommonCoreLibrary.Serialisation
{
    internal class SrlStreamWriter : BinaryWriter
    {
        private BitArray _bitBufferArray;
        private byte     _bitBufferCount;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream">Stream</param>
        internal SrlStreamWriter(Stream stream)
            : base(stream)
        {
            if (!stream.CanWrite)
            {
                throw new SrlException("Stream isn't writable", this);
            }

            this._bitBufferArray = new BitArray(8);
            this._bitBufferCount = 0;
        }
        
        /// <summary>
        /// Commit and clear buffers
        /// </summary>
        public override void Flush()
        {
            this.Write(this._bitBufferArray.ToByteArray()[0]);
            this._bitBufferArray.SetAll(false);
            this._bitBufferCount = 0;
            base.Flush();
        }

        /// <summary>
        /// Write bits from BitArray to stream
        /// </summary>
        /// <param name="bitArray">BitArray</param>
        /// <param name="length">Number of bits</param>
        private void WriteBitArray(BitArray bitArray, int length)
        {
            for (int var1 = 0; var1 < length; var1++)
            {
                this._bitBufferArray[this._bitBufferCount++] = bitArray[var1];
                if (this._bitBufferCount == 8)
                {
                    byte var3 = 0;
                    for (int var2 = 0; var2 < 8; var2++)
                    {
                        if (this._bitBufferArray[var2])
                            var3 |= (byte)(1 << (7 - var2));
                        else
                            var3 &= (byte)~(1 << (7 - var2));
                    }
                    this.Write(var3);
                    this._bitBufferArray.SetAll(false);
                    this._bitBufferCount = 0;
                }
            }
        }

        /// <summary>
        /// Write bytes to stream
        /// </summary>
        /// <param name="bytes">Byte array</param>
        internal void WriteByte(byte[] bytes)
        {
            BitArray var1 = new BitArray(bytes);
            this.WriteBitArray(var1, var1.Count);
        }

        /// <summary>
        /// Write SrlType to stream
        /// </summary>
        /// <param name="type">SrlType</param>
        internal void WriteType(SrlType type)
        {
            BitArray var1 = new BitArray(new byte[] { (byte)type });
            this.WriteBitArray(var1, 4);
        }
    }
}
