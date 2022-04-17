using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    void Start()
    {
        
    }


    public void OpenGate()
    {
        gameObject.SetActive(false);
    }
}
