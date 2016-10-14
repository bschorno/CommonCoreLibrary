using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Algorithm.Path
{
    public class AStarPriorityQueue<T>
    {
        protected List<T> _innerList = new List<T>();
        protected IComparer<T> _comparer;

        /// <summary>
        /// Capacity of list
        /// </summary>
        public int Capacity
        {
            get
            {
                return this._innerList.Capacity;
            }
        }

        /// <summary>
        /// Numbers of elements in list
        /// </summary>
        public int Count
        {
            get
            {
                return this._innerList.Count;
            }
        }

        /// <summary>
        /// Element at index
        /// </summary>
        public T this[int index]
        {
            get
            {
                return this._innerList[index];
            }
            set
            {
                this._innerList[index] = value;
                this.Update(index);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AStarPriorityQueue()
        {
            this._comparer = Comparer<T>.Default;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="comparer">Comparer</param>
        public AStarPriorityQueue(IComparer<T> comparer)
        {
            this._comparer = comparer;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="comparer">Comparer</param>
        /// <param name="capacity">List capacity</param>
        public AStarPriorityQueue(IComparer<T> comparer, int capacity)
        {
            this._comparer = comparer;
            this._innerList.Capacity = capacity;
        }

        /// <summary>
        /// Switch position of two elements
        /// </summary>
        /// <param name="i">Position of element 1</param>
        /// <param name="j">Position of element 2</param>
        protected void Switch(int i, int j)
        {
            T var1 = this._innerList[i];
            this._innerList[i] = this._innerList[j];
            this._innerList[j] = var1;
        }

        /// <summary>
        /// Compare two elements
        /// </summary>
        /// <param name="i">Position of element 1</param>
        /// <param name="j">Position of element 2</param>
        /// <returns></returns>
        protected virtual int Compare(int i, int j)
        {
            return this._comparer.Compare(this._innerList[i], this._innerList[j]);
        }

        /// <summary>
        /// Push item in list
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Position of item</returns>
        public int Push(T item)
        {
            int var1 = this._innerList.Count;
            int var2;
            this._innerList.Add(item);
            while (true)
            {
                if (var1 == 0)
                    break;
                var2 = (var1 - 1) / 2;
                if (this.Compare(var1, var2) < 0)
                {
                    this.Switch(var1, var2);
                    var1 = var2;
                }
                else
                    break;
            }
            return var1;
        }

        /// <summary>
        /// Get first element and remove from list
        /// </summary>
        /// <returns>Element</returns>
        public T Pop()
        {
            T var1 = this._innerList[0];
            int var2 = 0;
            int var3;
            int var4;
            int var5;
            this._innerList[0] = this._innerList[this._innerList.Count - 1];
            this._innerList.RemoveAt(this._innerList.Count - 1);

            while (true)
            {
                var5 = var2;
                var3 = 2 * var2 + 1;
                var4 = 2 * var2 + 2;
                if (this._innerList.Count > var3 && this.Compare(var2, var3) > 0) // links kleiner
                    var2 = var3;
                if (this._innerList.Count > var4 && this.Compare(var2, var4) > 0) // rechts noch kleiner
                    var2 = var4;

                if (var2 == var5)
                    break;
                this.Switch(var2, var5);
            }
            return var1;
        }

        /// <summary>
        /// Get first element and keep in list
        /// </summary>
        /// <returns>Element</returns>
        public T Peek()
        {
            if (this._innerList.Count > 0)
                return this._innerList[0];
            return default(T);
        }

        /// <summary>
        /// Update elment at position
        /// </summary>
        /// <param name="i">Position</param>
        public void Update(int i)
        {
            int var1 = i;
            int var2;
            int var3;
            int var4;
            while (true)
            {
                if (var1 == 0)
                    break;
                var4 = (var1 - 1) / 2;
                if (this.Compare(var1, var4) < 0)
                {
                    this.Switch(var1, var4);
                    var1 = var4;
                }
                else
                    break;
            }

            if (var1 < i)
                return;

            while (true)
            {
                var2 = var1;
                var3 = 2 * var1 + 1;
                var4 = 2 * var1 + 2;
                if (this._innerList.Count > var3 && this.Compare(var1, var3) > 0) // links kleiner
                    var1 = var3;
                if (this._innerList.Count > var4 && this.Compare(var1, var4) > 0) // rechts noch kleienr
                    var1 = var4;

                if (var1 == var2)
                    break;
                this.Switch(var1, var2);
            }
        }

        /// <summary>
        /// Clear list
        /// </summary>
        public void Clear()
        {
            this._innerList.Clear();
        }
    }
}
