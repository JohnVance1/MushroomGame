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
    public List<GameObject> projectilePrefabs;
    public List<GameObject> pickUpPrefabs;
    public GameObject banana;
    public Cauldron cauldron;


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

        //Instantiate(banana, Location(), Quaternion.identity);
        
    }

    internal static void LoadScene(int change)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Switches the ingredient type for the Player
    /// </summary>
    /// <param name="ingBase"></param>
    /// <returns></returns>
    public GameObject SwitchIngredientType(PickUpBase ingBase)
    {
        foreach(GameObject b in projectilePrefabs)
        {
            if(b.GetComponent<IngredientShootBase>().type == ingBase.type)
            {
                return b;
            }
        }
        return null;
    }

    /// <summary>
    /// Spawns the Ingredient chosen
    /// </summary>
    public void SpawnIngredient()
    {
        GameObject spawnee = null;

        foreach (GameObject obj in pickUpPrefabs)
        {
            if (cauldron.requiredIngredients.Contains(obj.GetComponent<PickUpBase>().type))
            {
                spawnee = obj;
            }
        }

        Instantiate(spawnee, Location(), Quaternion.identity);
    }

    /// <summary>
    /// Helper Method for getting a random Vector2 location
    /// </summary>
    /// <returns></returns>
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

        if (player.playerIngredients.Count >= 1)
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

    /// <summary>
    /// A helper method for checking if something is the same sign (+ or -)
    /// </summary>
    /// <param name="num1"></param>
    /// <param name="num2"></param>
    /// <returns></returns>
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
