using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grax_AtWar : MonoBehaviour
{
    public bool atWar = false;
    GameManager gm;
    OrcMeleeStats cogM;
    OrcRangeStats cogR;
    System.Random rn;
    GameObject spawn;
    Transform[] waypoint;
    Transform[] basewaypoint;
    int waypointIndex;
    Transform tran;
    int role = 2;
    int time = 120;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cogM = gm.GetComponent<OrcMeleeStats>();
        cogR = gm.GetComponent<OrcRangeStats>();
        rn = new System.Random();
        spawn = GameObject.Find("CubeGraxian_Spawn");
        List<GameObject> test = new List<GameObject>(GameObject.FindGameObjectsWithTag("Graxian Spawn"));
        List<Transform> lista = new List<Transform>();
        foreach (var tes in test)
        {
            lista.Add(tes.transform);
            if (tes.transform.position.x == (float)-23.67)
            {
                tran = tes.transform;
            }
        }
        waypoint = lista.ToArray();
        test = new List<GameObject>(GameObject.FindGameObjectsWithTag("Base Location"));
        lista = new List<Transform>();
        foreach (var tes in test)
        {
            lista.Add(tes.transform);

        }
        basewaypoint = lista.ToArray();
        StartCoroutine(spawnAttack());
        StartCoroutine(resource());
    }

    // Update is called once per frame
    void Update()
    {
        if (atWar)
        {
            if (gm.graxian_Minerals > 100)
            {
                SpawnWar();
            }
            StartCoroutine(WarSpawn());
        }
        else
        {
            if (gm.graxian_Minerals > 500 && gm.Graxian.Count <= 10)
            {
                StartCoroutine(Spawn());
            }
        }
    }

    IEnumerator resource()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            gm.graxian_Minerals += 100;
        }
    }

    IEnumerator Spawn()
    {
        gm.graxian_Minerals -= 100;
        yield return new WaitForSeconds(3);
        int check = rn.Next(0, 100);
        if (check >= 80)
        {
            var objectToSpawn = cogM.GetRobotMinerObject();
            var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
            spawnedMiner.GetComponent<Orc_Movement_AI>().target = waypoint[0].position;
            gm.Graxian.Add(spawnedMiner);
        }
        if (check <= 20)
        {
            var objectToSpawn = cogR.GetRobotMinerObject();
            var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
            spawnedMiner.GetComponent<Orc_Movement_AI>().target = waypoint[0].position;
            gm.Graxian.Add(spawnedMiner);
        }
    }

    public void SpawnWar()
    {
        gm.graxian_Minerals -= 100;
        int check = rn.Next(0, 3);
        if (waypointIndex == 3)
        {
            waypointIndex = 0;
        }
        if (check == 3)
        {
            var objectToSpawn = cogM.GetRobotMinerObject();
            var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
            spawnedMiner.GetComponent<Orc_Movement_AI>().target = waypoint[0].position;
            gm.Graxian.Add(spawnedMiner);
            waypointIndex++;
        }
        if (check == 1)
        {
            var objectToSpawn = cogR.GetRobotMinerObject();
            var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
            spawnedMiner.GetComponent<Orc_Movement_AI>().target = waypoint[0].position;
            gm.Graxian.Add(spawnedMiner);
            waypointIndex++;
        }
    }

    IEnumerator WarSpawn()
    {
        yield return new WaitForSeconds(30);
        StartCoroutine(spawnAttack());
    }


    IEnumerator spawnAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            for (int i = 0; i < (role); i++)
            {
                int check = rn.Next(0, 100);
                if (check >= 51)
                {
                    var objectToSpawn = cogM.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                    spawnedMiner.GetComponent<Orc_Movement_AI>().target = waypoint[0].position;
                    StartCoroutine(startAttack(spawnedMiner));
                }
                if (check <= 50)
                {
                    var objectToSpawn = cogR.GetRobotMinerObject();
                    var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
                    spawnedMiner.GetComponent<Orc_Movement_AI>().target = waypoint[0].position;
                    StartCoroutine(startAttack(spawnedMiner));
                }
            }
            role += 2;
            if(time >= 20)
                time -= 10;
            //if(gm.Graxian.Count >= role)
            //{
            //    int x = 0;
            //    foreach (var cogs in gm.Graxian)
            //    {
            //        if(x >= role)
            //        {
            //            //time -= 10;
            //            role += 2;
            //        }
            //        if (cogs.GetComponent<Orc_Attack_AI>().getTarget() == null)
            //        {
            //            var target = GameObject.FindGameObjectWithTag("Building");
            //            cogs.GetComponent<Orc_Movement_AI>().targ = target;
            //        }
            //        x++;
            //    }
            //}
        }
    }

    IEnumerator startAttack(GameObject orc)
    {
        orc.GetComponent<Orc_Movement_AI>().target = Vector3.zero;
        while (true)
        {
            if (orc.GetComponent<Orc_Movement_AI>().target == Vector3.zero)
            {
                var target = GameObject.Find("Attack1").transform.position;
                orc.GetComponent<Orc_Movement_AI>().target = target;
            }
            yield return new WaitForSeconds(2);
            var target2 = GameObject.Find("Attack1").transform.position;
            if (Vector3.Distance(orc.transform.position, target2) < 15)
            {
                var target3 = GameObject.FindGameObjectWithTag("Building");
                orc.GetComponent<Orc_Movement_AI>().targ = target3;
                break;
            }
        }
    }

}
