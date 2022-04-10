using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamBetweenRooms_Horizontal : MonoBehaviour
{
    public Transform rightRoom;
    public Transform leftRoom;
    public bool moveCam = false;
    public bool left = false;
    public bool right = false;

    public Vector3 movePos;

    public void Update()
    {
        if(moveCam)
        {
            Vector3 lerped = Vector2.Lerp(Camera.main.transform.position, movePos, 0.05f);
            lerped.z = -10;
            Camera.main.transform.position = lerped;

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

        Vector2 direction = collision.transform.position - transform.position;
        direction.Normalize();

        if (direction.x < 0)
        {
            movePos = rightRoom.position;
            moveCam = true;
            left = true;
        }
        else if (direction.x > 0)
        {
            movePos = leftRoom.position;
            moveCam = true;
            right = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() == null)
        {
            return;
        }

        Vector2 direction = collision.transform.position - transform.position;
        direction.Normalize();

        if(left && direction.x < 0)
        {
            movePos = leftRoom.position;
            moveCam = true;
        }
        else if (right && direction.x > 0)
        {
            movePos = rightRoom.position;
            moveCam = true;
        }

        left = false;
        right = false;
    }
}
