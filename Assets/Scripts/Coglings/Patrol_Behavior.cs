using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol_Behavior : MonoBehaviour
{

    bool patrolLeader = false;
    List<GameObject> aurarium = new List<GameObject>();
    Coglings_Movement_AI movement;
    public Transform[] waypoint;
    Vector3 target;
    public GameObject targ;
    public GameObject patrolLeaderObject;
    int waypointIndex;
    bool patroling = false;
    bool mining = true;
    bool targMin = true;
    bool canMine = true;
    public bool goinghome = false;
    public bool AtDest = true;

    GameManager gm;
    int resource = 0;


    bool atWar = false;
    bool waiting = false;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<Unit_Name>().unit_Name == Unit_Names.Cogling_Miner)
            patrolLeader = true;
        var list = new List<GameObject>(GameObject.FindGameObjectsWithTag("Resource"));
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        foreach(var patrol in gm.CoglingMiner)
        {
            if(Vector3.Distance(patrol.transform.position, this.gameObject.transform.position) < 15)
            {
                patrolLeaderObject = patrol;
            }
        }
        aurarium = getZuraruim(list);
        movement = this.gameObject.GetComponent<Coglings_Movement_AI>();
        List<GameObject> test = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cogling Patrol"));
        List<Transform> lista = new List<Transform>();
        foreach(var tes in test)
        {
            lista.Add(tes.transform);
        }
        waypoint = lista.ToArray();
        startPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        if(patrolLeaderObject == null)
        {
            foreach (var patrol in gm.CoglingMiner)
            {
                if (Vector3.Distance(patrol.transform.position, this.gameObject.transform.position) < 10)
                {
                    patrolLeaderObject = patrol;
                }
            }
        }
        if (patrolLeader)
        {
            if (Vector3.Distance(transform.position, target) < 7)
            {
                if (patroling)
                {
                    IterateWaypoints();
                    startPatrol();
                    patroling = false;

                }
                else
                {
                    goinghome = false;
                    if (targ == null)
                    {
                        var list = new List<GameObject>(GameObject.FindGameObjectsWithTag("Resource"));
                        foreach (var zur in getZuraruim(list))
                        {
                            if (Vector3.Distance(transform.position, zur.transform.position) < 20 && targMin)
                            {
                                target = zur.transform.position;
                                targ = zur;
                                movement.target = target;
                                targMin = false;
                                break;
                            }
                        }
                    }
                    //mine until xd units
                    if (targ)
                    {
                        if (Vector3.Distance(transform.position, targ.transform.position) < 7)
                        {
                            StartCoroutine(StartMining());

                        }
                    }
                    else
                    {
                        patroling = true;
                        targMin = true;
                        goinghome = true;
                    }

                }
                //start mining
                //when at x units
                //return home
                
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target) < 7)
            {
                if (AtDest)
                {
                    AtDest = false;
                    StartCoroutine(AtDestination());
                }
            }
        }
    }

    List<GameObject> getZuraruim(List<GameObject> aur)
    {
        var list = new List<GameObject>();
        foreach(var res in aur)
        {
            if(res.GetComponent<ResourceType>().resource == ResourceTypes.Zorium)
            {
                list.Add(res);
            }
        }
        return list;
    }



    void startPatrol()
    {
        if(patrolLeader)
        {
            target = waypoint[waypointIndex].position;
            movement.target = target;
        }
        else
        {
            target = waypoint[waypointIndex].position;
            movement.target = target;
        }
    }

    void IterateWaypoints()
    {
        waypointIndex++;
        if(waypointIndex == waypoint.Length)
        {
            waypointIndex = 0;
            goinghome = false;
        }
    }

    void Mining()
    {
        if (Vector3.Distance(targ.transform.position, transform.position) < 10)
        {

            if (canMine)
            {
                StartCoroutine(Mine());
            }
        }

    }

    IEnumerator AtDestination()
    {

        yield return new WaitForSeconds(.1f);
        if (patrolLeaderObject.GetComponent<Patrol_Behavior>().goinghome)
        {
            IterateWaypoints();
            startPatrol();
            
        }
        AtDest = true;

    }

    IEnumerator StartMining()
    {
        Mining();
        yield return new WaitWhile(() => resource < 100);
        patroling = true;
        gm.cogling_Minerals += 1000;
        resource = 0;
        targ = null;
        targMin = true;
        goinghome = true;
    }

    IEnumerator Mine()
    {
        canMine = false;
        yield return new WaitForSeconds(2);
        if (!(targ == null))
        {
            targ.GetComponent<Resource_HP>().dmgResource(10, this.gameObject);
            //gamemaster.GetComponent<GameManager>().Ironite += 10;
            resource += 10;
        }
        canMine = true;
    }

}
