using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.GetComponent<Player>() != null)
        {
            Vector2 dir = collision.transform.position - transform.position;
            dir.Normalize();
            
            //if(dir.x > 0)
            //{
            //    rb.AddForce(Vector2.right);
            //}
            //else if(dir.x < 0)
            //{
            //    rb.AddForce(Vector2.left);

            //}
            //else if(dir.y > 0)
            //{
            //    rb.AddForce(Vector2.up);

            //}
            //else if(dir.y < 0)
            //{
            //    rb.AddForce(Vector2.down);

            //}
        }
    }
}
