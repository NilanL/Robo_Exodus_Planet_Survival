using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wolf_AI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    GameObject target;
    bool gettingAttaced = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gettingAttaced)
        {
            var tars = GameObject.FindGameObjectsWithTag("Selectable");
            foreach (var tar in tars)
            {
                if (Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 50)
                {
                    target = tar;
                }
            }
        }
        if (target)
        {
            navMeshAgent.destination = target.transform.position;
            GetComponent<Enemy_Attacking>().SetTarget(target);
        }
            
    }

    public void gettingAttacked(GameObject tar)
    {
        target = tar;
    }
}
