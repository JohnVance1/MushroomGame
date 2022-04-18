using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Sprite openSprite;
    public bool open = false;
    void Start()
    {
        //open = false;
    }


    public void OpenGate()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = openSprite;
        open = true;
    }
}
