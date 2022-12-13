using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public PlayerControls controls;
    public Camera cam;

    public Vector3 movement;
    public Vector2 cameraRotate;
    public float speed;
    public float distToGround;
    public float gravity;
    public float mouseSensVertical;
    public float mouseSensHorizontal;
    private float xRotation = 0f;

    private bool grounded;
    private bool jumping;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        controls = new PlayerControls();
        controls.Enable();
        
    }

    public void Update()
    {
        checkGround();
        movement = transform.right * controls.main.Move.ReadValue<Vector2>().x + transform.forward * controls.main.Move.ReadValue<Vector2>().y;
        cameraRotate = controls.main.Camera.ReadValue<Vector2>();
        
        slopeDetection();
        
        movement *= speed * Time.deltaTime; //Normalize the speed before adding in the gravity component
        gravityCalc();
        
        cameraMove();
        controller.Move(movement);
    }

    private void gravityCalc() //Needs slopes to be handled
    {
        if (!grounded && !jumping) movement.y = -gravity * Time.deltaTime;
    }

    private void checkGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void cameraMove()
    {
        xRotation -= cameraRotate.y * mouseSensVertical * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.position = transform.position;
        transform.Rotate(Vector3.up, cameraRotate.x * mouseSensHorizontal * Time.deltaTime);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    
    private void slopeDetection()
    {
        var rayArr = new List<RaycastHit>();
        var rayCurrent = new RaycastHit();
        var rayCount = 5;


        for (int x = 0; x < rayCount; x++)
        {
            var rayPos = transform.position;
            rayPos.x -= 1;
            rayPos.x += (2 / rayCount) * x;

            for (int y = 0; y < rayCount; y++)
            {
                rayPos.y -= 1;
                rayPos.y += (2 / rayCount) * x;
                
                Physics.Raycast(rayPos, Vector3.down, out rayCurrent);
                Debug.DrawRay(transform.position, Vector3.down * rayCurrent.distance, Color.yellow);
                rayArr.Add((rayCurrent));
            }
            
        }
        
        Debug.Log(rayArr.ToString());
    }
}
