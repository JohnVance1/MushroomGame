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
    public List<IngredientBase> playerIngredients;

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

    public void PickedUp(IngredientBase ingBase)
    {
        playerIngredients.Add(ingBase);
    }

    public void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    public void OnShoot(InputValue value)
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
