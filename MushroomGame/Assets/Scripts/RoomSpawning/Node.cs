using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool visited;
    public int x;
    public int y;
    public Room room;

    public Node(int posX, int posY)
    {
        x = posX;
        y = posY;
        visited = false;
        room = null;
        
    }
}
