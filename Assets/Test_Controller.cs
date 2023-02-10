using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator), typeof(ParticleSystem))]
public class Test_Controller : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;
    private ParticleSystem particleSystem;

    [SerializeField]
    private float movementSpeed, rotationSpeed, jumpSpeed, gravity;

    private Vector3 movementDirection = Vector3.zero;
    private bool playerGrounded;

    private bool isMining;
    private bool isAttacking;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        particleSystem = GetComponent<ParticleSystem>();
        isMining = false;
        isAttacking = false;
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
        animator.SetBool("IsWalking", Input.GetAxisRaw("Vertical") != 0);

        if (Input.GetKeyDown(KeyCode.J))
        {
            isMining = !isMining;

            if (isMining == true)
                particleSystem.Play();
            else
                particleSystem.Stop();
        }

        if (Input.GetKeyDown(KeyCode.K))
            isAttacking = !isAttacking;

        animator.SetBool("IsMining", isMining);
        animator.SetBool("IsAttacking", isAttacking);

        //animator.SetBool("is")
        //animator.SetBool("isJumping", !characterController.isGrounded);
    }
}