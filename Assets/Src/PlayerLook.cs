using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;


public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public PlayerControls input;
    public Transform player;
    

    public float lookSens;

    private void Awake()
    {
        input = new PlayerControls();
    }

    private void Update()
    {
        
        
    }
}
