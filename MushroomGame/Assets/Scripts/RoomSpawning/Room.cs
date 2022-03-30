using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    UP = 1,
    RIGHT = 2,
    DOWN = 4,
    LEFT = 8

}


public class Room : MonoBehaviour
{
    public float height;
    public float width;
    public bool visited;
    public int x;
    public int y;
    public int dirCount;

    public List<Node> neighbors = new List<Node>();

    public List<Directions> directions = new List<Directions>();
    public List<Directions> necessaryDir = new List<Directions>();

    public GameObject downSpawnPoint;
    public GameObject upSpawnPoint;
    public GameObject rightSpawnPoint;
    public GameObject leftSpawnPoint;

    public Room(int _x, int _y)
    {
        x = _x;
        y = _y;
        visited = false;

    }



    public void Start()
    {

        
    }

    public void Update()
    {
        
    }

    public void DirectionCount()
    {
        foreach(Directions dir in directions)
        {
            dirCount += (int)dir;
        }

    }


}
