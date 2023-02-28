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
        var wall1 = gm.GetComponent<Wall_Stats>();

        if(gm.GetComponent<GameManager>().Ironite >= 500)
        {
            // Add wall
            Instantiate(wall1.getLevel1WallObject(), wallSpawn.position, wallSpawn.rotation);
            gm.GetComponent<GameManager>().Ironite -= 500;
            // Remove trees
            if (foliage != null)
                Destroy(foliage);
        }

    }
}
