using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf_AtWar : MonoBehaviour
{
    public bool atWar = false;
    GameManager gm;
    ELF_Melee_Stat cogM;
    Elf_Ranged_Stat cogR;
    System.Random rn;
    GameObject spawn;
    Transform[] waypoint;
    int waypointIndex;
    Transform tran;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cogM = GetComponent<ELF_Melee_Stat>();
        cogR = GetComponent<Elf_Ranged_Stat>();
        rn = new System.Random();
        spawn = GameObject.Find("elf_spawn");
        List<GameObject> test = new List<GameObject>(GameObject.FindGameObjectsWithTag("elf spawn"));
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
        StartCoroutine(resource());
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.sleemasi_Minerals > 100)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator resource()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            gm.sleemasi_Minerals += 100;
        }
    }

    IEnumerator Spawn()
    {
        gm.sleemasi_Minerals -= 100;
        yield return new WaitForSeconds(3);
        int check = rn.Next(0, 100);
        if (check >= 60)
        {
            var objectToSpawn = cogM.GetRobotMinerObject();
            var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
            spawnedMiner.GetComponent<Elf_Movement_AI>().target = waypoint[0].position;
        }
        if (check <= 50)
        {
            var objectToSpawn = cogR.GetRobotMinerObject();
            var spawnedMiner = Instantiate(objectToSpawn, spawn.transform.position, spawn.transform.rotation);
            spawnedMiner.GetComponent<Elf_Movement_AI>().target = waypoint[0].position;
        }
    }
}
