using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;

    private bool isSelected = false;
    private Ray ray;

    [SerializeField]
    public Camera _camera;

    private bool isMining;
    private bool isAttacking;
    private bool isWalking;

    public Transform target;
    private bool isMiningMove = false;
    private bool isMiningAnimate = false;
    private bool isAttackingAnimate = false;

    private LayerMask fogOfWarLayer;

    [SerializeField]
    private float movementSpeed, rotationSpeed, jumpSpeed, gravity;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = 12;
        navMeshAgent.acceleration = 25;
        navMeshAgent.angularSpeed = 300;

        isMining = false;
        isAttacking = false;
        isWalking = false;

        fogOfWarLayer = LayerMask.GetMask("FogOfWar");
    }

    // Update is called once per frame
    void Update()
    {
        if (isMiningMove)
        {
            if(target)
                navMeshAgent.destination = target.position;
        }

        if (isSelected)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, ~fogOfWarLayer))
            {
                navMeshAgent.destination = hit.point;
            }
        }
    }

    public void IsSelected()
    {
        isSelected = true;
    }

    public void Movement(Ray ray2)
    {
        isMining = false;
        ray = ray2;
    }

    public void IsMiningMove()
    {
        isMiningMove = true;
        isSelected = false;
    }

    public void InRange()
    {
        navMeshAgent.Stop();
        navMeshAgent.ResetPath();
        isMiningMove = false;
    }
}
