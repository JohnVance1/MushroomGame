using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingCauldron : MonoBehaviour
{
    public Player player;
    public float speed;

    void Start()
    {
        player = Player.instance;
    }

    void Update()
    {
        if(OutsideRadius(player.transform.position, 3f))
        {
            transform.position += Seek(player.transform.position) * Time.deltaTime * speed;
        }


    }

    public Vector3 Seek(Vector3 pos)
    {
        return (pos - transform.position).normalized;
    }

    public bool OutsideRadius(Vector3 checkPos, float radius)
    {
        if(Vector3.Distance(checkPos, transform.position) > radius)
        {
            return true;
        }
        return false;

    }


}
