using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//important
using Prime31;
public class Player : MonoBehaviour
{
    public float gravity = -10f;
    public float movespeed = 5f;
    public float jumpHeight = 3.75f;
    public float centreRadius = 0.1f;

    private CharacterController2D controller;
    private Animator anim;
    private SpriteRenderer renderer;

    private Vector3 velocity;
    private bool isClimbing = false;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, centreRadius);
    }
    // Start is called before the first frame update
    void Start()
    {
        //getcomponents
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //get left and right movement
        float inputH = Input.GetAxis("Horizontal");
        //gets up and down movement
        float inputV = Input.GetAxis("Vertical");

        //grounded check
        if (!controller.isGrounded && !isClimbing)
        {
            //apply time to gravity
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            //get spacebar inpug
            bool isJumping = Input.GetButtonDown("Jump");

            if (isJumping)
            {
                //character jump
                Jump();

            }
        }
        //unity bool == script bool
        anim.SetBool("IsGrounded", controller.isGrounded);
        //unity float == velocity float
        anim.SetFloat("JumpY", velocity.y);

        //call
        Move(inputH);
        Climb(inputH, inputV);

        //if character not climbing 
        if (!isClimbing)
        {
            //applies velocity to controller to make it move
            controller.move(velocity * Time.deltaTime);
        }

    }

    void Move(float inputH)
    {
        //left and right movement
        velocity.x = inputH * movespeed;

        //bool isrunning = true when inputH is not zero
        bool isRunning = inputH != 0;
        //animate if move button is pressed
        anim.SetBool("IsRunning", isRunning);

        // flip the sprite when you are moving in the oposite direction
        if (isRunning) { renderer.flipX = inputH < 0; }

    }

    void Climb(float inputH, float inputV)
    {
        bool isOverLadder = false; //overlapping a ladder


        //get a list of hit objects overlapping point
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, centreRadius);
        //loop through each point
        foreach (var hit in hits)
        {
            //if point overlaps climbable object
            if (hit.tag == "Ladder")
            {
                //player overlap ladder
                isOverLadder = true;
                break;
            }
        }
        //if does & inputV has been made
        if (isOverLadder && inputV != 0)
        {
            //climb
            isClimbing = true;
            velocity.y = 0;
        }

        if (!isOverLadder)
        {
            //not climbing anymore
            isClimbing = false;
        }
        //do logic for climbing
        if (isClimbing)
        {
            //translate character up and down
            Vector3 inputDir = new Vector3(inputH, inputV);
            transform.Translate(inputDir * movespeed * Time.deltaTime);
        }
        anim.SetBool("IsClimbing", isClimbing);
        anim.SetFloat("ClimbSpeed", inputV);
    }
    void Jump()
    {
        // set velocity y index to height
        velocity.y = jumpHeight;
        anim.SetTrigger("Jump");

    }
}
