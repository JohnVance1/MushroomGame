using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            //Player.instance.BerryCount++;
            SceneManager.instance.SpawnIngredient();
        }
    }
}
