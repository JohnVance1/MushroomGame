using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;
    public float rotation;
    public float lifeTime;
    float timer; 
    // Start is called before the first frame update
    void Start()
    {
        timer = lifeTime;
        transform.rotation = Quaternion.Euler(0,0,rotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if(timer <= 0) gameObject.SetActive(false);
    }
    public void ResetTimer()
    {
        timer = lifeTime;
    }
}
