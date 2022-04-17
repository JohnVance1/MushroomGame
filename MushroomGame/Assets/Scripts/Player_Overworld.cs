using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Overworld : MonoBehaviour
{
    public static Player_Overworld instance { get; private set; }

    public float moveSpeed;
    public Vector2 playerInput;
    public GameObject projectile;
    public int currentRoom;

    public List<GameObject> playerIngredients;

    public float BerryCount;
    [SerializeField] private int healthInitial = 10;
    private int healthCurrent;
    public float bulletCooldown;
    float bulletTimer;

    public int doorSpawnIndex;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            //Destroy(this);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        BerryCount = 0;

        //Camera.main.GetComponent<CameraController>().player = this;

        DontDestroyOnLoad(gameObject);



    }

    void FixedUpdate()
    {
        transform.position += (Vector3)playerInput * Time.deltaTime * moveSpeed;
        bulletTimer -= Time.deltaTime;
    }

    public void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    
    //private void OnTriggerEnter2D(Collider2D collision)
    // {
    //    if(collision.tag == "Bullet" && bulletTimer <= 0)
    //     {
    //        healthCurrent -= 1;
    //         Debug.Log("ouch");
    //         bulletTimer = bulletCooldown;
    //     }
    //   }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
    }
}
