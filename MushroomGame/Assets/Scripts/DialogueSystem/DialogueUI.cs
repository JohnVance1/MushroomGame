using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text textLabel;
    //[SerializeField] private DialogueObject testDialogue;
    [SerializeField] private GameObject dialogueBox;
    private TypewriterEffect typewriterEffect;
    
    public bool IsOpen { get; private set; }
   

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>(); //get typerwriter effect
        CloseDialogueBox();
        //ShowDialogue(testDialogue); //call show dialogue method

    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThruDialogue(dialogueObject)); //starts a coroutine that steps through all the dialogue options in the StepThruDialogue method. calls typewriteeffects run method
        
    }

    private IEnumerator StepThruDialogue(DialogueObject dialogueObject)
    {
        
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame);
        }

        CloseDialogueBox();


    }

    private void CloseDialogueBox()
    {
        IsOpen = false; 
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;  
    }

}
