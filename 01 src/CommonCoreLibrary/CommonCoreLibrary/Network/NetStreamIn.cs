using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary
{
    public class NetStreamIn : NetStream
    {
        /// <summary>
        /// return true if stream can read
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// return true if stream can write
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NetStreamIn()
            : base()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">data buffer</param>
        public NetStreamIn(NetBuffer buffer)
            : base(buffer)
        {

        }

        /// <summary>
        /// read bytes from stream
        /// </summary>
        /// <param name="offset">offset from current position</param>
        /// <param name="count">number of bytes to read</param>
        /// <returns>byte array</returns>
        public override byte[] Read(int offset, int count)
        {
            return this._buffer.ReadBytes(this._position + offset, count);
            this._position += offset;
        }

        /// <summary>
        /// read single byte from stream
        /// </summary>
        /// <param name="offset">offset</param>
        /// <returns>byte</returns>
        public byte ReadByte(int offset)
        {
            return this.Read(offset, 1)[0];
        }

        /// <summary>
        /// read single byte from stream
        /// </summary>
        /// <returns>byte</returns>
        public byte ReadByte()
        {
            return this.ReadByte(0);
        }


        /// <summary>
        /// read short from stream
        /// </summary>
        /// <param name="offset">offset</param>
        /// <returns>short</returns>
        public short ReadShort(int offset)
        {
            return BitConverter.ToInt16(this.Read(offset, 2), 0);
        }

        /// <summary>
        /// read short from stream
        /// </summary>
        /// <returns>short</returns>
        public short ReadShort()
        {
            return this.ReadShort(0);
        }

        /// <summary>
        /// read integer from stream
        /// </summary>
        /// <param name="offset">offset</param>
        /// <returns>integer</returns>
        public int ReadInt(int offset)
        {
            return BitConverter.ToInt32(this.Read(offset, 4), 0);
        }

        /// <summary>
        /// read integer from stream
        /// </summary>
        /// <returns>integer</returns>
        public int ReadInt()
        {
            return this.ReadInt(0);
        }

        /// <summary>
        /// read long from stream
        /// </summary>
        /// <param name="offset">offset</param>
        /// <returns>long</returns>
        public long ReadLong(int offset)
        {
            return BitConverter.ToInt64(this.Read(offset, 8), 0);
        }

        /// <summary>
        /// read long from stream
        /// </summary>
        /// <returns>long</returns>
        public long ReadLong()
        {
            return this.ReadLong(0);
        }

        /// <summary>
        /// read ushort from stream
        /// </summary>
        /// <param name="offset">offset</param>
        /// <returns>ushort</returns>
        public ushort ReadUShort(int offset)
        {
            return BitConverter.ToUInt16(this.Read(offset, 2), 0);
        }

        /// <summary>
        /// read ushort from stream
        /// </summary>
        /// <returns>ushort</returns>
        public ushort ReadUShort()
        {
            return this.ReadUShort(0);
        }

        /// <summary>
        /// read uint from stream
        /// </summary>
        /// <param name="offset">offset</param>
        /// <returns>uint</returns>
        public uint ReadUInt(int offset)
        {
            return BitConverter.ToUInt32(this.Read(offset, 4), 0);
        }

        /// <summary>
        /// read uint from stream
        /// </summary>
        /// <returns>uint</returns>
        public uint ReadUInt()
        {
            return this.ReadUInt(0);
        }


        /// <summary>
        /// read ulong from stream
        /// </summary>
        /// <param name="offset">offset</param>
        /// <returns>ulong</returns>
        public ulong ReadULong(int offset)
        {
            return BitConverter.ToUInt64(this.Read(offset, 8), 0);
        }

        /// <summary>
        /// read ulong from stream
        /// </summary>
        /// <returns>ulong</returns>
        public ulong ReadULong()
        {
            return this.ReadULong(0);
        }

        /// <summary>
        /// read float from stream
        /// </summary>
        /// <param name="offset">offset</param>
        /// <returns>float</returns>
        public float ReadFloat(int offset)
        {
            return BitConverter.ToSingle(this.Read(offset, 4), 0);
        }

        /// <summary>
        /// read float from stream
        /// </summary>
        /// <returns>float</returns>
        public float ReadFloat()
        {
            return this.ReadFloat(0);
        }

        /// <summary>
        /// read double from stream
        /// </summary>
        /// <param name="offset">offset</param>
        /// <returns>double</returns>
        public double ReadDouble(int offset)
        {
            return BitConverter.ToDouble(this.Read(offset, 8), 0);
        }

        /// <summary>
        /// read double from stream
        /// </summary>
        /// <returns>double</returns>
        public double ReadDouble()
        {
            return this.ReadDouble(0);
        }

        /// <summary>
        /// read string from stream
        /// </summary>
        /// <param name="length">number of char</param>
        /// <param name="offset">offset</param>
        /// <returns>string</returns>
        public string ReadString(int length, int offset)
        {
            byte[] var1 = this.Read(offset, length * 2);
            char[] var2 = new char[length];
            for (int var3 = 0; var3 <= length; var3++)
            {
                var2[var3] = BitConverter.ToChar(new byte[] { var1[var3 * 2],
                                                              var1[var3 * 2 + 1] }, 0);
            }
            return new string(var2);
        }

        /// <summary>
        /// read string from stream
        /// </summary>
        /// <param name="length">number of char</param>
        /// <returns>string</returns>
        public string ReadString(int length)
        {
            return this.ReadString(length, 0);
        }
    }
}