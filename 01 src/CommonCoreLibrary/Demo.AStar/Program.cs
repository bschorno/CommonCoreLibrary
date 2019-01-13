using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonCoreLibrary.Algorithm.Path;

namespace Demo.AStar
{
    class Program
    {
        static void Main(string[] args)
        {
            int startX = 0;
            int startY = 0;
            int endX = 5;
            int endY = 0;

            byte[,] grid = new byte[,]
            {
                { 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 0, 1, 0 },
                { 1, 1, 1, 0, 1, 1 },
                { 1, 0, 0, 0, 0, 1 },
                { 1, 0, 1, 1, 1, 1 },
                { 1, 1, 1, 0, 0, 1 }
            };

            AStarPath path = new AStarPath(new AStarGrid(grid));
            List<AStarNode> node = path.FindPath(startX, startY, endX, endY);

            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    if (grid[j, i] == 0)
                        Console.Write(" X");
                    else
                        Console.Write("  ");
                }
                Console.WriteLine();
            }

            foreach (AStarNode n in node)
            {
                Console.SetCursorPosition(n.X * 2, n.Y);
                Console.Write(" .");
            }

            Console.ReadLine();
        }
    }
}
