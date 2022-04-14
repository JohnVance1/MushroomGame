using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    public GameObject gate;
    public List<GameObject> plates = new List<GameObject>();
    private bool openGate;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        openGate = true;
        foreach (GameObject plate in plates)
        {
            //if(!plate.GetComponent<PressurePlate>().active)
            //{
            //    openGate = false;
            //}
        }

        if(openGate)
        {
            gate.GetComponent<Gate>().OpenGate();
        }
    }
}
