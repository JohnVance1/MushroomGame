using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public static Player instance;
    public Vector2 playerInput;
    public GameObject projectile;
    public float BerryCount;
    [SerializeField] private int healthInitial = 10;
    private int healthCurrent;
    public float bulletCooldown;
    float bulletTimer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        BerryCount = 0;

        

    }

    void FixedUpdate()
    {
        transform.position += (Vector3)playerInput * Time.deltaTime * moveSpeed;
        bulletTimer -= Time.deltaTime;
    }

    //private void Update()
    //{
    //    transform.position += (Vector3)playerInput * Time.deltaTime * moveSpeed;

    //}

    public void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    public void OnShoot(InputValue value)
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
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

    public void ResetHealth()
    {
        healthCurrent = healthInitial; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       //healthCurrent -= damageAmount;
        if (collision.tag == "Bullet" && bulletTimer <= 0)
        {
            healthInitial -= 1;
            print(healthInitial);
            bulletTimer = bulletCooldown;
        }
    }


}
