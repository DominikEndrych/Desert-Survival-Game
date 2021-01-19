using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public CharacterController characterController;
    public Transform checkGround;
    public Animator equipmentAnimator;

    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float staminaBase = 30f;

    private float speed;
    private float stamina;
    private bool canRun;
    private bool runOnCooldown = false;
    public bool isRunning { get; set; }


    private float gravity = -15.8f;
    private float groundDistance = 0.4f;
    private Vector3 velocity;
    public LayerMask whatIsGround;
    private bool isGrounded;

    private void Start()
    {
        stamina = staminaBase;
        canRun = true;
    }


    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        //check if player is on the ground
        isGrounded = Physics.CheckSphere(checkGround.position, groundDistance, whatIsGround);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");


        speed = this.getSpeed();
        /*
        equipmentAnimator.SetBool("isMoving", isMoving);
        equipmentAnimator.SetFloat("speed", speed);
        */


        characterController.Move(move * speed * Time.deltaTime);
        
        //building up velocity over time
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);    //needs to by multiplied once more because of the gravity law

    }

    private float getSpeed()
    {
        float speed;

        //player is running
        if (Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            speed = runSpeed;
            isRunning = true;
            stamina -= 0.2f; 

        }
        //player is not running and stamina is slowly recharging
        else if (canRun && stamina < staminaBase)
        {
            stamina += 0.01f;
            isRunning = false;
            speed = walkSpeed;
        }
        //player is not running
        else 
        {
            isRunning = false;
            speed = walkSpeed;
        }

        if (stamina <= 0 && !runOnCooldown) StartCoroutine(RunCooldown());  //start recharging stamina

        return speed;
    }

    private IEnumerator RunCooldown()
    {
        Debug.Log("Exhausted");
        runOnCooldown = true;
        isRunning = false;
        canRun = false;
        yield return new WaitForSeconds(5);
        canRun = true;
        stamina = staminaBase;
        runOnCooldown = false;
        Debug.Log("You can run again");
    }

}
