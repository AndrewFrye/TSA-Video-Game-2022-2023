using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public InputActionMap input;

    public Vector3 movement;
    public float speed;
    public float distToGround;
    public float gravity;

    private bool grounded;
    private bool jumping;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        input.Enable();
    }

    public void Update()
    {
        checkGround();
        //set the X and Z movement of the player based off the input
        movement = new Vector3(input.FindAction("Move").ReadValue<Vector2>().x, 0,
            input.FindAction("Move").ReadValue<Vector2>().y);
        
        movement *= speed * Time.deltaTime; //Normalize the speed before adding in the gravity component



        controller.Move(movement);
    }

    private void gravityCalc() //Needs slopes to be handled
    {
        if(!grounded && !jumping) movement.y = -gravity * Time.deltaTime;
    }

    private void checkGround()
    {
        if(Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f))
        {
            grounded = true;
        } else
        {
            grounded = false;
        }
    }
}
