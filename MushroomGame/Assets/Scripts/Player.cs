using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private Player instance;
    public Vector2 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    //void FixedUpdate()
    //{

    //    transform.position += (Vector3)playerInput * Time.deltaTime * moveSpeed;
    //}

    private void Update()
    {
        transform.position += (Vector3)playerInput * Time.deltaTime * moveSpeed;

    }

    public void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();



    }
}
