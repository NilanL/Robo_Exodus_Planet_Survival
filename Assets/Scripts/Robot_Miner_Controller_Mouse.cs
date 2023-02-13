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

    [SerializeField]
    public GameObject miningEffectPrefab;

    [SerializeField]
    public GameObject attackingEffectPrefab;

    [SerializeField]
    public Camera _camera;

    private bool isMining;
    private bool isAttacking;
    private bool isWalking;

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
        var centralBone = transform.Find("Robot_Miner_Armature").Find("Central_Bone");
        var leftLaser = centralBone.Find("Left_Arm_Bone1").Find("Left_Arm_Bone2").Find("Left_Arm_Bone3");
        var rightLaser = centralBone.Find("Right_Arm_Bone1").Find("Right_Arm_Bone2").Find("Right_Arm_Bone3");

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

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                navMeshAgent.destination = hit.point;
            }
        }

        // Animations
        SetWalkingAnimation();

        if (miningInput)
            ToggleMiningAnimation();

        if (attackingInput)
            ToggleAttackingAnimation();
    }

    void SetWalkingAnimation()
    {
        isWalking = navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;

        if (isWalking)
        {
            SetMiningAnimation(false);
            SetAttackingAnimation(false);
        }

        animator.SetBool("IsWalking", isWalking);
    }

    void ToggleMiningAnimation()
    {
        isMining = !isMining;

        if (isMining)
        {
            SetAttackingAnimation(false);
        }

        if (isMining == true)
        {
            attackingLaserRight.enabled = true;
            miningParticleSystem.Play();
        }
        else
        {
            attackingLaserRight.enabled = false;
            miningParticleSystem.Stop();
        }

        animator.SetBool("IsMining", isMining);
    }

    void SetMiningAnimation(bool val)
    {
        isMining = val;

        if (isMining == true)
        {
            attackingLaserRight.enabled = true;
            miningParticleSystem.Play();
        }
        else
        {
            attackingLaserRight.enabled = false;
            miningParticleSystem.Stop();
        }

        animator.SetBool("IsMining", isMining);
    }

    void ToggleAttackingAnimation()
    {
        isAttacking = !isAttacking;

        if (isAttacking)
        {
            SetMiningAnimation(false);
        }

        if (isAttacking == true)
        {
            attackingLaserLeft.enabled = true;
            attackingLaserRight.enabled = true;
        }
        else
        {
            attackingLaserLeft.enabled = false;
            attackingLaserRight.enabled = false;
        }

        animator.SetBool("IsAttacking", isAttacking);
    }

    void SetAttackingAnimation(bool val)
    {
        isAttacking = val;

        if (isAttacking == true)
        {
            attackingLaserLeft.enabled = true;
            attackingLaserRight.enabled = true;
        }
        else
        {
            attackingLaserLeft.enabled = false;
            attackingLaserRight.enabled = false;
        }

        animator.SetBool("IsAttacking", isAttacking);
    }
}