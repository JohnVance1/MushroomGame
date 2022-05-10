using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustSceneLoading : MonoBehaviour
{
    public int changing;
    public void ChangeScene()
    {
        SceneManager.LoadScene(changing);
    }
}
