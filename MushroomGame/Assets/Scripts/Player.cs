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
    public void PickedUp(IngredientBase ingBase)
    {
        GameObject shoot = SceneManager.instance.SwitchIngredientType(ingBase);
        playerIngredients.Add(shoot);
    }

    public void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    public void OnShoot(InputValue value)
    {
        if (playerIngredients.Count > 0)
        {
            GameObject obj = playerIngredients[0];
            Instantiate(obj, transform.position, Quaternion.identity);
        }
    }

    public void ResetHealth()
    {
        healthCurrent = healthInitial; 
    }

    public void TakeDamage(int damageAmount)
    {
        healthCurrent -= damageAmount;
        if (healthCurrent <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HealPlayer(int healamount)
    {
        healthCurrent += healamount;

        if(healthCurrent > healthInitial)
        {
            ResetHealth();
        }
    }

}
