using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public abstract class SrlTag
    {
        /// <summary>
        /// Type of this tag
        /// </summary>
        public abstract SrlType Type
        {
            get;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        internal SrlTag()
        {

        }

        /// <summary>
        /// Get bytes of this tag
        /// </summary>
        /// <param name="stream">Stream</param>
        internal abstract void GetBytes(SrlStreamWriter stream);

        /// <summary>
        /// Set bytes for this tag
        /// </summary>
        /// <param name="stream">Stream</param>
        internal abstract void SetBytes(SrlStreamReader stream);

        internal static SrlTag GetInstance(SrlType type)
        {
            switch (type)
            {
                case SrlType.Undefine:
                    throw new SrlException("SrlType can't be undefine!");
                case SrlType.Byte:
                    return new SrlByte();
                case SrlType.Int16:
                    return new SrlShort();
                case SrlType.Int32:
                    return new SrlInt();
                case SrlType.Int64:
                    return new SrlLong();
                case SrlType.Float:
                    return new SrlFloat();
                case SrlType.Double:
                    return new SrlDouble();
                case SrlType.String:
                    return new SrlString();
                case SrlType.List:
                    return new SrlList();
                case SrlType.Compound:
                    return new SrlCompound();
                case SrlType.Boolean:
                    return new SrlBoolean();
                case SrlType.Char:
                    return new SrlChar();
                case SrlType.Object:
                    return new SrlObject();
                case SrlType.End:
                    throw new SrlException("Invalid SrlType!");
                default:
                    throw new SrlException("Invalid SrlType!");
            }
            return null;
        }
    }
}
