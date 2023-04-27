using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flocking : MonoBehaviour
{
    public NavMeshAgent spawnPosition;
    public Transform targetPosition;
    public Flocking_Wait fw;
    public Orc_Movement_AI om;
    GameManager gm;
    List<GameObject> Orcs;

    [HideInInspector]
    public bool pathAvailable;
    public NavMeshPath navMeshPath;
    bool noPath = false;


    // Start is called before the first frame update
    void Start()
    {
        fw = this.gameObject.GetComponent<Flocking_Wait>();
        om = this.gameObject.GetComponent<Orc_Movement_AI>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Orcs = gm.Graxian;
        spawnPosition = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Orcs = gm.GraxianMiner;
        if (spawnPosition.hasPath)
        {
            noPath = true;
            foreach(var orc in Orcs)
            {
                if(Vector3.Distance(orc.transform.position, this.gameObject.transform.position) < 20)
                {
                    if(!orc.GetComponent<Flocking_Wait>().pathAvailable)
                    {
                        noPath = false;
                        
                    }
                }
            }
            if(!noPath)
            {
                spawnPosition.isStopped = true; 
            }
            else
            {
                spawnPosition.isStopped = false;
            }
        }
    }
}
