using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Algorithm
{
    public class AStarPath
    {
        private PrioriyQueueBH<AStarNode> _openList = new PrioriyQueueBH<AStarNode>(new AStarNodeComparer());
        private List<AStarNode>           _closeList = new List<AStarNode>();
        private AStarGrid                 _grid;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="grid">Grid</param>
        public AStarPath(AStarGrid grid)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");
            this._grid = grid;
        }

        /// <summary>
        /// Find path
        /// </summary>
        /// <param name="startX">Start-Coordinate X</param>
        /// <param name="startY">Start-Coordinate Y</param>
        /// <param name="endX">Target-Coordinate X</param>
        /// <param name="endY">Target-Coordinate Y</param>
        /// <returns></returns>
        public List<AStarNode> FindPath(int startX, int startY, int endX, int endY)
        {
            AStarNode startNode = new AStarNode();
            startNode.G = 0;
            startNode.H = 0;
            startNode.F = 0;
            startNode.X = startX;
            startNode.Y = startY;

            this._openList.Clear();
            this._closeList.Clear();

            sbyte[,] direction = new sbyte[4, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };

            while (this._openList.Count > 0)
            {
                AStarNode currentNode = this._openList.Pop();

                if (currentNode.X == endX && currentNode.Y == endY)
                {
                    this._closeList.Add(currentNode);
                    return this._closeList;
                }

                this._closeList.Add(currentNode);

                for (int i = 0; i < 4; i++)
                {
                    AStarNode successorNode = new AStarNode();
                    successorNode.X = currentNode.X + direction[i, 0];
                    successorNode.Y = currentNode.Y + direction[i, 1];

                    if (successorNode.X < 0 || successorNode.Y < 0 ||
                        successorNode.X >= this._grid.Width ||
                        successorNode.Y >= this._grid.Height)
                        continue;

                    if (this._grid[successorNode.X, successorNode.Y] == 0)
                        continue; //Wall

                    int g = currentNode.G + this._grid[successorNode.X, successorNode.Y];

                    int openIndex = -1;
                    for (int j = 0; j < this._openList.Count; j++)
                    {
                        if (this._openList[j].X == successorNode.X &&
                            this._openList[j].Y == successorNode.Y)
                        {
                            openIndex = j;
                            break;
                        }
                    }
                    if (openIndex != -1 && this._openList[openIndex].G <= g)
                        continue;


                    int closeIndex = -1;
                    for (int j = 0; j < this._closeList.Count; j++)
                    {
                        if (this._closeList[j].X == successorNode.X &&
                            this._closeList[j].Y == successorNode.Y)
                        {
                            closeIndex = j;
                            break;
                        }
                    }
                    if (closeIndex != -1 && this._closeList[closeIndex].G <= g)
                        continue;

                    successorNode.Parent = currentNode;
                    successorNode.G = g;
                    successorNode.F = g + successorNode.H;
                    this._openList.Push(successorNode);

                }
            }

            return new List<AStarNode>();
        }
    }
}
