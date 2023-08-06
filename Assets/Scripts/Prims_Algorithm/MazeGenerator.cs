using System;
using System.Collections.Generic;

namespace Prims_Algorithm
{
    public class MazeGenerator
    {
        private int sizeX;
        private int sizeY;
        private Cell[,] mazeGrid;
        private List<Cell> _cells = new List<Cell>();
        private Random _random = new Random();
        private Cell start;
        private Cell end;
        public MazeGenerator(int x, int y)
        {
            sizeX = x;
            sizeY = y;
            InitiateGrid();
            MakeMaze();
        }


        private void InitiateGrid()
        {
            mazeGrid = new Cell[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    Cell newCell = new Cell(x, y);
                    mazeGrid[x, y] = newCell;
                    _cells.Add(newCell);
                }
            }
        }
        
        private void MakeMaze()
        {
            // random was too short at times
            start = _cells[_random.Next(0, _cells.Count)];
            // end = _cells[_random.Next(0, _cells.Count)];
            // while(end == start)
            // {
            //     end = _cells[_random.Next(0, _cells.Count)];
            // }
            
            List<Cell> visistedCells = new List<Cell>();
            start.Visited = true;
            visistedCells.Add(start);
            while (visistedCells.Count > 0)
            {
                Cell current = visistedCells[_random.Next(0, visistedCells.Count)];
                List<Cell> unvisitedNeighbors = GetNotInvestigatedNeighbors(current);
                if (unvisitedNeighbors.Count > 0)
                {
                    Cell chosenCell = unvisitedNeighbors[_random.Next(0, unvisitedNeighbors.Count)];
                    RemoveWallBetween(current, chosenCell);
                    chosenCell.Visited = true;
                    visistedCells.Add(chosenCell);
                }
                else
                {
                    visistedCells.Remove(current);
                }
            }
            // fixed start to the top left and end to the bottom right
            start = mazeGrid[0, 0];
            end = mazeGrid[sizeX - 1, sizeY - 1];
        }

        List<Cell> GetNotInvestigatedNeighbors(Cell cell)
        {
            List<Cell> neighbors = new List<Cell>();
            
            if (cell.Y + 1 < sizeY && !mazeGrid[cell.X, cell.Y + 1].Visited) neighbors.Add(mazeGrid[cell.X, cell.Y + 1]); // Top
            if (cell.X + 1 < sizeX && !mazeGrid[cell.X + 1, cell.Y].Visited) neighbors.Add(mazeGrid[cell.X + 1, cell.Y]); // Right
            if (cell.Y - 1 >= 0 && !mazeGrid[cell.X, cell.Y - 1].Visited) neighbors.Add(mazeGrid[cell.X, cell.Y - 1]); // Bottom
            if (cell.X - 1 >= 0 && !mazeGrid[cell.X - 1, cell.Y].Visited) neighbors.Add(mazeGrid[cell.X - 1, cell.Y]); // Left
            
            return neighbors;
        }

        public List<Cell> GetMovableNeighbors(Cell cell)
        {
            List<Cell> neighbors = new List<Cell>();

            // Check Top
            if (cell.Y + 1 < sizeY && !cell.Walls[0] && !mazeGrid[cell.X, cell.Y + 1].Walls[2])
                neighbors.Add(mazeGrid[cell.X, cell.Y + 1]);

            // Check Right
            if (cell.X + 1 < sizeX && !cell.Walls[1] && !mazeGrid[cell.X + 1, cell.Y].Walls[3])
                neighbors.Add(mazeGrid[cell.X + 1, cell.Y]);

            // Check Bottom
            if (cell.Y - 1 >= 0 && !cell.Walls[2] && !mazeGrid[cell.X, cell.Y - 1].Walls[0])
                neighbors.Add(mazeGrid[cell.X, cell.Y - 1]);

            // Check Left
            if (cell.X - 1 >= 0 && !cell.Walls[3] && !mazeGrid[cell.X - 1, cell.Y].Walls[1])
                neighbors.Add(mazeGrid[cell.X - 1, cell.Y]);

            return neighbors;
        }


        void RemoveWallBetween(Cell current, Cell neighbor)
        {
            if (neighbor.X > current.X)
            {
                current.Walls[1] = false;
                neighbor.Walls[3] = false;
            }
            else if (neighbor.X < current.X)
            {
                current.Walls[3] = false;
                neighbor.Walls[1] = false;
            }
            else if (neighbor.Y > current.Y)
            {
                current.Walls[0] = false;
                neighbor.Walls[2] = false;
            }
            else if (neighbor.Y < current.Y)
            {
                current.Walls[2] = false;
                neighbor.Walls[0] = false;
            }
        }

        public Cell[,] GetMaze()
        {
            return mazeGrid;
        }

        public Cell GetStart()
        {
            return start;
        }
        
        public Cell GetEnd()
        {
            return end;
        }
    }
}