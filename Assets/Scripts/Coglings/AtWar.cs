using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtWar : MonoBehaviour
{

    public bool atWar = false;
    GameManager gm;
    Coglings_Melee_Stat cogM;
    Coglings_Range_Stat cogR;
    System.Random rn;
    GameObject spawn;
    Transform[] waypoint;
    int waypointIndex;
    Transform tran;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cogM = GetComponent<Coglings_Melee_Stat>();
        cogR = GetComponent<Coglings_Range_Stat>();
        rn = new System.Random();
        spawn = GameObject.Find("CubeCoglings_Spawn");
        List<GameObject> test = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cogling Patrol"));
        List<Transform> lista = new List<Transform>();
        foreach (var tes in test)
        {
            lista.Add(tes.transform);
            if(tes.transform.position.x == (float)-23.67)
            {
                tran = tes.transform;
            }
        }
        waypoint = lista.ToArray();
        StartCoroutine(resource());
    }

    // Update is called once per frame
    void Update()
    {
        if(atWar)
        {
            if (gm.cogling_Minerals > 200)
            {
                SpawnWar();
            }
            if(gm.Coglings.Count > 5)
            {
                foreach(var cogs in gm.Coglings)
                {
                   if(cogs.GetComponent<Coglings_Movement_AI>().targ == null)
                    {
                        var target = GameObject.FindGameObjectWithTag("Building");
                        cogs.GetComponent<Coglings_Movement_AI>().targ = target;
                    }
                }
            }
        }
        else
        {
            if(gm.cogling_Minerals > 500)
            {
                StartCoroutine(Spawn());
            }
        }
    }

    IEnumerator resource()
    {
        while(true)
        {
            yield return new WaitForSeconds(30);
            gm.cogling_Minerals += 100;
        }
    }

    IEnumerator Spawn()
    {
        gm.cogling_Minerals -= 500;
        yield return new WaitForSeconds(3);
        int check = rn.Next(0, 100);
        if(check >= 90)
        {
            var objectToSpawn = cogM.GetRobotMinerObject();
            var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
            spawnedMiner.GetComponent<Coglings_Movement_AI>().target = waypoint[0].position;
        }
        if(check <= 10)
        {
            var objectToSpawn = cogR.GetRobotMinerObject();
            var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
            spawnedMiner.GetComponent<Coglings_Movement_AI>().target = waypoint[0].position;
        }
    }

    public void SpawnWar()
    {
        gm.cogling_Minerals -= 200;
        int check = rn.Next(0, 3);
        if(waypointIndex == 3)
        {
            waypointIndex = 0;
        }
        if (check == 3)
        {
            var objectToSpawn = cogM.GetRobotMinerObject();
            var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
            spawnedMiner.GetComponent<Coglings_Movement_AI>().target = waypoint[waypointIndex].position;
            waypointIndex++;
        }
        if (check == 1)
        {
            var objectToSpawn = cogR.GetRobotMinerObject();
            var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
            spawnedMiner.GetComponent<Coglings_Movement_AI>().target = waypoint[waypointIndex].position;
            waypointIndex++;
        }
    }


}
