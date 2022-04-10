using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] upOpenings;
    public GameObject[] rightOpenings;
    public GameObject[] downOpenings;
    public GameObject[] leftOpenings;

    private Grid grid;
    public int width;
    public int height;

    private Stack<Node> stack;
    public Room startingRoom;

    private float timer;
    private float timerMax;

    public List<GameObject> spawnedRooms;


    private void Start()
    {
        grid = new Grid(width, height);
        stack = new Stack<Node>();
        spawnedRooms = new List<GameObject>();
        timer = 0f;
        timerMax = 2f;
        SpawnRooms();
    }

    public void ClearGrid()
    {
        foreach(GameObject room in spawnedRooms)
        {
            spawnedRooms.Remove(room);
            Destroy(room);
        }

        grid = new Grid(width, height);
        stack = new Stack<Node>();
        spawnedRooms = new List<GameObject>();

    }

    public void OnGenerate()
    {
        ClearGrid();
        SpawnRooms();
    }


    public void SpawnRooms()
    {
        //stack.Push(grid.grid[grid.width / 2, grid.height / 2]);
        grid.grid[grid.width / 2, grid.height / 2].room = startingRoom;
        startingRoom.x = grid.width / 2;
        startingRoom.y = grid.height / 2;
        startingRoom.visited = true;
        Node previousNode = grid.grid[grid.width / 2, grid.height / 2];
        previousNode.visited = true;
        stack.Push(previousNode);

        Node currentNode = null;

        while (stack.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer >= timerMax)
            {
                previousNode = stack.Peek();

                // Check the current nodes' rooms
                currentNode = grid.CheckNeighbors(previousNode);

                // if there isnt a neighbor return            
                if (currentNode != null)
                {
                    // if there is a neighbor then spawn a new room, set it to visited, and push it to the stack
                    CheckNeighborLocation(currentNode, previousNode);
                    currentNode.visited = true;
                    stack.Push(currentNode);
                    //previousNode = currentNode;
                }
                else
                {
                    stack.Pop();
                }
                timer = 0f;
            }
        }    

    }

    public int DirectionsToBit(List<Directions> directions)
    {
        int bit = 0;

        foreach(Directions dir in directions)
        {
            bit += (int)dir;
        }

        return bit;
    }

    public GameObject GetSpecificRoom(Node current, GameObject[] roomList)
    {
        List<Directions> dirs = GetRequiredDirections(current);
        List<Directions> walls = GetWallDirections(current);

        int dirsBit = DirectionsToBit(dirs);
        int wallsBit = DirectionsToBit(walls);

        if(wallsBit > 0)
        {
            Debug.Log(0);
        }

        List<GameObject> spawnableRooms = new List<GameObject>();

        foreach(GameObject room in roomList)
        {
            room.GetComponent<Room>().DirectionCount();
            int roomBit = room.GetComponent<Room>().dirCount;

            if(((roomBit & dirsBit) == dirsBit) &&
                (((roomBit & wallsBit) != wallsBit) ||
                wallsBit == 0))
            {
                spawnableRooms.Add(room);
            }



            //int num = 0;

            //foreach (Directions dir in dirs)
            //{
            //    if (room.GetComponent<Room>().directions.Contains(dir))
            //    {
            //        num++;
            //    }
                
            //}

            //foreach (Directions wall in walls)
            //{
            //    if (room.GetComponent<Room>().directions.Contains(wall))
            //    {
            //        num--;
            //    }

            //}

            //if (num == dirs.Count)
            //{
            //    spawnableRooms.Add(room);
            //}
        }

        return spawnableRooms[Random.Range(0, spawnableRooms.Count)];


    }
    public List<Directions> GetWallDirections(Node current)
    {
        int x = current.x;
        int y = current.y;
        List<Directions> directions = new List<Directions>();

        Node up = grid.grid[x, y + 1];
        Node down = grid.grid[x, y - 1];
        Node right = grid.grid[x + 1, y];
        Node left = grid.grid[x - 1, y];

        if (up.visited)
        {
            if (!up.room.directions.Contains(Directions.DOWN))
            {
                directions.Add(Directions.UP);
            }
        }
        if (down.visited)
        {
            if (!down.room.directions.Contains(Directions.UP))
            {
                directions.Add(Directions.DOWN);
            }
        }
        if (right.visited)
        {
            if (!right.room.directions.Contains(Directions.LEFT))
            {
                directions.Add(Directions.RIGHT);
            }
        }
        if (left.visited)
        {
            if (!left.room.directions.Contains(Directions.RIGHT))
            {
                directions.Add(Directions.LEFT);
            }
        }

        if (directions.Count > 1)
        {
            Debug.Log(directions);
        }

        return directions;
    }


    public List<Directions> GetRequiredDirections(Node current)
    {
        int x = current.x;
        int y = current.y;
        List<Directions> directions = new List<Directions>();

        Node up = grid.grid[x, y + 1];
        Node down = grid.grid[x, y - 1];
        Node right = grid.grid[x + 1, y];
        Node left = grid.grid[x - 1, y];

        if(up.visited)
        {
            if (up.room.directions.Contains(Directions.DOWN))
            {
                directions.Add(Directions.UP);
            }
        }
        if (down.visited)
        {
            if (down.room.directions.Contains(Directions.UP))
            {
                directions.Add(Directions.DOWN);
            }
        }
        if (right.visited)
        {
            if (right.room.directions.Contains(Directions.LEFT))
            {
                directions.Add(Directions.RIGHT);
            }
        }
        if (left.visited)
        {
            if (left.room.directions.Contains(Directions.RIGHT))
            {
                directions.Add(Directions.LEFT);
            }
        }

        if(directions.Count > 1)
        {
            Debug.Log(directions);
        }

        return directions;
    }

    public void CheckNeighborLocation(Node current, Node previous)
    {
        if (current.y == previous.y + 1)
        {
            //SpawnDownRoom(previous.room, current);
            SpawnAllRoom(previous.room, current, downOpenings, previous.room.upSpawnPoint, Directions.DOWN, 0, 1);
        }
        else if (current.y == previous.y - 1)
        {
            //SpawnUpRoom(previous.room, current);
            SpawnAllRoom(previous.room, current, upOpenings, previous.room.downSpawnPoint, Directions.UP, 0, -1);
        }
        else if (current.x == previous.x + 1)
        {
            //SpawnLeftRoom(previous.room, current);
            SpawnAllRoom(previous.room, current, leftOpenings, previous.room.rightSpawnPoint, Directions.LEFT, 1, 0);
        }
        else if (current.x == previous.x - 1)
        {
            //SpawnRightRoom(previous.room, current);
            SpawnAllRoom(previous.room, current, rightOpenings, previous.room.leftSpawnPoint, Directions.RIGHT, -1, 0);
        }

        //previous.visited = false;

    }

    public void SpawnAllRoom(Room previous, Node current, GameObject[] roomList, GameObject spawnPoint, Directions toRemove, int x, int y)
    {
        int rand = Random.Range(0, roomList.Length);

        GameObject spawnee = Instantiate(GetSpecificRoom(current, roomList), spawnPoint.transform.localPosition + previous.transform.position, Quaternion.identity);

        Room spawnRoom = spawnee.GetComponent<Room>();
        spawnRoom.directions.Remove(toRemove);
        //current.directions.Remove(Directions.DOWN);

        spawnRoom.y = previous.y + y;
        spawnRoom.x = previous.x + x;
        grid.grid[spawnRoom.x, spawnRoom.y].room = spawnRoom;
        //spawnRoom.DirectionCount();
        spawnedRooms.Add(spawnee);
        //spawnRoom.neighbors = grid.CheckNeighbors(spawnRoom.x, spawnRoom.y);
        //queue.Enqueue(spawnRoom);

    }


    public void SpawnUpRoom(Room previous, Node current)
    {
        int rand = Random.Range(0, upOpenings.Length);

        GameObject spawnee = Instantiate(GetSpecificRoom(current, upOpenings), previous.downSpawnPoint.transform.localPosition + previous.transform.position, Quaternion.identity);
        
        Room spawnRoom = spawnee.GetComponent<Room>();
        spawnRoom.directions.Remove(Directions.UP);
        //current.directions.Remove(Directions.DOWN);

        spawnRoom.y = previous.y - 1;
        spawnRoom.x = previous.x;
        grid.grid[spawnRoom.x, spawnRoom.y].room = spawnRoom;
        spawnRoom.DirectionCount();

        //spawnRoom.neighbors = grid.CheckNeighbors(spawnRoom.x, spawnRoom.y);
        //queue.Enqueue(spawnRoom);


    }

    public void SpawnRightRoom(Room previous, Node current)
    {
        int rand = Random.Range(0, rightOpenings.Length);
        GameObject spawnee = Instantiate(GetSpecificRoom(current, rightOpenings), previous.leftSpawnPoint.transform.localPosition + previous.transform.position, Quaternion.identity);
        
        Room spawnRoom = spawnee.GetComponent<Room>();
        spawnRoom.directions.Remove(Directions.RIGHT);
        //current.directions.Remove(Directions.LEFT);

        spawnRoom.y = previous.y;
        spawnRoom.x = previous.x - 1;
        grid.grid[spawnRoom.x, spawnRoom.y].room = spawnRoom;
        spawnRoom.DirectionCount();
        //spawnRoom.neighbors = grid.CheckNeighbors(spawnRoom.x, spawnRoom.y);
        //queue.Enqueue(spawnRoom);


    }

    public void SpawnDownRoom(Room previous, Node current)
    {
        int rand = Random.Range(0, downOpenings.Length);
        GameObject spawnee = Instantiate(GetSpecificRoom(current, downOpenings), previous.upSpawnPoint.transform.localPosition + previous.transform.position, Quaternion.identity);
        Room spawnRoom = spawnee.GetComponent<Room>();
        spawnRoom.directions.Remove(Directions.DOWN);
        //current.directions.Remove(Directions.UP);

        spawnRoom.y = previous.y + 1;
        spawnRoom.x = previous.x;
        grid.grid[spawnRoom.x, spawnRoom.y].room = spawnRoom;
        spawnRoom.DirectionCount();

        //spawnRoom.neighbors = grid.CheckNeighbors(spawnRoom.x, spawnRoom.y);
        //queue.Enqueue(spawnRoom);


    }

    public void SpawnLeftRoom(Room previous, Node current)
    {
        int rand = Random.Range(0, leftOpenings.Length);
        GameObject spawnee = Instantiate(GetSpecificRoom(current, leftOpenings), previous.rightSpawnPoint.transform.localPosition + previous.transform.position, Quaternion.identity);

        Room spawnRoom = spawnee.GetComponent<Room>();
        spawnRoom.directions.Remove(Directions.LEFT);
        //current.directions.Remove(Directions.RIGHT);

        spawnRoom.y = previous.y;
        spawnRoom.x = previous.x + 1;
        grid.grid[spawnRoom.x, spawnRoom.y].room = spawnRoom;
        spawnRoom.DirectionCount();

        //spawnRoom.neighbors = grid.CheckNeighbors(spawnRoom.x, spawnRoom.y);
        //queue.Enqueue(spawnRoom);

    }


    public int RandomRoom(int length)
    {
        return Random.Range(0, length);
    }



}
