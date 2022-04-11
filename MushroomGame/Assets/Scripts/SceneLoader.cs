using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            //Destroy(this);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private string lastTrigger;


    void Start()
    {        
        DontDestroyOnLoad(gameObject);
    }

    public void OnEnteredExitTrigger(string triggerName, string levelToLoad)
    {
        lastTrigger = triggerName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
    }

    void OnLevelWasLoaded()
    {
        GameObject playerObject = Player.instance.gameObject;
        MoveToRoom[] allExits = FindObjectsOfType<MoveToRoom>();
        foreach (MoveToRoom exit in allExits)
        {
            if (exit.triggerName == lastTrigger)
            {
                playerObject.transform.position = exit.spawnPoint.position;
                CameraController.instance.LevelBounds = FindObjectOfType<RegularRoom>().roomBounds;
                return;
            }
        }
        //handle there not being any sceneloaders here somehow.
    }
}
 
