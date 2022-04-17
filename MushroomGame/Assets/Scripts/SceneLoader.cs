using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private string lastLevel;

    public string LastTrigger { get; }

    void Start()
    {
        
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int sceneName)
    {
         changing = sceneName;
        Invoke("ChangeScene", 2f);


    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(changing);
    }
    public void OnEnteredExitTrigger(string triggerName, string levelToLoad)
    {
        lastTrigger = triggerName;
        lastLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
    }

    public void ExitCauldron(string triggerName)
    {
        //lastTrigger = triggerName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(lastLevel);
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
    }

    public void Exit()
    {
        Application.Quit();
    }

    public int changing;
}
 
