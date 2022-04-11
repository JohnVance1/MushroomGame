using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularRoom : MonoBehaviour
{
    public GameObject camLocation;
    public int roomNumber;
    public BoxCollider2D roomBounds;

    private void Start()
    {
        roomBounds = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (Player.instance.currentRoom != roomNumber)
            {
                Player.instance.currentRoom = roomNumber;
            }
        }
    }

}
