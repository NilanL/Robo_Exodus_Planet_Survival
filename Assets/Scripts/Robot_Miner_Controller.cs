using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class Robot_Miner_Controller : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;
    private ParticleSystem miningParticleSystem;
    private LineRenderer attackingLaserLeft;
    private LineRenderer attackingLaserRight;

    [SerializeField]
    public GameObject miningEffectPrefab;

    [SerializeField]
    public GameObject attackingEffectPrefab;

    private Vector3 movementDirection = Vector3.zero;
    private bool playerGrounded;

    private bool isMining;
    private bool isAttacking;

    [SerializeField]
    private float movementSpeed, rotationSpeed, jumpSpeed, gravity;

    // Start is called before the first frame update
    void Start()
    {
        // Get controller and animator
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

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

        // Set default speeds iff not set
        movementSpeed = movementSpeed == 0 ? 12 : movementSpeed;
        rotationSpeed = rotationSpeed == 0 ? 4 : rotationSpeed;
        jumpSpeed = jumpSpeed == 0 ? 3 : jumpSpeed;
        gravity = gravity == 0 ? 5 : gravity;
    }

    // Update is called once per frame
    void Update()
    {
        // Change these to modify animation inputs
        float forwardMovementInput = Input.GetAxisRaw("Vertical");
        float rotationalMovementInput = Input.GetAxisRaw("Horizontal");
        bool jumpInput = Input.GetButton("Jump");
        bool miningInput = Input.GetKeyDown(KeyCode.J);
        bool attackingInput = Input.GetKeyDown(KeyCode.K);

        playerGrounded = characterController.isGrounded;

        // Do movement here
        Vector3 inputMovement = transform.forward * movementSpeed * forwardMovementInput;
        characterController.Move(inputMovement * Time.deltaTime);

        transform.Rotate(Vector3.up * rotationalMovementInput * rotationSpeed);


        // Jumping
        if (jumpInput && playerGrounded)
        {
            movementDirection.y = jumpSpeed;
        }
        movementDirection.y -= gravity * Time.deltaTime;

        characterController.Move(movementDirection * Time.deltaTime);

        // Animations
        SetWalkingAnimation();

        if (miningInput)
            ToggleMiningAnimation();

        if (attackingInput)
            ToggleAttackingAnimation();
    }

    void SetWalkingAnimation()
    {
        animator.SetBool("IsWalking", Input.GetAxisRaw("Vertical") != 0);
    }

    void ToggleMiningAnimation()
    {
        isMining = !isMining;

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