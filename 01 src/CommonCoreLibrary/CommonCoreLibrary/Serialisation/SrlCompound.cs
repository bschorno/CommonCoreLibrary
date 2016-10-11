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

        /// <summary>
        /// Returns an enumerator that iterates trought the List
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<SrlTag> GetEnumerator()
        {
            return this._value.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates trought the List
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._value.GetEnumerator();
        }

        /// <summary>
        /// Search for element and return index
        /// </summary>
        /// <param name="tag">Element</param>
        /// <returns>Index</returns>
        public int IndexOf(SrlTag tag)
        {
            if (tag == null)
                throw new SrlException("Value can't be NULL");
            return this._value.IndexOf(tag);
        }

        /// <summary>
        /// Insert tag at specifig index
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="tag">Tag</param>
        public void Insert(int index, SrlTag tag)
        {
            if (tag == null)
                throw new SrlException("Value can't be NULL");
            else if (tag == this)
                throw new SrlException(string.Format("This list can't added to itself"));

            this._value.Insert(index, tag);
        }

        /// <summary>
        /// Remove tag at specifig index
        /// </summary>
        /// <param name="index">Index</param>
        public void RemoveAt(int index)
        {
            this._value.RemoveAt(index);
        }

        /// <summary>
        /// Add tag to list at last position
        /// </summary>
        /// <param name="tag">Tag</param>
        public void Add(SrlTag tag)
        {
            if (tag == null)
                throw new SrlException("Value can't be NULL");
            else if (tag == this)
                throw new SrlException(string.Format("This list can't added to itself"));

            this._value.Add(tag);
        }

        /// <summary>
        /// Remove all elements from list
        /// </summary>
        public void Clear()
        {
            this._value.Clear();
        }

        /// <summary>
        /// Check if list contains tag
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <returns></returns>
        public bool Contains(SrlTag tag)
        {
            if (tag == null)
                throw new SrlException("Value can't be NULL");
            return this._value.Contains(tag);
        }

        /// <summary>
        /// Copy the entire list to an one dimensional array
        /// </summary>
        /// <param name="tag">Array of tag</param>
        /// <param name="arrayIndex">Starting index from target array</param>
        public void CopyTo(SrlTag[] tag, int arrayIndex)
        {
            this._value.CopyTo(tag, arrayIndex);
        }

        /// <summary>
        /// Remove tag from list
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <returns></returns>
        public bool Remove(SrlTag tag)
        {
            if (tag == null)
                throw new SrlException("Value can't be NULL");
            return this._value.Remove(tag);
        }

        /// <summary>
        /// Get numbers of element containing in this list
        /// </summary>
        public int Count
        {
            get
            {
                return this._value.Count;
            }
        }

        /// <summary>
        /// Is readonly
        /// </summary>
        bool ICollection<SrlTag>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Remove object from this list
        /// </summary>
        /// <param name="value">Object</param>
        void IList.Remove(object value)
        {
            this.Remove((SrlTag)value);
        }

        /// <summary>
        /// Get object at index
        /// </summary>
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

        /// <summary>
        /// Add object to list
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Insered index</returns>
        int IList.Add(object value)
        {
            this.Add((SrlTag)value);
            return (this._value.Count - 1);
        }
        
        /// <summary>
        /// Check if object is contained in list
        /// </summary>
        /// <param name="value">Object to search for</param>
        /// <returns></returns>
        bool IList.Contains(object value)
        {
            return this.Contains((SrlTag)value);
        }

        /// <summary>
        /// Get index of an object
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Index</returns>
        int IList.IndexOf(object value)
        {
            return this.IndexOf((SrlTag)value);
        }

        /// <summary>
        /// Insert object at specifig index
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="value">Object</param>
        void IList.Insert(int index, object value)
        {
            this.Insert(index, (SrlTag)value);
        }

        /// <summary>
        /// Has list an fixed size
        /// </summary>
        bool IList.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Is list readonly
        /// </summary>
        bool IList.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Remove object at specifig index
        /// </summary>
        /// <param name="index">Index</param>
        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        /// <summary>
        /// Copy entire list into an one dimensional array
        /// </summary>
        /// <param name="array">Target array</param>
        /// <param name="index">Start index in target array</param>
        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo((SrlTag[])array, index);
        }

        /// <summary>
        /// Get count of objects containing in this list
        /// </summary>
        int ICollection.Count
        {
            get
            {
                return this.Count;
            }
        }

        /// <summary>
        /// Is list synchronized
        /// </summary>
        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Syncronize root
        /// </summary>
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
        /// Get boolean-tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlBoolean GetBoolean()
        {
            return this.GetTag() as SrlBoolean ?? new SrlBoolean();
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
        /// Get char-tag
        /// </summary>
        /// <returns>Tag</returns>
        public SrlChar GetChar()
        {
            return this.GetTag() as SrlChar ?? new SrlChar();
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
