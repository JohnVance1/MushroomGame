using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamBetweenRooms_Vertical : MonoBehaviour
{
    public Transform topRoom;
    public Transform bottomRoom;
    public bool moveCam = false;
    public bool bottom = false;
    public bool top = false;

    public Vector3 movePos;

    public void Update()
    {
        if(moveCam)
        {
            // Moves the Camera
            Vector3 lerped = Vector2.Lerp(Camera.main.transform.position, movePos, 0.05f);
            lerped.z = -10;
            Camera.main.transform.position = lerped;

            // if the camera is really close to the move position just set it to that position
            if (Vector2.Distance(Camera.main.transform.position, movePos) <= 0.1)
            {
                Camera.main.transform.position = movePos;
                moveCam = false;
            }
        }
    }

    /// <summary>
    /// The main trigger for the camera moving between rooms
    /// </summary>
    /// <param name="collision">Mainly the Player</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() == null)
        {
            return;
        }

        // Gets the direction between the Collider and the Player
        Vector2 direction = collision.transform.position - transform.position;
        direction.Normalize();

        // If the Player is below the collider
        if (direction.y < 0)
        {
            movePos = topRoom.position;
            moveCam = true;
            bottom = true;
        }
        // If the Player is above the collider
        else if (direction.y > 0)
        {
            movePos = bottomRoom.position;
            moveCam = true;
            top = true;
        }

    }

    /// <summary>
    /// Only matters if the Player goes back the way they came before exiting the collider
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() == null)
        {
            return;
        }

        // Gets the direction again
        Vector2 direction = collision.transform.position - transform.position;
        direction.Normalize();

        // If the Player entered from the bottom and they exit from the bottom
        if(bottom && direction.y < 0)
        {
            movePos = bottomRoom.position;
            moveCam = true;
        }
        // If the Player entered from the top and they exit from the top
        else if (top && direction.y > 0)
        {
            movePos = topRoom.position;
            moveCam = true;
        }

        bottom = false;
        top = false;
    }
}
