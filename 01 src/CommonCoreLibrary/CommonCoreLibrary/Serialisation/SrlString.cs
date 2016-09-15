using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public sealed class SrlString : SrlTag
    {
        private string _value;

        /// <summary>
        /// Type of this tag
        /// </summary>
        public override SrlType Type
        {
            get
            {
                return SrlType.String;
            }
        }

        /// <summary>
        /// Value
        /// </summary>
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                if (this._value.Length < int.MaxValue)
                    this._value = value;
                else
                    this._value = value.Substring(0, int.MaxValue);
            }
        }

        /// <summary>
        /// Length of the value
        /// </summary>
        public int Length
        {
            get
            {
                return this._value.Length;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        internal SrlString()
            : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public SrlString(string value)
            : this()
        {
            if (value.Length < int.MaxValue)
                this._value = value;
            else
                this._value = value.Substring(0, int.MaxValue);
        }

        /// <summary>
        /// Get bytes of this tag
        /// </summary>
        /// <param name="stream">Stream</param>
        internal override void GetBytes(SrlStreamWriter stream)
        {
            int var1 = this._value.Length;
            byte[] var2 = new byte[var1 * 2];
            for (int var3 = 0; var3 < var2.Length; var3 += 2)
            {
                byte[] var4 = BitConverter.GetBytes(this._value[var3 / 2]);
                var2[var3] = var4[0];
                var2[var3 + 1] = var4[1];
            }
            stream.WriteByte(BitConverter.GetBytes(var1));
            stream.WriteByte(var2);
        }

        /// <summary>
        /// Set bytes for this tag
        /// </summary>
        /// <param name="stream">Stream</param>
        internal override void SetBytes(SrlStreamReader stream)
        {
            int var1 = BitConverter.ToInt32(stream.ReadByte(32), 0);
            byte[] var2 = stream.ReadByte(var1 * 16);
            for (int var3 = 0; var3 < var2.Length; var3 += 2)
            {
                byte[] var4 = new byte[] { var2[var3], var2[var3 + 1] };
                char var5 = BitConverter.ToChar(var4, 0);
                this._value += var5;
            }
        }
    }
}
