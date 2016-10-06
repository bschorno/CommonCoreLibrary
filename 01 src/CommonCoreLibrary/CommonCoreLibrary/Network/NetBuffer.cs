using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Network
{
    public class NetBuffer
    {
        private byte[] _data;

        /// <summary>
        /// binary data
        /// </summary>
        public byte[] Data
        {
            get
            {
                return this._data;
            }
            set
            {
                this._data = value;
            }
        }

        /// <summary>
        /// length of this buffer
        /// </summary>
        public int Length
        {
            get
            {
                return this._data.Length;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NetBuffer()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">buffer content as byte array</param>
        /// <param name="offset">offset from given byte array</param>
        /// <param name="length">length of bytes to be overtaken</param>
        public NetBuffer(ref byte[] data, int offset, int length)
            : this()
        {
            this._data = new byte[length];
            Array.Copy(data, 0, this._data, offset, length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">buffer content as byte array</param>
        public NetBuffer(ref byte[] data)
            : this()
        {
            this._data = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length">length of buffer, creates empty byte array of this length</param>
        public NetBuffer(int length)
            : this()
        {
            this._data = new byte[length];
        }

        /// <summary>
        /// write an array of bytes into the buffer
        /// </summary>
        /// <param name="bytes">source bytes</param>
        /// <param name="sourceOffset">offset from source array</param>
        /// <param name="destinationPosition">insert position in destination array</param>
        /// <param name="length">count of overgiven bytes</param>
        public void WriteBytes(ref byte[] data, int sourceOffset, int destinationPosition, int length)
        {
            if (sourceOffset + length >= data.Length)
            {
                if (length >= data.Length)
                    length = data.Length - 1;
                sourceOffset = data.Length - length;
            }
            if (destinationPosition + length >= this._data.Length)
            {
                byte[] var1 = this._data;
                this._data = new byte[destinationPosition + length];
                Array.Copy(var1, this._data, var1.Length);
            }
            Array.Copy(data, sourceOffset, this._data, destinationPosition, length);
        }

        /// <summary>
        /// write an array of bytes into the buffer
        /// </summary>
        /// <param name="bytes">source bytes</param>
        /// <param name="sourceOffset">offset from source array</param>
        /// <param name="destinationPosition">insert position in destination array </param>
        public void WriteBytes(ref byte[] data, int sourceOffset, int destinationPosition)
        {
            this.WriteBytes(ref data, sourceOffset, destinationPosition, data.Length);
        }

        /// <summary>
        /// write an array of bytes into the buffer
        /// </summary>
        /// <param name="bytes">source bytes</param>
        /// <param name="destinationPosition">insert position in destination array</param>
        public void WriteBytes(ref byte[] data, int destinationPosition)
        {
            this.WriteBytes(ref data, 0, destinationPosition);
        }

        /// <summary>
        /// write a single byte into the buffer
        /// </summary>
        /// <param name="data">byte</param>
        /// <param name="destinationPosition">insert position in destination array</param>
        public void WriteByte(byte data, int destinationPosition)
        {
            if (destinationPosition >= this._data.Length)
                destinationPosition = this._data.Length - 1;
            this._data[destinationPosition] = data;
        }

        /// <summary>
        /// read an array of bytes
        /// </summary>
        /// <param name="position">read position in array</param>
        /// <param name="length">number of bytes to read</param>
        /// <returns>array of bytes</returns>
        public byte[] ReadBytes(int position, int length)
        {
            if (length <= 0)
                return new byte[0];
            if (position >= this._data.Length)
                position = this._data.Length - 1;
            if (position + length >= this._data.Length)
                length = this._data.Length - position;
            if (length > 1)
            {
                byte[] var1 = new byte[length];
                Array.Copy(this._data, position, var1, 0, length);
                return var1;
            }
            else
            {
                return new byte[] { this._data[position] };
            }
        }

        /// <summary>
        /// read single byte
        /// </summary>
        /// <param name="position">read position in array</param>
        /// <returns>byte</returns>
        public byte ReadByte(int position)
        {
            return this.ReadBytes(position, 1)[0];
        }

        /// <summary>
        /// clear buffer
        /// </summary>
        public void Clear()
        {
            this._data = new byte[0];
        }
    }
}