using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; } 

    public float moveSpeed;
    public Vector2 playerInput;
    public GameObject projectile;
    public int currentRoom;

    public List<GameObject> playerIngredients;

    public List<Ingredients> overworldIngredients;

    public float BerryCount;
    [SerializeField] private int healthInitial = 10;
    private int healthCurrent;
    public float bulletCooldown;
    float bulletTimer;

    public int doorSpawnIndex;
    public bool inOverworld;

    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }
    private void Awake()
    {
        playerIngredients = new List<GameObject>();
        //overworldIngredients = new List<Ingredients>();
        inOverworld = false;

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

        if (inOverworld)
        {
            Camera.main.GetComponent<CameraController>().player = this;
        }
        DontDestroyOnLoad(gameObject);

        

    }

    void FixedUpdate()
    {
        if (dialogueUI.IsOpen) return;
        transform.position += (Vector3)playerInput * Time.deltaTime * moveSpeed;
        bulletTimer -= Time.deltaTime;

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (Interactable != null)
            {
                Interactable.Interact(this);
            }
        }

    }

   

    /// <summary>
    /// Called when the Player collides with the ingredient
    /// </summary>
    /// <param name="ingBase"></param>
    public void PickedUp(PickUpBase ingBase)
    {
        GameObject shoot = SceneManager.instance.SwitchIngredientType(ingBase);
        playerIngredients.Add(shoot);
    }

    /// <summary>
    /// The method thats called when the Move action is called
    /// </summary>
    /// <param name="value"></param>
    public void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    /// <summary>
    /// The method that is called when the Fire action is called
    /// </summary>
    /// <param name="value"></param>
    public void OnShoot(InputValue value)
    {
        if (playerIngredients.Count > 0)
        {
            GameObject obj = playerIngredients[playerIngredients.Count - 1];
            Instantiate(obj, transform.position, Quaternion.identity);
            playerIngredients.RemoveAt(playerIngredients.Count - 1);
        }
    }


    /// <summary>
    /// Resets the Player's current health
    /// </summary>
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

    public void OverworldPickup(Ingredients type)
    {
        overworldIngredients.Add(type);

    }

    /// <summary>
    /// Has the Player take a certain amount of damage
    /// </summary>
    /// <param name="damageAmount"> The amount of damage for the Player to take </param>
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

    /// <summary>
    /// Heals the Player by the amount specified
    /// </summary>
    /// <param name="healamount">The amount to be healed</param>
    public void HealPlayer(int healamount)
    {
        healthCurrent += healamount;

        if(healthCurrent > healthInitial)
        {
            ResetHealth();
        }
    }

    

}
