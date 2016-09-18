using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary
{
    public class NetChunk
    {
        private NetBuffer    _buffer;
        private short        _mcn;
        private short        _min;

        /// <summary>
        /// buffer
        /// </summary>
        public NetBuffer Buffer
        {
            get
            {
                return this._buffer;
            }
            set
            {
                this._buffer = value;
            }
        }
        
        /// <summary>
        /// message count number
        /// </summary>
        public short MCN
        {
            get
            {
                return this._mcn;
            }
        }

        /// <summary>
        /// message identification number
        /// </summary>
        public short MIN
        {
            get
            {
                return this._min;
            }
        }

        private NetChunk()
        {
            
        }

        public NetChunk(short mcn, short min)
            : this()
        {
            this._mcn = mcn;
            this._min = min;
        }
    }
}