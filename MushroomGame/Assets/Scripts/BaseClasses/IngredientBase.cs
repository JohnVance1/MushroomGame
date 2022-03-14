using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBase : Ingredient
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Player.instance.BerryCount++;
            Player.instance.PickedUp(this);
            SceneManager.instance.SpawnIngredient();
            Destroy(gameObject);

        }
    }
}
