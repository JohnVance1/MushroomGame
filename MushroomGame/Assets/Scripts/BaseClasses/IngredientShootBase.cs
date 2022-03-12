using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientShootBase : MonoBehaviour
{
    public float moveSpeed;

    void Start()
    {

    }

    public virtual void Update()
    {
        transform.position += -transform.position.normalized * Time.deltaTime * moveSpeed;

        if (transform.position.magnitude <= 0.2)
        {
            Destroy(gameObject);

        }
    }


}
