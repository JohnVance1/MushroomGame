using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletsTest : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    
    [SerializeField] private GameObject projectile;


    
    public float fireRate;
    public float speed;
    

    private void OnCollisionWithPlayer(GameObject player)
    {
        Player player1 = player.GetComponent<Player>();


        //player1.TakeDamage(damageAmount);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Ingredient")
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Vector2 plenemy = Player.instance.transform.position - transform.position;
        transform.position += (Vector3)(Time.deltaTime * speed * plenemy.normalized);


    }

 

}
