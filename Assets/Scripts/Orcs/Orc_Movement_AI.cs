using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc_Movement_AI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    public Vector3 target;
    public GameObject targ;
    bool isGettingAttacked = false;
    bool inRange = false;
    GameManager gm;
    bool inWar = false;
    public bool booltarg = false;
    public Transform test;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isGettingAttacked)
        //{
        //    foreach (var tar in gm.selectables)
        //    {
        //        if (Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 50)
        //        {
        //            if (inWar)
        //                target = tar.transform.position;
        //            else if (tar.GetComponent<TaskManager>().mining)
        //                target = tar.transform.position;
        //        }
        //    }
        //    foreach (var tar in gm.buildings)
        //    {
        //        if (Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 50)
        //        {
        //            if (targ == null && inWar)
        //                target = tar.transform.position;
        //        }
        //    }
        //}

        if (!isGettingAttacked)
        {
            //var tars = GameObject.FindGameObjectsWithTag("Selectable");
            foreach (var tar in gm.selectables)
            {
                if (((Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 50)))
                {
                    if(targ == null)
                    {
                        if (target == Vector3.zero)
                            targ = tar;
                    }
                    else if(targ.tag == "Building")
                    {
                        if (target == Vector3.zero)
                            targ = tar;
                    }
                }
            }
            //tars = GameObject.FindGameObjectsWithTag("Building");
            foreach (var tar in gm.buildings)
            {
                if ((Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 50) && targ == null)
                {
                    if (target == Vector3.zero)
                        targ = tar;
                }
            }
        }

        if (targ && !booltarg)
        {
            if (!inRange)
            {
                target = Vector3.zero;

                navMeshAgent.SetDestination(targ.transform.position);
                GetComponent<Flocking_Wait>().targetPosition = navMeshAgent.destination;
            }
            GetComponent<Orc_Attack_AI>().SetTarget(targ);
            booltarg = true;
        }
        else if (target != Vector3.zero)
        {
            navMeshAgent.SetDestination(target);
            GetComponent<Flocking_Wait>().targetPosition = navMeshAgent.destination;
            targ = null;
            booltarg = false;
        }

        if (target != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, target) < 10)
            {
                navMeshAgent.ResetPath();
            }
        }
        else if( targ != null)
        {
            if (Vector3.Distance(transform.position, targ.transform.position) < 10)
            {
                navMeshAgent.ResetPath();
            }
        }
    }

    public void gettingAttacked(GameObject tar)
    {
        targ = tar;
        target = tar.transform.position;
    }

    public void InRange()
    {
        navMeshAgent.Stop();
        navMeshAgent.ResetPath();
        inRange = true;
    }

    public void OutofRange()
    {
        inRange = false;
    }
}
