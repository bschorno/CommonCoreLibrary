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
                if (value == SrlType.End ||
                    value == SrlType.Undefine)
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
            if (type == SrlType.End ||
                type == SrlType.Undefine)
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

            if (this._listType != SrlType.Undefine && tag.Type != this._listType)
                throw new SrlException(string.Format("The given type '{0}' must be of type '{1}'",
                                                     tag.Type,
                                                     this._listType));
            this._value.Insert(index, tag);
            if (this._listType == SrlType.Undefine)
                this._listType = tag.Type;
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

            if (this._listType != SrlType.Undefine && tag.Type != this._listType)
                throw new SrlException(string.Format("The given type '{0}' must be of type '{1}'",
                                                     tag.Type,
                                                     this._listType));
            this._value.Add(tag);
            if (this._listType == SrlType.Undefine)
                this._listType = tag.Type;
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
