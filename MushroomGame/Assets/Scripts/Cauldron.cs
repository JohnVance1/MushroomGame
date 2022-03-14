using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public List<Ingredients> storedIngredients;
    public List<Ingredients> requiredIngredients;

    void Start()
    {
        
    }

    void Update()
    {
        if (CheckCompleteCauldron())
        {
            Debug.Log("Potion Complete!");
        }

    }

    public bool CheckCompleteCauldron()
    {
        return false;
    }

    public void AddStoredIngredient(GameObject shootBase)
    {
        storedIngredients.Add(shootBase.GetComponent<IngredientShootBase>().type);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ingredient")
        {
            AddStoredIngredient(collision.gameObject);
        }
    }
}
