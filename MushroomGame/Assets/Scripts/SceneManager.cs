using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    public Vector2 coordinate;
    public GameObject bound1;
    public GameObject bound2;
    public GameObject berry;
    public static SceneManager instance;
    private Player player;
    public GameObject banana;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        player = Player.instance;

        SpawnIngredient();

        Instantiate(banana, Location(), Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnIngredient()
    {
        Instantiate(berry, Location(), Quaternion.identity);

    }

    public Vector2 Location ()
    {
        float minx = 0;
        float maxx = 0;
        float miny = 0;
        float maxy = 0;

        float randX = 0;
        float randY = 0;


        if (bound1.transform.position.x > bound2.transform.position.x)
        {
            maxx = bound1.transform.position.x;
            minx = bound2.transform.position.x;

            maxy = bound2.transform.position.y;
            miny = bound1.transform.position.y;

        }

        else
        {
            maxx = bound2.transform.position.x;
            minx = bound1.transform.position.x;

            maxy = bound1.transform.position.y;
            miny = bound2.transform.position.y;


        }

        randX = Random.Range(minx, maxx);
        randY = Random.Range(miny, maxy);

        if (player.BerryCount >= 1)
        {
            if(SameSign(player.transform.position.x, randX) &&
                SameSign(player.transform.position.y, randY))
            {
                int abs = Random.Range(0, 2);

                if(abs == 0)
                {
                    randX *= -1;
                }
                else
                {
                    randY *= -1;

                }
            }
        }

        coordinate = new Vector2(randX, randY);

        return coordinate;
    }

    public bool SameSign(float num1, float num2)
    {
        if((num1 > 0 && num2 > 0) || (num1 < 0 && num2 < 0))
        {
            return true;
        }
        return false;


        //return num1 > 0 && num2 > 0 || num1 < 0 && num2 < 0;
    }
}
