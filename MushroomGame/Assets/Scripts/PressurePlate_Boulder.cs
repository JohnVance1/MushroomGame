using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate_Boulder : MonoBehaviour
{
    public bool active;
    private bool currentlyTriggered;

    void Start()
    {
        active = false;
        currentlyTriggered = false;
    }

    public void SetPressurePlate()
    {
        active = true;
        currentlyTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "Player" || collision.tag == "Boulder") && !currentlyTriggered)
        {
            Invoke("SetPressurePlate", 0.5f);
            currentlyTriggered = true;
        }
    }
}
