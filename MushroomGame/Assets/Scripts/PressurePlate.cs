using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool active;
    private bool currentlyTriggered;
    public Sprite activeSprite;
    public Sprite inActiveSprite;

    void Start()
    {
        active = false;
        currentlyTriggered = false;
    }

    private void Update()
    {
        if(active)
        {
            GetComponent<SpriteRenderer>().sprite = activeSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = inActiveSprite;

        }
    }

    public void SetPressurePlateTrue()
    {
        active = true;
        currentlyTriggered = false;
    }

    public void SetPressurePlateFalse()
    {
        active = false;
        currentlyTriggered = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if((collision.tag == "Player" || collision.tag == "Boulder") && !currentlyTriggered)
        {
            Invoke("SetPressurePlateTrue", 0.2f);
            currentlyTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "Boulder"))
        {
            Invoke("SetPressurePlateFalse", 0.2f);
        }
    }
}
