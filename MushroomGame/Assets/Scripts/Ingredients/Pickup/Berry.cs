using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : IngredientBase
{
    void Start()
    {
        type = Ingredients.Berry;
    }

    void Update()
    {
        
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.instance.PickedUp(this);
            Destroy(gameObject);
            Player.instance.BerryCount++;
            SceneManager.instance.SpawnIngredient();
        }
    }




}
