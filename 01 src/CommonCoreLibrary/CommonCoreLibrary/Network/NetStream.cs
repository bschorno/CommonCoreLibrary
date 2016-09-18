using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary
{
    public abstract class NetStream
    {
        protected NetBuffer   _buffer;
        protected int         _position       = 0;

        /// <summary>
        /// buffer
        /// </summary>
        public NetBuffer Buffer
        {
            get
            {
                return this._buffer;
            }
        }

        /// <summary>
        /// return true if stream can read
        /// </summary>
        public abstract bool CanRead
        {
            get;
        }

        /// <summary>
        /// return true if stream can write
        /// </summary>
        public abstract bool CanWrite
        {
            get;
        }


        /// <summary>
        /// Position in stream
        /// </summary>
        public int Position
        {
            get
            {
                return this._position;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NetStream()
            : this(new NetBuffer())
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">data buffer</param>
        public NetStream(NetBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException("Is null", "buffer");

            this._buffer = buffer;
        }

        /// <summary>
        /// write bytes to stream
        /// </summary>
        /// <param name="data">byte array</param>
        /// <param name="offset">offset from current position</param>
        /// <param name="count">count of bytes</param>
        public virtual void Write(ref byte[] data, int offset, int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// write bytes to stream at current position
        /// </summary>
        /// <param name="data">byte array</param>
        public virtual void Write(ref byte[] data)
        {
            this.Write(ref data, 0, data.Length);
        }

        /// <summary>
        /// read bytes from stream
        /// </summary>
        /// <param name="offset">offset from current position</param>
        /// <param name="count">number of bytes to read</param>
        /// <returns>byte array</returns>
        public virtual byte[] Read(int offset, int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// read bytes from stream at current position
        /// </summary>
        /// <param name="count">number of bytes to read</param>
        /// <returns>byte array</returns>
        public virtual byte[] Read(int count)
        {
            return this.Read(0, count);
        }

        /// <summary>
        /// seek in stream
        /// </summary>
        /// <param name="offset">offset</param>
        /// <param name="origin">origin from where to seek</param>
        public virtual void Seek(int offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this._position = offset;
                    if (this._position >= this._buffer.Length)
                        this._position = this._buffer.Length - 1;
                    break;
                case SeekOrigin.Current:
                    this._position += offset;
                    if (this._position >= this._buffer.Length)
                        this._position = this._buffer.Length - 1;
                    break;
                case SeekOrigin.End:
                    this._position = this._buffer.Length - offset;
                    if (this._position < 0)
                        this._position = 0;
                    break;
            }
        }

        /// <summary>
        /// seek in stream from current posititon
        /// </summary>
        /// <param name="offset">offset</param>
        public virtual void Seek(int offset)
        {
            this.Seek(offset, SeekOrigin.Current);
        }
    }
}