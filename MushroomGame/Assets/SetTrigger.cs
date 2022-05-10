using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrigger : MonoBehaviour
{
    public Animator animator;
    public string triggerName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger(triggerName);
    }
}
