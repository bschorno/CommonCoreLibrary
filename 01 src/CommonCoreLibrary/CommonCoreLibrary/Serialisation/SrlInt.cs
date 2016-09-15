using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public sealed class SrlInt : SrlTag
    {
        private int _value;

        /// <summary>
        /// Type of this tag
        /// </summary>
        public override SrlType Type
        {
            get
            {
                return SrlType.Int32;
            }
        }

        /// <summary>
        /// Value
        /// </summary>
        public int Value
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
        internal SrlInt()
            : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public SrlInt(int value)
            : this()
        {
            this._value = value;
        }

        /// <summary>
        /// Get bytes of this tag
        /// </summary>
        /// <param name="stream"></param>
        internal override void GetBytes(SrlStreamWriter stream)
        {
            stream.WriteByte(BitConverter.GetBytes(this._value));
        }

        /// <summary>
        /// Set bytes for this tag
        /// </summary>
        /// <param name="stream"></param>
        internal override void SetBytes(SrlStreamReader stream)
        {
            this._value = BitConverter.ToInt32(stream.ReadByte(32), 0);
        }
    }
}
