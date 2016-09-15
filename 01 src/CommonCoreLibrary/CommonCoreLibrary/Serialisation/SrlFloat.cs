using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public sealed class SrlFloat : SrlTag
    {
        private float _value;

        /// <summary>
        /// 
        /// </summary>
        public override SrlType Type
        {
            get
            {
                return SrlType.Float;
            }
        }

        /// <summary>
        /// Value
        /// </summary>
        public float Value
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
        internal SrlFloat()
            : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public SrlFloat(float value)
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
            this._value = BitConverter.ToSingle(stream.ReadByte(32), 0);
        }
    }
}
