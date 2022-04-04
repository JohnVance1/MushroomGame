using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timelvl1 : MonoBehaviour
{
    private bool[] tasks = new bool[100];
    private bool[] repeatedTasks = new bool[100];
    public BulletSpawner bulletSpawner;

    public void Start()
    {
        Time.fixedDeltaTime = 0.001f;

    }

    public void FixedUpdate()

    {
        for (float i = 0; i < 10; i += 1)
        {
            if (TimeManager.isTime((float)i/10f, ref repeatedTasks[(int)(i * 10)]))
            {
                bulletSpawner.index = 0;
                bulletSpawner.SpawnBullets();
            }
        }

        if (TimeManager.isTime(1f, ref tasks[0]))
        {
            bulletSpawner.index = 0;
            bulletSpawner.SpawnBullets();
        }

        if (TimeManager.isTime(2f, ref tasks[1]))
        {
            bulletSpawner.index = 0;
            bulletSpawner.SpawnBullets();
        }

        if (TimeManager.isTime(3f, ref tasks[2]))
        {
            bulletSpawner.index = 1;
            bulletSpawner.SpawnBullets();
        }

        
    }
}
