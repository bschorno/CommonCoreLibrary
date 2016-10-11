using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public sealed class SrlBoolean : SrlTag
    {
        private bool _value;

        /// <summary>
        /// Type of this tag
        /// </summary>
        public override SrlType Type
        {
            get
            {
                return SrlType.Boolean;
            }
        }

        /// <summary>
        /// Value
        /// </summary>
        public bool Value
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
        internal SrlBoolean()
            : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public SrlBoolean(bool value)
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
            stream.WriteBit(new bool[] { this._value });
        }

        /// <summary>
        /// Set bytes for this tag
        /// </summary>
        /// <param name="stream">Stream</param>
        internal override void SetBytes(SrlStreamReader stream)
        {
            this._value = stream.ReadBit(1)[0];
        }
    }
}
