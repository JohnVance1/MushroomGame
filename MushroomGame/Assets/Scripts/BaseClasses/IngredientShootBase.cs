using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Base class for the Projectile Ingredients
/// </summary>
public class IngredientShootBase : Ingredient
{
    public float moveSpeed;

    void Start()
    {

    }

    public virtual void Update()
    {
        transform.position += -transform.position.normalized * Time.deltaTime * moveSpeed;

        if (transform.position.magnitude <= 0.25)
        {
            Destroy(gameObject);

        }
    }


}
