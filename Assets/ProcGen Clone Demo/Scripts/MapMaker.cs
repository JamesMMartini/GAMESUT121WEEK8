using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    // Start is called before the first frame update
    public enum SeedType { RANDOM, CUSTOM }
    public enum Direction { UP, DOWN, LEFT, RIGHT }
    [Header("Random Data")]
    public SeedType seedType = SeedType.RANDOM;

    System.Random random;
    public int seed = 0;

    [Space]
    public bool animatedPath;
    //public List<GridCell> gridCellList = new List<GridCell>();
    public Vector2 gridStart;
    public GameObject roomPrefab;
    public MazeRoom[][] rooms;
    public int roomCount = 10;
    public int mapWidth = 10;
    public int mapHeight = 10;

    [Range(1.0f, 7.0f)]
    public float cellScale = 1.0f;

    private void Awake()
    {
        SetSeed();
        CreateMaze();
    }

    void CreateMaze()
    {
        // Create the array and give each room in the array a location
        rooms = new MazeRoom[mapHeight][];
        for (int r = 0; r < rooms.Length; r++)
        {
            rooms[r] = new MazeRoom[mapWidth];
            for (int c = 0; c < rooms[r].Length; c++)
            {
                roomPrefab.transform.localScale = new Vector3(cellScale, cellScale, cellScale);
                GameObject newRoom = Instantiate(roomPrefab);
                rooms[r][c] = new MazeRoom(new Vector2(gridStart.x + (c * cellScale), gridStart.y + (r * cellScale)), newRoom);
            }
        }

        // Now that the maze is filled with game objects to represent the rooms,
        // we need to decide which rooms to set as active
        int row = 0;
        int col = 0;
        rooms[row][col].SetActive(true);
        for (int i = 0; i < roomCount; i++)
        {
            List<Direction> availableDirections = new List<Direction>();
            if (row > 0 && !rooms[row - 1][col].active)
                availableDirections.Add(Direction.DOWN);
            if (row < rooms.Length - 1 && !rooms[row + 1][col].active)
                availableDirections.Add(Direction.UP);
            if (col > 0 && !rooms[row][col - 1].active)
                availableDirections.Add(Direction.LEFT);
            if (col < rooms[row].Length - 1 && !rooms[row][col + 1].active)
                availableDirections.Add(Direction.RIGHT);

            // Now that we have the available adjecent rooms, we can choose one to make active and go to that one next
            if (availableDirections.Count > 0)
            {
                int dir = random.Next(0, availableDirections.Count);
                if (availableDirections[dir] == Direction.DOWN)
                    row--;
                else if (availableDirections[dir] == Direction.UP)
                    row++;
                else if (availableDirections[dir] == Direction.LEFT)
                    col--;
                else if (availableDirections[dir] == Direction.RIGHT)
                    col++;

                rooms[row][col].SetActive(true);
            }
        }
        
        // Now that we have the maze we need to add the doors between the rooms
        for (int r = 0; r < rooms.Length; r++)
        {
            for (int c = 0; c < rooms[r].Length; c++)
            {
                if (rooms[r][c].active)
                {
                    List<Direction> adjacentRooms = new List<Direction>();
                    if (r > 0 && rooms[r - 1][c].active)
                        adjacentRooms.Add(Direction.DOWN);
                    if (r < rooms.Length - 1 && rooms[r + 1][c].active)
                        adjacentRooms.Add(Direction.UP);
                    if (c > 0 && rooms[r][c - 1].active)
                        adjacentRooms.Add(Direction.LEFT);
                    if (c < rooms[r].Length - 1 && rooms[r][c + 1].active)
                        adjacentRooms.Add(Direction.RIGHT);

                    foreach (Direction dir in adjacentRooms)
                    {
                        rooms[r][c].room.GetComponent<RoomManager>().AddDoor(dir);
                    }
                }
            }
        }
    }

    private void Update()
    {
        
    }

    void SetSeed()
    {
        if (seedType == SeedType.RANDOM)
            random = new System.Random();
        else if (seedType == SeedType.CUSTOM)
            random = new System.Random(seed);
    }

    //IEnumerator CreatePathRoutine()
    //{
    //    gridCellList.Clear();
    //    Vector2 currentPosition = new Vector2(-15.0f, -9.0f);

    //    gridCellList.Add(new GridCell(currentPosition));

    //    for (int i = 0; i < pathLength; i++)
    //    {

    //        int n = random.Next(100);

    //        if (n > 0 && n < 49)
    //        {
    //            currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
    //        }
    //        else
    //        {
    //            currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
    //        }

    //        gridCellList.Add(new GridCell(currentPosition));
    //        yield return null;
    //    }
    //}


    //private void OnDrawGizmos()
    //{
    //    for (int i = 0; i < gridCellList.Count; i++)
    //    {
    //        Gizmos.color = Color.white;
    //        Gizmos.DrawWireCube(gridCellList[i].location, Vector2.one * cellSize);
    //        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    //        Gizmos.DrawCube(gridCellList[i].location, Vector2.one * cellSize);
    //    }
    //}
}

[System.Serializable]
public class MazeRoom
{
    public Vector2 location;
    public GameObject room;
    public bool active;

    public MazeRoom() { }

    public MazeRoom(Vector2 l, GameObject prefab)
    {
        location = new Vector2(l.x, l.y);
        room = prefab;
        active = false;

        room.GetComponent<RoomManager>().SetColor();

        room.transform.position = location;
        room.SetActive(false);
    }

    public void SetActive(bool a)
    {
        if (a)
        {
            active = true;
            room.SetActive(true);
        }
        else
        {
            active = false;
            room.SetActive(false);
        }
    }
}

[System.Serializable]
public class GridCell
{

    public Vector2 location;

    public GridCell() { }
    public GridCell(Vector2 l)
    {
        location = new Vector2(l.x, l.y);
    }
    public GridCell(float x, float y)
    {
        location = new Vector2(x, y);
    }

}