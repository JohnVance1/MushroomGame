using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleThankYou : MonoBehaviour
{
    
    [SerializeField] public GameObject healthBarCanvas;

    [SerializeField] public GameObject dialogueCanvas;
    [SerializeField] public GameObject thankYou;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        healthBarCanvas.SetActive(false);
        dialogueCanvas.SetActive(false);
        thankYou.SetActive(true);
    }
}
