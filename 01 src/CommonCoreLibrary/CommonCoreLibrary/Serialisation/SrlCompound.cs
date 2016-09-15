using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public sealed class SrlCompound : SrlTag, IList<SrlTag>, IList
    {
        private readonly List<SrlTag> _value = new List<SrlTag>();

        private int _readIndex = 0;

        /// <summary>
        /// Type of this tag
        /// </summary>
        public override SrlType Type
        {
            get
            {
                return SrlType.Compound;
            }
        }

        /// <summary>
        /// Read index for returning tag
        /// </summary>
        public int ReadIndex
        {
            get
            {
                return this._readIndex;
            }
            set
            {
                this._readIndex = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SrlCompound()
            : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tags">Tags</param>
        public SrlCompound(params SrlTag[] tags)
            : this()
        {
            this._value.AddRange(tags);
        }

        /// <summary>
        /// Get tag at index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Tag</returns>
        public SrlTag this[int index]
        {
            get
            {
                return this._value[index];
            }
            set
            {
                if (value == null)
                    throw new SrlException("Value can't be NULL");
                else if (value == this)
                    throw new SrlException(string.Format("This list can't added to itself"));

                this._value[index] = value;
            }
        }

        public IEnumerator<SrlTag> GetEnumerator()
        {
            return this._value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._value.GetEnumerator();
        }

        public int IndexOf(SrlTag tag)
        {
            if (tag == null)
                throw new SrlException("Value can't be NULL");
            return this._value.IndexOf(tag);
        }

        public void Insert(int index, SrlTag tag)
        {
            if (tag == null)
                throw new SrlException("Value can't be NULL");
            else if (tag == this)
                throw new SrlException(string.Format("This list can't added to itself"));

            this._value.Insert(index, tag);
        }

        public void RemoveAt(int index)
        {
            this._value.RemoveAt(index);
        }

        public void Add(SrlTag tag)
        {
            if (tag == null)
                throw new SrlException("Value can't be NULL");
            else if (tag == this)
                throw new SrlException(string.Format("This list can't added to itself"));

            this._value.Add(tag);
        }

        public void Clear()
        {
            this._value.Clear();
        }

        public bool Contains(SrlTag tag)
        {
            if (tag == null)
                throw new SrlException("Value can't be NULL");
            return this._value.Contains(tag);
        }

        public void CopyTo(SrlTag[] tag, int arrayIndex)
        {
            this._value.CopyTo(tag, arrayIndex);
        }

        public bool Remove(SrlTag tag)
        {
            if (tag == null)
                throw new SrlException("Value can't be NULL");
            return this._value.Remove(tag);
        }

        public int Count
        {
            get
            {
                return this._value.Count;
            }
        }

        bool ICollection<SrlTag>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        void IList.Remove(object value)
        {
            this.Remove((SrlTag)value);
        }

        object IList.this[int index]
        {
            set
            {
                this[index] = (SrlTag)value;
            }
            get
            {
                return this[index];
            }
        }

        int IList.Add(object value)
        {
            this.Add((SrlTag)value);
            return (this._value.Count - 1);
        }

        bool IList.Contains(object value)
        {
            return this.Contains((SrlTag)value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((SrlTag)value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (SrlTag)value);
        }

        bool IList.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        bool IList.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo((SrlTag[])array, index);
        }

        int ICollection.Count
        {
            get
            {
                return this.Count;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection)this._value).SyncRoot;
            }
        }

        /// <summary>
        /// Get bytes of this tag
        /// </summary>
        /// <param name="stream">Stream</param>
        internal override void GetBytes(SrlStreamWriter stream)
        {
            stream.WriteByte(BitConverter.GetBytes(this._value.Count));

            foreach (SrlTag var2 in this._value)
            {
                stream.WriteType(var2.Type);
                var2.GetBytes(stream);
            }
        }

        /// <summary>
        /// Set bytes for this tag
        /// </summary>
        /// <param name="stream">Stream</param>
        internal override void SetBytes(SrlStreamReader stream)
        {
            int var1 = BitConverter.ToInt32(stream.ReadByte(32), 0);

            for (int var2 = 0; var2 < var1; var2++)
            {
                SrlType var3 = stream.ReadType();
                SrlTag var4 = SrlTag.GetInstance(var3);
                var4.SetBytes(stream);
                this._value.Add(var4);
            }
        }

        /// <summary>
        /// Get tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlTag GetTag()
        {
            return this._value.Count < this._readIndex + 1 ? null : this._value[_readIndex++];
        }

        /// <summary>
        /// Get byte-tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlByte GetByte()
        {
            return this.GetTag() as SrlByte ?? new SrlByte();
        }

        /// <summary>
        /// Get short-tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlShort GetShort()
        {
            return this.GetTag() as SrlShort ?? new SrlShort();
        }

        /// <summary>
        /// Get int-tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlInt GetInt()
        {
            return this.GetTag() as SrlInt ?? new SrlInt();
        }

        /// <summary>
        /// Get long-tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlLong GetLong()
        {
            return this.GetTag() as SrlLong ?? new SrlLong();
        }

        /// <summary>
        /// Get float-tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlFloat GetFloat()
        {
            return this.GetTag() as SrlFloat ?? new SrlFloat();
        }

        /// <summary>
        /// Get double-tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlDouble GetDouble()
        {
            return this.GetTag() as SrlDouble ?? new SrlDouble();
        }

        /// <summary>
        /// Get string-tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlString GetString()
        {
            return this.GetTag() as SrlString ?? new SrlString();
        }

        /// <summary>
        /// Get list-tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlList GetList()
        {
            return this.GetTag() as SrlList ?? new SrlList();
        }

        /// <summary>
        /// Get compound-tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlCompound GetCompound()
        {
            return this.GetTag() as SrlCompound ?? new SrlCompound();
        }
    }
}
