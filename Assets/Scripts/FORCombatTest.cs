using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FORCombatTest : MonoBehaviour
{
    GameObject spawn;
    Coglings_Melee_Stat cogM;
    Coglings_Range_Stat cogR;
    //Remove after testing
    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.Find("CubeCoglings_Spawn");
        cogM = GetComponent<Coglings_Melee_Stat>();
        cogR = GetComponent<Coglings_Range_Stat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseUnits(int count)
    {
        System.Random ran = new System.Random();
        var number = ran.Next(0, 10);
        switch(number)
        {
            case 0:
                for (int i = 0; i < (int)count; i++)
                    {
                    var objectToSpawn = cogM.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                break;
            case 1:
                for (int i = 0; i < Mathf.FloorToInt(count * .9f); i++)
                {
                    var objectToSpawn = cogM.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                for (int i = 0; i < Mathf.FloorToInt(count * .1f); i++)
                {
                    var objectToSpawn = cogR.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                break;
            case 2:
                for (int i = 0; i < Mathf.FloorToInt(count * .8f); i++)
                {
                    var objectToSpawn = cogM.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                for (int i = 0; i < Mathf.FloorToInt(count * .2f); i++)
                {
                    var objectToSpawn = cogR.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                break;
            case 3:
                for (int i = 0; i < Mathf.FloorToInt(count * .7f); i++)
                {
                    var objectToSpawn = cogM.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                for (int i = 0; i < Mathf.FloorToInt(count * .3f); i++)
                {
                    var objectToSpawn = cogR.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                break;
            case 4:
                for (int i = 0; i < Mathf.FloorToInt(count * .6f); i++)
                {
                    var objectToSpawn = cogM.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                for (int i = 0; i < Mathf.FloorToInt(count * .4f); i++)
                {
                    var objectToSpawn = cogR.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                break;
            case 5:
                for (int i = 0; i < Mathf.FloorToInt(count * .5f); i++)
                {
                    var objectToSpawn = cogM.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                for (int i = 0; i < Mathf.FloorToInt(count * .5f); i++)
                {
                    var objectToSpawn = cogR.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                break;
            case 6:
                for (int i = 0; i < Mathf.FloorToInt(count * .4f); i++)
                {
                    var objectToSpawn = cogM.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                for (int i = 0; i < Mathf.FloorToInt(count * .6f); i++)
                {
                    var objectToSpawn = cogR.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                break;
            case 7:
                for (int i = 0; i < Mathf.FloorToInt(count * .3f); i++)
                {
                    var objectToSpawn = cogM.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                for (int i = 0; i < Mathf.FloorToInt(count * .7f); i++)
                {
                    var objectToSpawn = cogR.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                break;
            case 8:
                for (int i = 0; i < Mathf.FloorToInt(count * .2f); i++)
                {
                    var objectToSpawn = cogM.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                for (int i = 0; i < Mathf.FloorToInt(count * .8f); i++)
                {
                    var objectToSpawn = cogR.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                break;
            case 9:
                for (int i = 0; i < Mathf.FloorToInt(count * .1f); i++)
                {
                    var objectToSpawn = cogM.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                for (int i = 0; i < Mathf.FloorToInt(count * .9f); i++)
                {
                    var objectToSpawn = cogR.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                break;
            case 10:
                for (int i = 0; i < count; i++)
                {
                    var objectToSpawn = cogR.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                }
                break;
        }
        
        

    }

    public void SetTarget()
    {
        var tars = GameObject.FindGameObjectsWithTag("Cogling");
        var target = GameObject.FindGameObjectWithTag("Building");
        foreach (var cog in tars)
        {
            var tar = cog.gameObject.GetComponent<Coglings_Movement_AI>();
            tar.gettingAttacked(target);
        }
    }
}
