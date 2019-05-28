using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//important
using Prime31;
public class Player : MonoBehaviour
{
    public float gravity = -10f;
    public float movespeed = 5f;
    private CharacterController2D controller;
    private Animator anim;
    private SpriteRenderer Renderer;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        Move(inputH, inputV);

       
    }
    void Move(float inputH, float inputV)
    {
        controller.move(transform.right * inputH * movespeed * Time.deltaTime);
        bool isRunning = inputH != 0;
            anim.SetBool("IsRunning", isRunning);
        Renderer.flipX = inputH > 0;
       
    }
}
