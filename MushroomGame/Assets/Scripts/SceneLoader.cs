using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance { get; private set; }

    public Dictionary<string, List<bool>> sceneGates;// = new Dictionary<string, List<bool>>();

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
    private GateManager gateManager;

    public string LastTrigger { get; }

    void Start()
    {
        //UnityEngine.SceneManagement.SceneManager.sceneUnloaded += UnLoadScene;


        Invoke("AddFirstGate", 0.5f);
        DontDestroyOnLoad(gameObject);
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
        List<bool> isOpen = new List<bool>();
        foreach (GameObject gate in gateManager.gates)
        {
            isOpen.Add(gate.GetComponent<Gate>().open);
        }
        sceneGates.Add(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, isOpen);
    }

    
    public void UnLoadScene()
    {
        string levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (sceneGates.ContainsKey(levelName))
        {
            List<bool> isOpen = new List<bool>();
            foreach (GameObject gate in gateManager.gates)
            {
                isOpen.Add(gate.GetComponent<Gate>().open);
            }
            sceneGates[levelName] = isOpen;
        }
    }

    void OnLevelWasLoaded()
    {
        string levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (sceneGates != null)
        {
            if (!sceneGates.ContainsKey(levelName))
            {
                gateManager = FindObjectOfType<GateManager>();
                List<bool> isOpen = new List<bool>();
                foreach (GameObject gate in gateManager.gates)
                {
                    isOpen.Add(gate.GetComponent<Gate>().open);
                }
                sceneGates.Add(levelName, isOpen);

            }
            else
            {
                gateManager = FindObjectOfType<GateManager>();
                List<bool> tempGates = new List<bool>();
                if (sceneGates.TryGetValue(levelName, out tempGates))
                {
                    for (int i = 0; i < gateManager.gates.Count; i++)
                    {
                        if (tempGates[i])
                        {
                            gateManager.gates[i].GetComponent<Gate>().OpenGate();
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
}
 
