using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private List<IngredientShootBase> storedIngredients;
    public List<IngredientShootBase> requiredIngredients;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ingredient")
        {
            //Destroy(gameObject);
            //Player.instance.BerryCount++;
            //SceneManager.instance.SpawnIngredient();
        }
    }
}
