using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Algorithm.Path
{
    internal class AStarNodeComparer : IComparer<AStarNode>
    {
        /// <summary>
        /// Compare two nodes
        /// </summary>
        /// <param name="x">First node</param>
        /// <param name="y">Second node</param>
        /// <returns>Result</returns>
        public int Compare(AStarNode x, AStarNode y)
        {
            if (x.F > y.F)
                return 1;
            else if (x.F < y.F)
                return -1;
            return 0;
        }
    }
}
