using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Overworld_Pickup : PickupBase
public class Overworld_Pickup : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OverworldPickup()
    {
        // Get the ingredient type that was picked up
        // Add it to a list of ingredients the Player has
        // When entering a "battle" make sure to transfer over the ingredients to the SceneManager
        // Should probably all be stored on the Player class instead of two seperate classes
    }
}
