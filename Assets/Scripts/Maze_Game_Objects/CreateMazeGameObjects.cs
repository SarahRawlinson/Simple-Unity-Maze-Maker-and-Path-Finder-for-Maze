using A_Star;
using Prims_Algorithm;
using UnityEngine;


namespace Maze_Game_Objects
{
    public class CreateMazeGameObjects : MonoBehaviour
    {
        [SerializeField] private int mazeWidth = 10;
        [SerializeField] private int mazeHeight = 10;
        [SerializeField] private GameObject wall;
        [SerializeField] private GameObject floor;
        [SerializeField] private float cellSize = 5f;
        [SerializeField] private float wallHeight = 2f;
        [SerializeField] private float wallThickness = .5f;
        [SerializeField] private float floorThickness = .5f;
        [SerializeField] Color wallColour = Color.red;
        [SerializeField] Color floorColour = Color.blue;
        [SerializeField] Color startColour = Color.yellow;
        [SerializeField] Color endColour = Color.magenta;
        [SerializeField] Color pathColour = Color.green;
        private MazeGenerator _mazeGenerator;

        private void Awake()
        {
            _mazeGenerator = new MazeGenerator(mazeWidth, mazeHeight);
            PlaceFloor();
            PlaceWalls();
        }

        private void PlaceFloor()
        {
            AStar aStar = new AStar(_mazeGenerator);
            var path = aStar.FindPath(_mazeGenerator.GetStart(), _mazeGenerator.GetEnd());
            for (int x = 0; x < mazeWidth; x++)
            {
                for (int y = 0; y < mazeHeight; y++)
                {
                    Vector3 pos = new Vector3(x * cellSize, 0, y * cellSize);
                    GameObject obj = Instantiate(floor, pos, Quaternion.Euler(90, 0, 0));
                    obj.transform.localScale = new Vector3(cellSize, cellSize, floorThickness);
                    if (_mazeGenerator.GetStart().X == x && _mazeGenerator.GetStart().Y == y)
                    {
                        obj.GetComponent<Renderer>().material.color = startColour;
                    }
                    else if (_mazeGenerator.GetEnd().X == x && _mazeGenerator.GetEnd().Y == y)
                    {
                        obj.GetComponent<Renderer>().material.color = endColour;
                    }
                    else
                    {
                        foreach (Cell cell in path)
                        {
                            if (cell.X == x && cell.Y == y)
                            {
                                obj.GetComponent<Renderer>().material.color = pathColour;
                                break;
                            }
                            else
                            {
                                obj.GetComponent<Renderer>().material.color = floorColour;
                            }
                        }
                    }
                }
            }
        }

        private void PlaceWalls()
        {
            Cell[,] cells = _mazeGenerator.GetMaze();
            for (int x = 0; x < mazeWidth; x++)
            {
                for (int y = 0; y < mazeHeight; y++)
                {
                    Cell cell = cells[x, y];
                    Vector3 pos;
                    if (cell.Walls[0])
                    {
                        pos = new Vector3(x * cellSize, wallHeight / 2, (y + .5f) * cellSize);
                        GameObject obj = Instantiate(wall, pos, Quaternion.identity);
                        obj.transform.localScale = new Vector3(cellSize, wallHeight, wallThickness);
                        obj.GetComponent<Renderer>().material.color = wallColour;
                    }
                    if (cell.Walls[1])
                    {
                        pos = new Vector3((x  + .5f) * cellSize, wallHeight / 2, y * cellSize);
                        GameObject obj = Instantiate(wall, pos, Quaternion.Euler(0, 90, 0)); 
                        obj.transform.localScale = new Vector3(cellSize, wallHeight, wallThickness);
                        obj.GetComponent<Renderer>().material.color = wallColour;
                    }
                    if (cell.Walls[2])
                    {
                        pos = new Vector3(x * cellSize, wallHeight / 2, (y - .5f) * cellSize);
                        GameObject obj = Instantiate(wall, pos, Quaternion.identity);
                        obj.transform.localScale = new Vector3(cellSize, wallHeight, wallThickness);
                        obj.GetComponent<Renderer>().material.color = wallColour;
                    }
                    if (cell.Walls[3])
                    {
                        pos = new Vector3((x  - .5f) * cellSize, wallHeight / 2, y * cellSize);
                        GameObject obj = Instantiate(wall, pos, Quaternion.Euler(0, 90, 0));
                        obj.transform.localScale = new Vector3(cellSize, wallHeight, wallThickness);
                        obj.GetComponent<Renderer>().material.color = wallColour;
                    }
                }
            }
        }
    }
}