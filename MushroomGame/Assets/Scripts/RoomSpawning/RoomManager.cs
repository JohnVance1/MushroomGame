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

    private void Start()
    {
        grid = new Grid(width, height);
        stack = new Stack<Node>();
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
            previousNode = stack.Peek();

            // Check the current nodes' rooms
            currentNode = grid.CheckNeighbors(previousNode);

            // if there isnt a neighbor return            
            if(currentNode != null)
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

        }    

    }

    public GameObject GetSpecificRoom(Node current, GameObject[] roomList)
    {
        List<Directions> dirs = GetRequiredDirections(current);
        List<GameObject> spawnableRooms = new List<GameObject>();

        foreach(GameObject room in roomList)
        {
            int num = 0;
            foreach (Directions dir in dirs)
            {
                if (room.GetComponent<Room>().directions.Contains(dir))
                {
                    num++;
                }
            }

            if(num == dirs.Count)
            {
                spawnableRooms.Add(room);
            }

        }

        return spawnableRooms[Random.Range(0, spawnableRooms.Count)];


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
            SpawnDownRoom(previous.room, current);
        }
        else if (current.y == previous.y - 1)
        {
            SpawnUpRoom(previous.room, current);
        }
        else if (current.x == previous.x + 1)
        {
            SpawnLeftRoom(previous.room, current);
        }
        else if (current.x == previous.x - 1)
        {
            SpawnRightRoom(previous.room, current);
        }

        //previous.visited = false;

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
        //spawnRoom.neighbors = grid.CheckNeighbors(spawnRoom.x, spawnRoom.y);
        //queue.Enqueue(spawnRoom);

    }


    public int RandomRoom(int length)
    {
        return Random.Range(0, length);
    }



}
