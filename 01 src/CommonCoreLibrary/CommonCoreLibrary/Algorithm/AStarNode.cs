using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Algorithm
{
    public sealed class AStarNode
    {
        private int _x;
        private int _y;
        private int _f;
        private int _g;
        private int _h;
        private AStarNode _parent;

        /// <summary>
        /// X-Coordinate
        /// </summary>
        public int X
        {
            get
            {
                return this._x;
            }
            internal set
            {
                this._x = value;
            }
        }

        /// <summary>
        /// Y-Coordinate
        /// </summary>
        public int Y
        {
            get
            {
                return this._y;
            }
            internal set
            {
                this._y = value;
            }
        }

        /// <summary>
        /// F-Value
        /// </summary>
        public int F
        {
            get
            {
                return this._f;
            }
            internal set
            {
                this._f = value;
            }
        }

        /// <summary>
        /// G-Value
        /// </summary>
        public int G
        {
            get
            {
                return this._g;
            }
            internal set
            {
                this._g = value;
            }
        }

        /// <summary>
        /// H-Value
        /// </summary>
        public int H
        {
            get
            {
                return this._h;
            }
            internal set
            {
                this._h = value;
            }
        }

        /// <summary>
        /// Parent node
        /// </summary>
        public AStarNode Parent
        {
            get
            {
                return this._parent;
            }
            internal set
            {
                this._parent = value;
            }
        }
    }
}
