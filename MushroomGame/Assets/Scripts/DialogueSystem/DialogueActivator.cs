
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    //implement Iinteractable interface
    

    private void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main_Cauldron" ||
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "StartingRoom" ||
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SecondRoom")
            {
                Interact(player);

                Destroy(GetComponent<BoxCollider2D>());
            }
            else
            {
                player.Interactable = this;
               
            }
          
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if(player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }

    public void Interact(Player player)
    {
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
