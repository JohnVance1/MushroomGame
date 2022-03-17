using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class for the Berry Projectile
/// </summary>
public class Mushroom_Projectile : IngredientShootBase
{


    void Start()
    {
        type = Ingredients.Mushroom;
    }

    public override void Update()
    {
        base.Update();
    }
}
