using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Algorithm
{
    public class AStarGrid
    {
        protected byte[,] _grid;

        /// <summary>
        /// Return grid as byte
        /// 0 = Solid obstacle
        /// 1 - n = Non obstacle, low -> high priority
        /// </summary>
        public byte[,] Grid
        {
            get
            {
                return this._grid;
            }
            set
            {
                this._grid = value;
            }
        }

        /// <summary>
        /// Get width of grid
        /// </summary>
        public int Width
        {
            get
            {
                return this._grid.GetLength(0);
            }
        }

        /// <summary>
        /// Get height of grid
        /// </summary>
        public int Height
        {
            get
            {
                return this._grid.GetLength(1);
            }
        }

        /// <summary>
        /// Set byte at position
        /// </summary>
        public byte this[int x, int y]
        {
            get
            {
                return this._grid[x, y];
            }
            set
            {
                this._grid[x, y] = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        protected AStarGrid()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="grid">Grid</param>
        public AStarGrid(byte[,] grid)
        {
            this._grid = grid;
        }
    }
}
