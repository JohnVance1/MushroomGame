using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
public class Cauldron : MonoBehaviour
{
    public string triggerName;

    // The list of ingredients that the Cauldron currently stores
    public List<Ingredients> storedIngredients;

    // The list of required ingredients for this Cauldron
    // This is set in the Unity Inspector
    public List<Ingredients> requiredIngredients;

    public DialogueUI dialogueUI;


    [Tooltip("The random number for the \"Random Required Ingredients\" button")]
    public int randomIngredientNumber;

    void Start()
    {
        randomIngredientNumber = 0;
        triggerName = SceneLoader.instance.LastTrigger;
        requiredIngredients = Player.instance.overworldIngredients;
        dialogueUI = Player.instance.DialogueUI;
    }

    void Update()
    {
        if (CheckCompleteCauldron())
        {
            Debug.Log("Potion Complete!");
            Player.instance.overworldIngredients.Clear();
            SceneManager.instance.dialogueCanvas.SetActive(true);
            Player.instance.Interactable = GetComponent<DialogueActivator>();
            GetComponent<DialogueActivator>().Interact(Player.instance);
            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main_Cauldron")
            {
                SceneLoader.instance.thirdRoomWin = true;
            }
            StartCoroutine(WaitForDialouge());
        }

    }

    public IEnumerator WaitForDialouge()
    {
        yield return new WaitForSeconds(2f);
        SceneLoader.instance.ExitCauldron(triggerName);


    }

    public Func<bool> Return() => () => dialogueUI.IsOpen == false;

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
            rand = UnityEngine.Random.Range(0, 2);
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
