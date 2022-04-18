using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletSpawnData[] spawnDatas;
    public int index = 0;
    public bool isSequenceRandom;
    public bool spawningAutomatically;
    BulletSpawnData GetSpawnData()
    {
        return spawnDatas[index];
    }

    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    float timer;
    float newtimer;
    float newtimermax;
  

    float[] rotations;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetSpawnData().cooldown;
        newtimermax = 2f;
        newtimer = 0;
       // rotations = new float[GetSpawnData().numberofBullets];

        if (GetSpawnData().isRandom)
        {

            DistributeRotations();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueUI.IsOpen) return;
        if (spawningAutomatically)
        {
            if (timer <= 0)
            {
                SpawnBullets();

                timer = GetSpawnData().cooldown;

                if (isSequenceRandom)
                {
                    index = Random.Range(0, spawnDatas.Length);
                }

                else
                {
                    index += 1;

                    if (index >= spawnDatas.Length) index = 0;
                }
                

            }

            timer -= Time.deltaTime;
        }
        
    }

    public GameObject[] SpawnBullets()
    {
       rotations = new float[GetSpawnData().numberofBullets];
        { 
         RandomRotations();
       }


        GameObject[] spawnedBullets = new GameObject[GetSpawnData().numberofBullets];

        for (int i = 0; i < GetSpawnData().numberofBullets; i++)
        {
            spawnedBullets[i] = BulletManager.GetBulletFromPool();

           if(spawnedBullets[i] == null)

            { 
                spawnedBullets[i] = Instantiate(GetSpawnData().bulletResource, transform);
            }
           else
            {
                spawnedBullets[i].transform.SetParent(transform);

                spawnedBullets[i].transform.localPosition = Vector2.zero;
            }
           var b = spawnedBullets[i].GetComponent<Bullet>();

           b.rotation = rotations[i];

           b.speed = GetSpawnData().bulletSpeed;

           b.velocity = GetSpawnData().bulletVelocity;

            if (GetSpawnData().isNotParent) spawnedBullets[i].transform.SetParent(null);

        }

        
        return spawnedBullets;  
    }

    public float[] RandomRotations()
    {
        for (int i = 0; i < GetSpawnData().numberofBullets; i++)
        {
            rotations[i] = Random.Range(GetSpawnData().minRotation, GetSpawnData().maxRotation);
        }
        return rotations;
    }
    public float[] DistributeRotations()
    {
        for (int i = 0; i < GetSpawnData().numberofBullets; i++)
        {
            var fraction = (float)i / ((float)GetSpawnData().numberofBullets - 1);

            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;

            var fractionofDifference = fraction * difference;

            rotations[i] = fractionofDifference * GetSpawnData().minRotation;

        }
        foreach (var r in rotations) print(r);

        return rotations;
    }
}
