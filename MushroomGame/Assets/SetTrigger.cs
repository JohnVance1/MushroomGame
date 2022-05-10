using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrigger : MonoBehaviour
{
    public Animator animator;
    public string triggerName;
    public string exitTrigger;
    public string levelToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger(triggerName);
        StartCoroutine(StartBossFight());
    }

    public IEnumerator StartBossFight()
    {
        yield return new WaitForSeconds(2f);
        SceneLoader.instance.OnEnteredExitTrigger(exitTrigger, levelToLoad);

    }

}
