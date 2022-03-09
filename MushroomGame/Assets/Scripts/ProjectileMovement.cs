using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.position.normalized * Time.deltaTime * moveSpeed;

        if (transform.position.magnitude <= 0.2)
        {
            Destroy(gameObject);

        }
    }
}
