using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public sealed class SrlDouble : SrlTag
    {
        private double _value;

        /// <summary>
        /// Types of this tag
        /// </summary>
        public override SrlType Type
        {
            get
            {
                return SrlType.Double;
            }
        }

        /// <summary>
        /// Value
        /// </summary>
        public double Value
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
        internal SrlDouble()
            : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public SrlDouble(double value)
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
            this._value = BitConverter.ToDouble(stream.ReadByte(64), 0);
        }
    }
}
