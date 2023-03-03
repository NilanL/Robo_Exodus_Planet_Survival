using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class Robot_Miner_Controller_Mouse : MonoBehaviour
{

    private Animator animator;
    private ParticleSystem miningParticleSystem;
    private NavMeshAgent navMeshAgent;
    private LineRenderer attackingLaserLeft;
    private LineRenderer attackingLaserRight;
    private bool isSelected = false;
    private Ray ray;

    [SerializeField]
    public GameObject miningEffectPrefab;

    [SerializeField]
    public GameObject attackingEffectPrefab;

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

    // Start is called before the first frame update
    void Start()
    {
        // Get controller and animator
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = 12;
        navMeshAgent.acceleration = 25;
        navMeshAgent.angularSpeed = 300;

        // Find attachment location for laser effects
        var centralBone = transform.Find("Robot_Miner_Armature/Central_Bone");
        var leftLaser = centralBone.Find("Left_Arm_Bone1/Left_Arm_Bone2/Left_Arm_Bone3");
        var rightLaser = centralBone.Find("Right_Arm_Bone1/Right_Arm_Bone2/Right_Arm_Bone3");

        // Get laser effect
        var tempLaser = attackingEffectPrefab.GetComponent<LineRenderer>();
        tempLaser.enabled = false;

        // Set left arm laser effect/position
        if (leftLaser != null)
        {
            var rotL = transform.rotation.eulerAngles;
            rotL.x += 12;
            rotL.y += 25;

            attackingLaserLeft = Instantiate(tempLaser, leftLaser.position, Quaternion.Euler(rotL), leftLaser);
        }

        // Set right arm laser effect/position
        if (rightLaser != null)
        {
            var rotR = transform.rotation.eulerAngles;
            rotR.x -= 12;
            rotR.y -= 25;

            attackingLaserRight = Instantiate(tempLaser, rightLaser.position, Quaternion.Euler(rotR), rightLaser);
        }

        // Set mining sparks effects
        miningParticleSystem = Instantiate(miningEffectPrefab.GetComponent<ParticleSystem>(), transform.position, transform.rotation, transform);

        isMining = false;
        isAttacking = false;
        isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Change these to modify animation inputs
        bool miningInput = Input.GetKeyDown(KeyCode.J);
        bool attackingInput = Input.GetKeyDown(KeyCode.K);

        if (isMiningMove)
        {
            navMeshAgent.destination = target.position;
            isMiningMove = false;
        }

        if (isSelected)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                navMeshAgent.destination = hit.point;
            }
        }

        // Animations
        SetWalkingAnimation();

        if (isMiningAnimate)
        {
            SetMiningAnimation(true);
        }
        else
        {
            SetMiningAnimation(false);
        }

        if (isAttackingAnimate)
        {
            SetAttackingAnimation(true);

            if (target)
            {
                navMeshAgent.destination = target.position;
            }
        }
        else
        {
            SetAttackingAnimation(false);
        }

    }

    void SetWalkingAnimation()
    {
        isWalking = navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;

        if (isWalking)
        {
            SetMiningAnimation(false);
            SetAttackingAnimation(false);

            miningParticleSystem.Stop();

            if (!isAttackingAnimate && !isMiningAnimate)
            {
                attackingLaserLeft.enabled = false;
                attackingLaserRight.enabled = false;
            }
        }

        animator.SetBool("IsWalking", isWalking);
    }

    void SetMiningAnimation(bool val)
    {
        isMining = val;

        if (isMining)
        {
            SetAttackingAnimation(false);
        }

        animator.SetBool("IsMining", isMining);
    }

    void SetAttackingAnimation(bool val)
    {
        isAttacking = val;

        if (isAttacking)
        {
            SetMiningAnimation(false);
        }

        animator.SetBool("IsAttacking", isAttacking);
    }

    public void IsSelected()
    {
        isSelected = true;
    }

    public void Movement(Ray ray2)
    {
        ray = ray2;
    }

    public void IsMiningMove()
    {
        isMiningMove = true;
        isSelected = false;
    }

    public void IsMining()
    {
        isMiningAnimate = true;

        attackingLaserLeft.enabled = false;
        attackingLaserRight.enabled = true;
        miningParticleSystem.Play();
    }

    public void IsNotMining()
    {
        isMiningAnimate = false;

        attackingLaserLeft.enabled = false;
        attackingLaserRight.enabled = false;
        miningParticleSystem.Stop();
    }

    public void IsAttacking()
    {
        isAttackingAnimate = true;

        attackingLaserLeft.enabled = true;
        attackingLaserRight.enabled = true;
    }

    public void IsNotAttacking()
    {
        isAttackingAnimate = false;

        attackingLaserLeft.enabled = false;
        attackingLaserRight.enabled = false;
    }
}