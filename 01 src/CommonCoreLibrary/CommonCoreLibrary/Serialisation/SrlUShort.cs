using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public sealed class SrlUShort : SrlTag
    {
        private ushort _value;

        /// <summary>
        /// Type of this tag
        /// </summary>
        public override SrlType Type
        {
            get
            {
                return SrlType.UInt16;
            }
        }

        /// <summary>
        /// Value
        /// </summary>
        public ushort Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        internal SrlUShort()
            : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public SrlUShort(ushort value)
            : this()
        {
            this._value = value;
        }

        /// <summary>
        /// Get bytes of this tag
        /// </summary>
        /// <param name="stream">Stream</param>
        internal override void GetBytes(SrlStreamWriter stream)
        {
            stream.WriteByte(BitConverter.GetBytes(this._value));
        }

        /// <summary>
        /// Set bytes for this tag
        /// </summary>
        /// <param name="stream">Stream</param>
        internal override void SetBytes(SrlStreamReader stream)
        {
            this._value = BitConverter.ToUInt16(stream.ReadByte(16), 0);
        }
    }
}
