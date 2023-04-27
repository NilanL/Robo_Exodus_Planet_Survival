using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elf_Movement_AI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    public Vector3 target;
    public GameObject targ;
    bool isGettingAttacked = false;
    bool inRange = false;
    AnimationController animController;
    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGettingAttacked)
        {
            var tars = GameObject.FindGameObjectsWithTag("Selectable");
            foreach (var tar in tars)
            {
                if (targ == null)
                {
                    if (target == Vector3.zero)
                        targ = tar;
                }
                else if (targ.tag == "Building")
                {
                    if (target == Vector3.zero)
                        targ = tar;
                }
            }
            tars = GameObject.FindGameObjectsWithTag("Building");
            foreach (var tar in tars)
            {
                if (Vector3.Distance((tar.transform.position), this.gameObject.transform.position) < 50)
                {
                    if (target == null)
                        targ = tar;
                }
            }
        }

        if (targ)
        {
            animController.IsMoving();
            if (!inRange)
                navMeshAgent.destination = targ.transform.position;
            GetComponent<Elf_Attack_AI>().SetTarget(targ);
        }

        else if (target != null)
        {
            if (isMoving)
                animController.IsMoving();

            navMeshAgent.destination = target;
        }

        else
        {
            animController.IsNotMoving();
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
