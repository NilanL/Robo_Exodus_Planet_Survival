using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour
{
    private AnimationController animController;
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

    [SerializeField]
    private float movementSpeed, rotationSpeed, jumpSpeed, gravity;

    private LayerMask fogOfWarLayer;

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

        fogOfWarLayer = LayerMask.GetMask("FogOfWar"); // Fog Of War Index
        animController = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent != null)
        {
            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                animController.IsMoving();
            }
            else
            {
                animController.IsNotMoving();
            }
        }

        if (isMiningMove)
        {
            if(target)
                navMeshAgent.destination = target.position;
        }

        if (isSelected)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 9999, ~fogOfWarLayer))
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
