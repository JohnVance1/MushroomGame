using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Cauldron : MonoBehaviour
{
    // The list of ingredients that the Cauldron currently stores
    public List<Ingredients> storedIngredients;

    // The list of required ingredients for this Cauldron
    // This is set in the Unity Inspector
    public List<Ingredients> requiredIngredients;

    [Tooltip("The random number for the \"Random Required Ingredients\" button")]
    public int randomIngredientNumber;

    void Start()
    {
        randomIngredientNumber = 0;
    }

    void Update()
    {
        if (CheckCompleteCauldron())
        {
            Debug.Log("Potion Complete!");
        }

    }

    /// <summary>
    /// Checks to see if the required amount of ingredients is reached
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Adds an ingredient to the ones the Cauldron currently stores
    /// </summary>
    /// <param name="shootBase"></param>
    public void AddStoredIngredient(IngredientShootBase shootBase)
    {
        storedIngredients.Add(shootBase.type);
        if(requiredIngredients.Contains(shootBase.type))
        {
            requiredIngredients.Remove(shootBase.type);
        }
    }

    /// <summary>
    /// Sets the requiredIngredients to random values based on the 
    /// integer provided
    /// </summary>
    /// <param name="reqiredNum"></param>
    public void RandomRequired(int reqiredNum)
    {
        int rand = 0;
        requiredIngredients.Clear();
        for (int i = 0; i < reqiredNum; i++)
        {
            rand = Random.Range(0, 2);
            requiredIngredients.Add((Ingredients)rand);
        }
    }


    public void AddIngredient(Ingredients ing)
    {
        requiredIngredients.Add(ing);
    }

    public void RemoveIngredient(Ingredients ing)
    {
        requiredIngredients.Remove(ing);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ingredient")
        {
            AddStoredIngredient(collision.gameObject.GetComponent<IngredientShootBase>());
        }
    }
}
