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

    public List<GameObject> playerIngredients;

    [SerializeField] private int healthInitial = 10;
    private int healthCurrent;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerIngredients = new List<GameObject>();
        if (instance == null)
        {
            instance = this;
        }  
    }

    void FixedUpdate()
    {
        transform.position += (Vector3)playerInput * Time.deltaTime * moveSpeed;
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
    public void ResetHealth()
    {
        healthCurrent = healthInitial; 
    }

    /// <summary>
    /// Has the Player take a certain amount of damage
    /// </summary>
    /// <param name="damageAmount"> The amount of damage for the Player to take </param>
    public void TakeDamage(int damageAmount)
    {
        healthCurrent -= damageAmount;
        if (healthCurrent <= 0)
        {
            Destroy(gameObject);
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
