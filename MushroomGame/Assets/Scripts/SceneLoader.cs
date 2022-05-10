using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance { get; private set; }

    public Dictionary<string, List<bool>> sceneGates;// = new Dictionary<string, List<bool>>();

    public bool thirdRoomWin;
    public bool bossRoomWin;


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
        thirdRoomWin = false;
        bossRoomWin = false;

    }

    private string lastTrigger;
    private string lastLevel;
    private GateManager gateManager;

    public string LastTrigger { get; }

    void Start()
    {
        //UnityEngine.SceneManagement.SceneManager.sceneUnloaded += UnLoadScene;

        Invoke("AddFirstGate", 0.5f);
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
        UnLoadScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
    }

    public void ExitCauldron(string triggerName)
    {
        //lastTrigger = triggerName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(lastLevel);
    }

    public void AddFirstGate()
    {
        sceneGates = new Dictionary<string, List<bool>>();
        gateManager = FindObjectOfType<GateManager>();
        if (gateManager != null)
        {
            List<bool> isOpen = new List<bool>();
            foreach (GameObject gate in gateManager.gatesAndPlates.Keys)
            {
                isOpen.Add(gate.GetComponent<Gate>().open);
            }
            sceneGates.Add(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, isOpen);
        }
    }

    
    public void UnLoadScene()
    {
        string levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (gateManager != null)
        {
            if (sceneGates.ContainsKey(levelName))
            {
                List<bool> isOpen = new List<bool>();
                foreach (GameObject gate in gateManager.gatesAndPlates.Keys)
                {
                    isOpen.Add(gate.GetComponent<Gate>().open);
                }
                sceneGates[levelName] = isOpen;
            }
        }
    }

    void OnLevelWasLoaded()
    {
        string levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if(levelName == "ThirdRoom" && thirdRoomWin == true)
        {
            GameObject[] tutorialPersons = GameObject.FindGameObjectsWithTag("NPC");
            foreach(GameObject person in tutorialPersons)
            {
                person.GetComponent<SpriteRenderer>().enabled = !person.GetComponent<SpriteRenderer>().enabled;
                person.GetComponentInChildren<BoxCollider2D>().enabled = !person.GetComponentInChildren<BoxCollider2D>().enabled;

            }

        }

        if (levelName == "BossRoom 1" && bossRoomWin == true)
        {
            GameObject[] tutorialPersons = GameObject.FindGameObjectsWithTag("DialogueDelete");
            foreach (GameObject person in tutorialPersons)
            {
                person.SetActive(false);

            }

            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            boss.GetComponent<Animator>().SetTrigger("Happy");

        }


        DialogueUI dialouge = FindObjectOfType<DialogueUI>();
        if(dialouge != null)
        {
            Player.instance.DialogueUI = dialouge;
        }

        if (sceneGates != null)
        {
            if (!sceneGates.ContainsKey(levelName))
            {
                gateManager = FindObjectOfType<GateManager>();
                if (gateManager != null)
                {
                    List<bool> isOpen = new List<bool>();
                    foreach (GameObject gate in gateManager.gatesAndPlates.Keys)
                    {
                        isOpen.Add(gate.GetComponent<Gate>().open);
                    }
                    sceneGates.Add(levelName, isOpen);
                }

            }
            else
            {
                gateManager = FindObjectOfType<GateManager>();
                if (gateManager != null)
                {
                    List<bool> tempGates = new List<bool>();
                    if (sceneGates.TryGetValue(levelName, out tempGates))
                    {
                        int i = 0;
                        foreach (GameObject gate in gateManager.gatesAndPlates.Keys)
                        {
                            if (tempGates[i])
                            {
                                gate.GetComponent<Gate>().OpenGate();
                            }
                            i++;
                        }
                    }
                }

            }
        }

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

    public void GameOver()
    {
        SceneManager _ui = SceneManager.instance;
        if (_ui != null)

        {
            _ui.ToggleDeathCanvas();
        }    
    }
    public void Exit()
    {
        Application.Quit();
    }

    public int changing;
}
 
