using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public sealed class SrlList : SrlTag, IList<SrlTag>, IList
    {
        private readonly List<SrlTag> _value = new List<SrlTag>();
        private SrlType               _listType = SrlType.Undefine;

        /// <summary>
        /// Type of this tag
        /// </summary>
        public override SrlType Type
        {
            get
            {
                return SrlType.List;
            }
        }

        /// <summary>
        /// Type of this list
        /// </summary>
        public SrlType ListType
        {
            get
            {
                return this._listType;
            }
            set
            {
                if (this._value.Count > 0)
                {
                    SrlType var1 = this._value[0].Type;
                    if (var1 != value)
                        throw new SrlException(string.Format("Given Type '{0}' doesn't match to the current element type '{1}'",
                                                             value,
                                                             var1), this);
                }
                if (value == SrlType.Undefine)
                    throw new SrlException(string.Format("Invalid SrlType '{0}'",
                                                         value), this);
                this._listType = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        internal SrlList()
            : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Type of this list</param>
        public SrlList(SrlType type)
            : this()
        {
            if (type == SrlType.Undefine)
                throw new SrlException(string.Format("Invalid SrlType '{0}'",
                                                     type), this);
            this._listType = type;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Type of this list</param>
        /// <param name="tags">Tags</param>
        public SrlList(SrlType type, params SrlTag[] tags)
            : this(type)
        {
            foreach (SrlTag var1 in tags)
            {
                if (var1.Type == this._listType)
                    this._value.Add(var1);
                else
                    throw new SrlException(string.Format("Given Type '{0}' doesn't match to the current element type '{1}'",
                                                         var1.Type,
                                                         this._listType), this);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tags">Tags</param>
        public SrlList(params SrlTag[] tags)
            : this(tags.Count() > 0 ? tags[0].Type : SrlType.Undefine, tags)
        {

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

                if (this._listType != SrlType.Undefine && value.Type != this._listType)
                    throw new SrlException(string.Format("The given type '{0}' must be of type '{1}'",
                                                         value.Type,
                                                         this._listType));
                this._value[index] = value;
                if (this._listType == SrlType.Undefine)
                    this._listType = value.Type;
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

            if (this._listType != SrlType.Undefine && tag.Type != this._listType)
                throw new SrlException(string.Format("The given type '{0}' must be of type '{1}'",
                                                     tag.Type,
                                                     this._listType));
            this._value.Insert(index, tag);
            if (this._listType == SrlType.Undefine)
                this._listType = tag.Type;
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

            if (this._listType != SrlType.Undefine && tag.Type != this._listType)
                throw new SrlException(string.Format("The given type '{0}' must be of type '{1}'",
                                                     tag.Type,
                                                     this._listType));
            this._value.Add(tag);
            if (this._listType == SrlType.Undefine)
                this._listType = tag.Type;
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
            stream.WriteType(this._listType);

            stream.WriteByte(BitConverter.GetBytes(this._value.Count));

            foreach (SrlTag var2 in this._value)
            {
                var2.GetBytes(stream);
            }
        }

        /// <summary>
        /// Set bytes for this tag
        /// </summary>
        /// <param name="stream">Stream</param>
        internal override void SetBytes(SrlStreamReader stream)
        {
            this._listType = stream.ReadType();
            int var1 = BitConverter.ToInt32(stream.ReadByte(32), 0);

            for (int var2 = 0; var2 < var1; var2++)
            {
                SrlTag var3 = SrlTag.GetInstance(this._listType);
                var3.SetBytes(stream);
                this._value.Add(var3);
            }
        }
    }
}
