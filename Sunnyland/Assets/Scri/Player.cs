using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//important
using Prime31;
public class Player : MonoBehaviour
{
    public float gravity = -10f;
    public float movespeed = 10f;
    private CharacterController2D controller;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        controller.move(transform.right * inputH * movespeed * Time.deltaTime);
    }
}
