using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class for the Berry Projectile
/// </summary>
public class Berry_Projectile : IngredientShootBase
{


    void Start()
    {
        type = Ingredients.Berry;
    }

    public override void Update()
    {
        base.Update();
    }
}
