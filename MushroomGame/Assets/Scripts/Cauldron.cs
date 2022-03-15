using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
        if(storedIngredients.Count < requiredIngredients.Count)
        {
            return false;
        }

        if(requiredIngredients.Count <= 0)
        {
            return true;
        }

        return false;                
    }

    public void AddStoredIngredient(IngredientShootBase shootBase)
    {
        storedIngredients.Add(shootBase.type);
        if(requiredIngredients.Contains(shootBase.type))
        {
            requiredIngredients.Remove(shootBase.type);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ingredient")
        {
            AddStoredIngredient(collision.gameObject.GetComponent<IngredientShootBase>());
        }
    }
}
