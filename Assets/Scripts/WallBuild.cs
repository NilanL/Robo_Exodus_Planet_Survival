using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuild : MonoBehaviour
{
    Transform wallSpawn;
    GameObject gm;
    GameObject foliage;

    // Start is called before the first frame update
    void Start()
    {
        var foundation = GameObject.Find("Base_Foundation");
        wallSpawn = foundation.transform.Find("Wall_Spawn_Location");
        gm = GameObject.Find("GameManager");
        foliage = GameObject.Find("Removeable_Foliage");
    }

    public void BuildWall_Level1()
    {
        var foundation = GameObject.Find("Base_Foundation");
        wallSpawn = foundation.transform.Find("Wall_Spawn_Location");
        var wall1 = GameObject.Find("GameManager").GetComponent<Wall_Stats>();

        var gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManger.Ironite >= 1000)
        {
            // Add wall
            Instantiate(wall1.getLevel1WallObject(), wallSpawn.position, wallSpawn.rotation);
            gameManger.Ironite -= 1000;

            gameManger.SetIsWallBuilt();
        }
    }

    public void removeFoliage()
    {
        foliage = GameObject.Find("Removeable_Foliage");
        var gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManger.Ironite >= 250)
        {
            gameManger.Ironite -= 250;

            // Remove trees
            if (foliage != null)
                Destroy(foliage);

            gameManger.SetIsFoliageCleared();
        }
    }
}
