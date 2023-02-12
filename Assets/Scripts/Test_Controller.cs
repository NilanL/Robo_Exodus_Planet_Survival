using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class Test_Controller : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;
    private ParticleSystem miningParticleSystem;
    private LineRenderer attackingLaserLeft;
    private LineRenderer attackingLaserRight;

    [SerializeField]
    private float movementSpeed, rotationSpeed, jumpSpeed, gravity;

    private Vector3 movementDirection = Vector3.zero;
    private bool playerGrounded;

    private bool isMining;
    private bool isAttacking;

    [SerializeField]
    public GameObject miningEffectPrefab;

    [SerializeField]
    public GameObject attackingEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //var temp = GetComponent<LineRenderer>();

        var centralBone = transform.Find("Robot_Miner_Armature").Find("Central_Bone");
        var leftLaser = centralBone.Find("Left_Arm_Bone1").Find("Left_Arm_Bone2").Find("Left_Arm_Bone3");
        var rightLaser = centralBone.Find("Right_Arm_Bone1").Find("Right_Arm_Bone2").Find("Right_Arm_Bone3");

        var tempLaser = attackingEffectPrefab.GetComponent<LineRenderer>();
        tempLaser.enabled = false;

        if (leftLaser != null)
        {
            var rot = transform.rotation.eulerAngles;

            rot.x += 12;
            rot.y += 25;

            attackingLaserLeft = Instantiate(tempLaser, leftLaser.position, Quaternion.Euler(rot), leftLaser);
        }

        if (rightLaser != null)
        {
            var rot = transform.rotation.eulerAngles;

            rot.x -= 12;
            rot.y -= 25;

            attackingLaserRight = Instantiate(tempLaser, rightLaser.position, Quaternion.Euler(rot), rightLaser);
        }

        //particleSystem = GetComponent<ParticleSystem>();

        //var main = particleSystem.main;
        //var gameObject = particleSystemsList.Where(x => x.name.Equals("Robot_Mining_Sparks")).FirstOrDefault();
        //var ps = gameObject.GetComponent<ParticleSystem>();

        //particleSystem = Instantiate(ps, transform.position, transform.rotation);

        miningParticleSystem = Instantiate(miningEffectPrefab.GetComponent<ParticleSystem>(), transform.position, transform.rotation, transform);

        isMining = false;
        isAttacking = false;

        movementSpeed = movementSpeed == 0 ? 12 : movementSpeed;
        rotationSpeed = rotationSpeed == 0 ? 4 : rotationSpeed;
        jumpSpeed = jumpSpeed == 0 ? 3 : jumpSpeed;
        gravity = gravity == 0 ? 5 : gravity;
    }

    // Update is called once per frame
    void Update()
    {
        playerGrounded = characterController.isGrounded;
 
        //movement
        Vector3 inputMovement = transform.forward * movementSpeed * Input.GetAxisRaw("Vertical");
        characterController.Move(inputMovement * Time.deltaTime);

        transform.Rotate(Vector3.up * Input.GetAxisRaw("Horizontal") * rotationSpeed);


        //jumping
        if (Input.GetButton("Jump") && playerGrounded)
        {
            movementDirection.y = jumpSpeed;
        }
        movementDirection.y -= gravity * Time.deltaTime;

        characterController.Move(movementDirection * Time.deltaTime);

        //animations
        SetWalkingAnimation();

        if (Input.GetKeyDown(KeyCode.J))
            ToggleMiningAnimation();

        if (Input.GetKeyDown(KeyCode.K))
            ToggleAttackingAnimation();

        //animator.SetBool("IsMining", isMining);
        //animator.SetBool("IsAttacking", isAttacking);

        //animator.SetBool("is")
        //animator.SetBool("isJumping", !characterController.isGrounded);
    }

    void SetWalkingAnimation()
    {
        animator.SetBool("IsWalking", Input.GetAxisRaw("Vertical") != 0);
    }

    void ToggleMiningAnimation()
    {
        isMining = !isMining;

        if (isMining == true)
            miningParticleSystem.Play();
        else
            miningParticleSystem.Stop();

        animator.SetBool("IsMining", isMining);
    }

    void SetMiningAnimation(bool val)
    {
        isMining = val;

        if (isMining == true)
            miningParticleSystem.Play();
        else
            miningParticleSystem.Stop();

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

    //public static Transform GetChild(Transform parent, string childName)
    //{
    //    var child = parent.Find(childName);
    //
    //    foreach (var vall in parent.)
    //    if (child != null)
    //    {
    //        return child;
    //    }
    //    else
    //    {
    //        return GetChild(child, childName);
    //    }
    //
    //    return null;
    //}
}