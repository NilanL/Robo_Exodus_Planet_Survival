using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTroopTesting : MonoBehaviour
{
    Text textField;
    GameObject gm;
    int num;
    public GameObject objectToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("Troops").GetComponent<Text>();
        gm = GameObject.Find("GameManager");        

    }

    void Update()
    {
        textField.text = "Troops " + gm.GetComponent<GameManager>().Unit_count + "/20";
    }


    public void Spawn_miner()
    {
        if (gm.GetComponent<GameManager>().Ironite >= 100 && !(gm.GetComponent<GameManager>().Unit_count > 19))
        {
            gm.GetComponent<GameManager>().Unit_count += 1;
            var minerStats = gm.GetComponent<MinerStats>();
            objectToSpawn = minerStats.GetRobotMinerObject();//GameObject.Find("robot_miner_mouse");
            //var camera = GameObject.Find("ParentCamera").;
            //objectToSpawn.GetComponent<Robot_Miner_Controller_Mouse>()._camera = camera;

            var objectToSpawnAt = GameObject.Find("Spawn_Location");

            var spawnedMiner = Instantiate(objectToSpawn, objectToSpawnAt.transform.position, objectToSpawnAt.transform.rotation);

            gm.GetComponent<GameManager>().Ironite -= 100;

        }
    }

    public void Spawn_melee()
    {
        if (gm.GetComponent<GameManager>().Ironite >= 100 && !(gm.GetComponent<GameManager>().Unit_count > 19))
        {
            gm.GetComponent<GameManager>().Unit_count += 1;
            var minerStats = gm.GetComponent<Melle_UnitStats>();
            objectToSpawn = minerStats.GetRobotMinerObject();//GameObject.Find("robot_miner_mouse");
            //var camera = GameObject.Find("ParentCamera").;
            //objectToSpawn.GetComponent<Robot_Miner_Controller_Mouse>()._camera = camera;

            var objectToSpawnAt = GameObject.Find("Spawn_Location");

            var spawnedMiner = Instantiate(objectToSpawn, objectToSpawnAt.transform.position, objectToSpawnAt.transform.rotation);

            gm.GetComponent<GameManager>().Ironite -= 100;

        }
    }

    public void Spawn_range()
    {
        if (gm.GetComponent<GameManager>().Ironite >= 100 & !(gm.GetComponent<GameManager>().Unit_count > 19))
        {
            gm.GetComponent<GameManager>().Unit_count += 1;
            var minerStats = gm.GetComponent<Robot_Range_Stats>();
            objectToSpawn = minerStats.GetRobotMinerObject();//GameObject.Find("robot_miner_mouse");
            //var camera = GameObject.Find("ParentCamera").;
            //objectToSpawn.GetComponent<Robot_Miner_Controller_Mouse>()._camera = camera;

            var objectToSpawnAt = GameObject.Find("Spawn_Location");

            var spawnedMiner = Instantiate(objectToSpawn, objectToSpawnAt.transform.position, objectToSpawnAt.transform.rotation);

            gm.GetComponent<GameManager>().Ironite -= 100;

        }
    }

}