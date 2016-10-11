using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CommonCoreLibrary.Serialisation
{
    public sealed class SrlObject : SrlTag
    {
        private object _value;

        /// <summary>
        /// 
        /// </summary>
        public override SrlType Type
        {
            get
            {
                return SrlType.Object;
            }
        }

        public object Value
        {
            get
            {
                return this._value;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                this._value = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        internal SrlObject()
            : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value</param>
        public SrlObject(object value)
            : this()
        {
            if (value == null)
                throw new ArgumentNullException("value");
            this._value = value;
        }

        /// <summary>
        /// Get bytes of this tag
        /// </summary>
        /// <param name="stream"></param>
        internal override void GetBytes(SrlStreamWriter stream)
        {
            Type var1 = this._value.GetType();
            if (var1.IsClass)
            {
                this.WriteString(stream, var1.AssemblyQualifiedName);
                this.WriteString(stream, var1.FullName);
                
            }
        }

        /// <summary>
        /// Set bytes for this tag
        /// </summary>
        /// <param name="stream"></param>
        internal override void SetBytes(SrlStreamReader stream)
        {
            string var1AssemblyName = this.ReadString(stream);
            string var1ClassName    = this.ReadString(stream);
        }

        /// <summary>
        /// Write string to stream
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="value">String</param>
        private void WriteString(SrlStreamWriter stream, string value)
        {
            int var1 = value.Length;
            byte[] var2 = new byte[var1 * 2];
            for (int var3 = 0; var3 < var2.Length; var3 += 2)
            {
                byte[] var4 = BitConverter.GetBytes(value[var3 / 2]);
                var2[var3] = var4[0];
                var2[var3 + 1] = var4[1];
            }
            stream.WriteByte(BitConverter.GetBytes(var1));
            stream.WriteByte(var2);
        }

        /// <summary>
        /// Read string from stream
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>String</returns>
        private string ReadString(SrlStreamReader stream)
        {
            int var1 = BitConverter.ToInt32(stream.ReadByte(32), 0);
            byte[] var2 = stream.ReadByte(var1 * 16);
            string var6 = string.Empty;
            for (int var3 = 0; var3 < var2.Length; var3 += 2)
            {
                byte[] var4 = new byte[] { var2[var3], var2[var3 + 1] };
                char var5 = BitConverter.ToChar(var4, 0);
                var6 += var5;
            }
            return var6;
        }
    }
}
