using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; } 

    public float moveSpeed;
    public Vector2 playerInput;
    public GameObject projectile;
    public int currentRoom;
    public Sprite hatSprite;
    private bool canDamage;

    public List<GameObject> playerIngredients;

    public List<Ingredients> overworldIngredients;

    public float BerryCount;
    [SerializeField] private int healthInitial = 20;
    private int healthCurrent;
    public float bulletCooldown = 2f;
    float bulletTimer;

    public int doorSpawnIndex;
    public bool inOverworld;

    public HealthBar healthBar;

    private Animator animator;

    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI { get { return dialogueUI; } set { dialogueUI = value; } }
    public IInteractable Interactable { get; set; }
    private void Awake()
    {
        canDamage = true;
        playerIngredients = new List<GameObject>();
        //overworldIngredients = new List<Ingredients>();
        animator = GetComponent<Animator>();
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
        healthCurrent = healthInitial;
        BerryCount = 0;
        bulletCooldown = 2f;
        if (inOverworld)
        {
            Camera.main.GetComponent<CameraController>().player = this;
        }
        DontDestroyOnLoad(gameObject);

        

    }

    private void Update()
    {
        if(bulletTimer <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main_Cauldron" ||
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "BossCauldron")
        {
            GetComponent<SpriteRenderer>().sprite = hatSprite;
            animator.SetBool("InCombat", true);
            animator.SetBool("Walk", false);

        }
        else
        {
            animator.SetBool("InCombat", false);

            if (playerInput != Vector2.zero)
            {
                animator.SetBool("Walk", true);
                if (playerInput.x < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (playerInput.x > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;

                }
            }
            else
            {
                animator.SetBool("Walk", false);

            }
        }
        UpdateHealth();
    }

    void FixedUpdate()
    {
        if (dialogueUI != null)
        {
            if (dialogueUI.IsOpen) return;
        }
        transform.position += (Vector3)playerInput * Time.deltaTime * moveSpeed;
        transform.position = new Vector3(transform.position.x, transform.position.y, 1.5f);
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
            bulletTimer = bulletCooldown;
            healthCurrent -= 1;
            healthBar.SetHealth(healthCurrent);
            //StartCoroutine(HitStun());
            StartCoroutine(FlashPlayer());

            GetComponent<SpriteRenderer>().color = Color.red;
            
        }
    }

    public IEnumerator HitStun()
    {
        canDamage = false;
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }

    public IEnumerator FlashPlayer()
    {
        while (bulletTimer > 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);

        }
    }

    public void UpdateHealth()
    {
        if(healthCurrent > healthInitial)
        {
            healthCurrent = healthInitial;
        }

        else if (healthCurrent <= 0f)
        {

            healthCurrent = 0;
            healthBar.slider.value = healthCurrent;
            PlayerDied();
            DontDestroyOnLoad(gameObject);

        }

        
    }

    public void PlayerDied()
    {

        SceneLoader.instance.GameOver();
        gameObject.SetActive(false);

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
