using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public int width;
    public int height;

    public Node[,] grid;

    public Grid(int startWidth, int startHight)
    {
        width = startWidth;
        height = startHight;
        grid = new Node[width, height];
        PopulateGrid();

    }       

    public void PopulateGrid()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                grid[i, j] = new Node(i, j);
            }
        }
    }

    public Node CheckNeighbors(Node current)
    {
        int x = current.x;
        int y = current.y;
        List<Node> directions = new List<Node>();

        Node up = grid[x, y + 1];
        Node down = grid[x, y - 1];
        Node right = grid[x + 1, y];
        Node left = grid[x - 1, y];


        foreach (Directions dir in current.room.directions)
        {
            if(dir == Directions.UP && !up.visited)
            {
                directions.Add(up);
            }
            else if (dir == Directions.DOWN && !down.visited)
            {
                directions.Add(down);
            }
            else if (dir == Directions.RIGHT && !right.visited)
            {
                directions.Add(right);
            }
            else if (dir == Directions.LEFT && !left.visited)
            {
                directions.Add(left);
            }
        }

        if(directions.Count == 0)
        {
            return null;
        }

        Node randNode = directions[Random.Range(0, directions.Count)];

        return randNode;




    }

}
