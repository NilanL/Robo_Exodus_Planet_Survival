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

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGettingAttacked)
        {
            var tars = GameObject.FindGameObjectsWithTag("Selectable");
            foreach (var tar in tars)
            {
                if (Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 50)
                {
                    target = tar;
                }
            }
            tars = GameObject.FindGameObjectsWithTag("Building");
            foreach (var tar in tars)
            {
                if (Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 50)
                {
                    if(target == null)
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
