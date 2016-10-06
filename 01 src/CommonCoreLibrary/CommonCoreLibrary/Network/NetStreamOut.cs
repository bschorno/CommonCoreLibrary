using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Network
{
    public class NetStreamOut : NetStream
    {        
        /// <summary>
        /// return true if stream can read
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// return true if stream can write
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NetStreamOut()
            : base()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">data buffer</param>
        public NetStreamOut(NetBuffer buffer)
            : base(buffer)
        {

        }

        /// <summary>
        /// write bytes to stream
        /// </summary>
        /// <param name="data">byte array</param>
        /// <param name="offset">offset from current position</param>
        /// <param name="count">count of bytes</param>
        public override void Write(ref byte[] data, int offset, int count)
        {
            this._buffer.WriteBytes(ref data, 0, this._position + offset, count);
            this._position += offset;
        }

        /// <summary>
        /// write single byte into stream
        /// </summary>
        /// <param name="value">byte</param>
        /// <param name="offset">offset</param>
        public void WriteByte(byte value, int offset)
        {
            byte[] var1 = new byte[] { value };
            this.Write(ref var1, offset, 1);
        }

        /// <summary>
        /// write single byte into stream
        /// </summary>
        /// <param name="value">byte</param>
        public void WriteByte(byte value)
        {
            this.WriteByte(value, 0);
        }

        /// <summary>
        /// write short into stream
        /// </summary>
        /// <param name="value">short</param>
        /// <param name="offset">offset</param>
        public void WriteShort(short value, int offset)
        {
            byte[] var1 = BitConverter.GetBytes(value);
            this.Write(ref var1, offset, 2);
        }

        /// <summary>
        /// write short into stream
        /// </summary>
        /// <param name="value">short</param>
        public void WriteShort(short value)
        {
            this.WriteShort(value, 0);
        }

        /// <summary>
        /// write integer into stream
        /// </summary>
        /// <param name="value">integer</param>
        /// <param name="offset">offset</param>
        public void WriteInt(int value, int offset)
        {
            byte[] var1 = BitConverter.GetBytes(value);
            this.Write(ref var1, offset, 4);
        }

        /// <summary>
        /// write integer into stream
        /// </summary>
        /// <param name="value">integer</param>
        public void WriteInt(int value)
        {
            this.WriteInt(value, 0);
        }

        /// <summary>
        /// write long into stream
        /// </summary>
        /// <param name="value">long</param>
        /// <param name="offset">offset</param>
        public void WriteLong(long value, int offset)
        {
            byte[] var1 = BitConverter.GetBytes(value);
            this.Write(ref var1, offset, 8);
        }

        /// <summary>
        /// write long into stream
        /// </summary>
        /// <param name="value">long</param>
        public void WriteLong(long value)
        {
            this.WriteLong(value, 0);
        }

        /// <summary>
        /// write ushort into stream
        /// </summary>
        /// <param name="value">ushort</param>
        /// <param name="offset">offset</param>
        public void WriteUShort(ushort value, int offset)
        {
            byte[] var1 = BitConverter.GetBytes(value);
            this.Write(ref var1, offset, 2);
        }

        /// <summary>
        /// write ushort into stream
        /// </summary>
        /// <param name="value">ushort</param>
        public void WriteUShort(ushort value)
        {
            this.WriteUShort(value, 0);
        }

        /// <summary>
        /// write uint into stream
        /// </summary>
        /// <param name="value">uint</param>
        /// <param name="offset">offset</param>
        public void WriteUInt(uint value, int offset)
        {
            byte[] var1 = BitConverter.GetBytes(value);
            this.Write(ref var1, offset, 4);
        }

        /// <summary>
        /// write uint into stream
        /// </summary>
        /// <param name="value">uint</param>
        public void WriteUInt(uint value)
        {
            this.WriteUInt(value, 0);
        }

        /// <summary>
        /// write ulong into stream
        /// </summary>
        /// <param name="value">ulong</param>
        /// <param name="offset">offset</param>
        public void WriteULong(ulong value, int offset)
        {
            byte[] var1 = BitConverter.GetBytes(value);
            this.Write(ref var1, offset, 8);
        }

        /// <summary>
        /// write ulong into stream
        /// </summary>
        /// <param name="value">ulong</param>
        public void WriteULong(ulong value)
        {
            this.WriteULong(value, 0);
        }

        /// <summary>
        /// write float into stream
        /// </summary>
        /// <param name="value">float</param>
        /// <param name="offset">offset</param>
        public void WriteFloat(float value, int offset)
        {
            byte[] var1 = BitConverter.GetBytes(value);
            this.Write(ref var1, offset, 4);
        }

        /// <summary>
        /// write float into stream
        /// </summary>
        /// <param name="value">float</param>
        public void WriteFloat(float value)
        {
            this.WriteFloat(value, 0);
        }

        /// <summary>
        /// write double into stream
        /// </summary>
        /// <param name="value">double</param>
        /// <param name="offset">offset</param>
        public void WriteDouble(double value, int offset)
        {
            byte[] var1 = BitConverter.GetBytes(value);
            this.Write(ref var1, offset, 8);
        }
        
        /// <summary>
        /// write double into stream
        /// </summary>
        /// <param name="value">double</param>
        public void WriteDouble(double value)
        {
            this.WriteDouble(value, 0);
        }

        /// <summary>
        /// write string into stream
        /// </summary>
        /// <param name="value">string</param
        /// <param name="length">number of char</param>
        /// <param name="offset">offset</param>
        public void WriteString(string value, int length, int offset)
        {
            byte[] var1 = new byte[length * 2];
            for (int var2 = 0; var2 <= length; var2++)
            {
                if (var2 >= value.Length)
                    break;
                byte[] var3 = BitConverter.GetBytes(value[var2]);
                var1[var2 * 2]     = var3[0];
                var1[var2 * 2 + 1] = var3[1];
            }
            this.Write(ref var1, offset, length * 2);
        }

        /// <summary>
        /// write string into stream
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="length">number of char</param>
        public void WriteString(string value, int length)
        {
            this.WriteString(value, length, 0);
        }
    }
}