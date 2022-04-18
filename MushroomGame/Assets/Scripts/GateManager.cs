using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class GateManager : SerializedMonoBehaviour
{
    [OdinSerialize]
    public Dictionary<GameObject, List<GameObject>> gatesAndPlates = new Dictionary<GameObject, List<GameObject>>();

    private List<bool> openGates = new List<bool>();
    private bool openGate;
    private int index;

    void Start()
    {
        for(int i = 0; i < gatesAndPlates.Count; i++)
        {
            openGates.Add(true);
        }
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        openGate = true;
        index = 0;

        for (int i = 0; i < openGates.Count; i++)
        {
            openGates[i] = true;
        }

        foreach (GameObject gate in gatesAndPlates.Keys)
        {
            foreach(GameObject plate in gatesAndPlates[gate])
            {
                if (!plate.GetComponent<PressurePlate>().active)
                {
                    openGates[index] = false;
                }
            }
            index++;

        }
        index = 0;
        //foreach (GameObject plate in plates)
        //{
        //    if (!plate.GetComponent<PressurePlate>().active)
        //    {
        //        openGate = false;
        //    }
        //}

        foreach (GameObject gate in gatesAndPlates.Keys)
        {
            if (openGates[index])
            {
                gate.GetComponent<Gate>().OpenGate();
            }
            index++;
        }
        
    }
}
