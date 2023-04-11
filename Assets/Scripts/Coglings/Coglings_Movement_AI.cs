using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Coglings_Movement_AI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    GameObject target;
    bool isGettingAttacked = false;
    bool inRange = false;
    GameManager gm;
    bool inWar = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGettingAttacked)
        {
            foreach (var tar in gm.selectables)
            {
                if (Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 50)
                {
                    if (inWar)
                        target = tar;
                    else if (tar.GetComponent<TaskManager>().mining)
                        target = tar;
                }
            }
            foreach (var tar in gm.buildings)
            {
                if (Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 50)
                {
                    if(target == null && inWar)
                        target = tar;
                }
            }
        }

        if (target)
        {
            if(!inRange)
                navMeshAgent.destination = target.transform.position;
            GetComponent<Coglings_Attack_AI>().SetTarget(target);
        }

    }

    public void gettingAttacked(GameObject tar)
    {
        target = tar;
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
