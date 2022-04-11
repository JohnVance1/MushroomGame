using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToRoom : MonoBehaviour
{
    public string triggerName; //this should match on both ends
    public string levelToLoad; //what level this trigger leads to
    public Transform spawnPoint; //where the player should spawn. IMPORTANT: not inside this trigger

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.GetComponent<Player>() != null)
            SceneLoader.instance.OnEnteredExitTrigger(triggerName, levelToLoad);
    }

}
