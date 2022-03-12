using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
