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
    public List<GameObject> playerIngredients;

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
        GameObject obj = playerIngredients[0];
        Instantiate(obj, transform.position, Quaternion.identity);
        
    }
}
