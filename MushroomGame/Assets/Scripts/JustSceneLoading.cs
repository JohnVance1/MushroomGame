using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustSceneLoading : MonoBehaviour
{
    public int changing;
    public void ChangeScene()
    {
        Destroy(Player.instance.gameObject);
        Destroy(SceneLoader.instance.gameObject);
        Destroy(CameraController.instance.gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene(changing);
    }
}
