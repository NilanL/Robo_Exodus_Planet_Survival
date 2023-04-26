using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Coglings_Movement_AI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    public Vector3 target;
    public GameObject targ;
    bool isGettingAttacked = false;
    bool inRange = false;
    AnimationController animController;
    public bool isMoving = false;
    public bool inWar;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<AnimationController>();
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
                        target = tar.transform.position;
                    else if (tar.GetComponent<TaskManager>().mining)
                        target = tar.transform.position;
                }
            }
            foreach (var tar in gm.buildings)
            {
                if (Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 50)
                {
                    if (targ == null && inWar)
                        target = tar.transform.position;
                }
            }
        }

        if (isMoving)
        {
            animController.IsMoving();
        }
        else
        {
            animController.IsNotMoving();
        }

        if (targ)
        {
            
            if (!inRange)
                navMeshAgent.destination = targ.transform.position;
            GetComponent<Coglings_Attack_AI>().SetTarget(targ);
        }

        else if(target != null)
        {
            navMeshAgent.destination = target;
        }


    }

    public void gettingAttacked(GameObject tar)
    {
        targ = tar;
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
