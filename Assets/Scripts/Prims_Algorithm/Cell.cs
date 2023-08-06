namespace Prims_Algorithm
{
    public class Cell
    {
        public int X, Y;
        public bool[] Walls = {true, true, true, true};
        public bool Visited = false;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
