using System;
using System.Collections.Generic;
using System.Linq;
using Prims_Algorithm;

namespace A_Star
{
    class AStar
    {
        private MazeGenerator maze;
        public AStar(MazeGenerator maze)
        {
            this.maze = maze;
        }

        public List<Cell> FindPath(Cell start, Cell goal)
        {
            HashSet<Cell> closedSet = new HashSet<Cell>();
            HashSet<Cell> openSet = new HashSet<Cell>() { start };

            Dictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();

            Dictionary<Cell, float> gScore = new Dictionary<Cell, float>();
            gScore[start] = 0;

            Dictionary<Cell, float> fScore = new Dictionary<Cell, float>();
            fScore[start] = HeuristicCostEstimate(start, goal);

            while (openSet.Count > 0)
            {
                Cell current = openSet.OrderBy(n => fScore[n]).First();

                if (current == goal)
                    return ReconstructPath(cameFrom, current);

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in maze.GetMovableNeighbors(current))
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    float tentative_gScore = gScore[current] + 1;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                    else if (tentative_gScore >= gScore[neighbor])
                        continue;

                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative_gScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, goal);
                }
            }
            return null;
        }

        private float HeuristicCostEstimate(Cell a, Cell b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        private List<Cell> ReconstructPath(Dictionary<Cell, Cell> cameFrom, Cell current)
        {
            List<Cell> path = new List<Cell> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(current);
            }
            path.Reverse();
            return path;
        }
    }

}