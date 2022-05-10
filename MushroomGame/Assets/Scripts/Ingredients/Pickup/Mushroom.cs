using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class for the Berry Pick-Up
/// </summary>
public class Mushroom : PickUpBase
{
    void Start()
    {
        type = Ingredients.Mushroom;
    }

    void Update()
    {
        
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.instance.PickedUp(this);
            //SceneManager.instance.SpawnIngredient();
            Destroy(gameObject);
        }
    }




}
