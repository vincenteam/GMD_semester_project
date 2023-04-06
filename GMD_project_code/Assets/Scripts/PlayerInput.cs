using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using Math = UnityEngine.ProBuilder.Math;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float sensitivityY = 0.1f;

    float yAccumulator;
 
    [SerializeField] float Snappiness = 10.0f;

    private float forwardInput;
    private float rgtLftInput;
    private float mouseY;
    private float mouseX;
    
    private bool jumpBtnDown = false;
    private bool jumpBtnUp = false;

    private bool deathInput;
    
    public delegate void ActionsDelegate();
    private ActionsDelegate _jump;
    public ActionsDelegate OnJump
    {
        get => _jump;
        set => _jump = value;
    }
    
    private ActionsDelegate _forward;
    public ActionsDelegate OnForward
    {
        get => _forward;
        set => _forward = value;
    }
    
    private ActionsDelegate _backward;
    public ActionsDelegate OnBackward
    {
        get => _backward;
        set => _backward = value;
    }
    
    private ActionsDelegate _right;
    public ActionsDelegate OnRight
    {
        get => _right;
        set => _right = value;
    }

    private ActionsDelegate _left;
    public ActionsDelegate OnLeft
    {
        get => _left;
        set => _left = value;
    }
    
    private ActionsDelegate _suicide;
    public ActionsDelegate OnSuicide
    {
        get => _suicide;
        set => _suicide = value;
    }
    
    public delegate void RotationDelegate(float amount);
    private RotationDelegate _rotateY;
    public RotationDelegate OnRotateY
    {
        get => _rotateY;
        set => _rotateY = value;
    }
    
    private RotationDelegate _rotateX;
    public RotationDelegate OnRotateX
    {
        get => _rotateX;
        set => _rotateX = value;
    }
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        
        rgtLftInput = Input.GetAxis("RightLeft");

        forwardInput = Input.GetAxis("Forward");

        if (Input.GetButtonDown("Jump")) jumpBtnDown = true;
        if (Input.GetButtonUp("Jump")) jumpBtnUp = true;

        deathInput = Input.GetButtonDown("Death");
    }
    
    void FixedUpdate()
    {
        if (deathInput)
        {
            enabled = false;
            _suicide();
        }
        
        if (jumpBtnDown)
        {
            _jump();
        }
        if (jumpBtnDown && jumpBtnUp)
        {
            jumpBtnDown = false;
            jumpBtnUp = false;
        }

        
        if (forwardInput > 0)
        {
            _forward();
        }else if (forwardInput < 0)
        {
            _backward();
        }
        
        if (rgtLftInput > 0)
        {
            _right();
        }else if (rgtLftInput < 0)
        {
            _left();
        }
        
        // rotation
        _rotateY(mouseX); // if 0 dont invoke ?
        
        yAccumulator = Mathf.Lerp( yAccumulator, mouseY, Snappiness * Time.deltaTime);
        _rotateX(yAccumulator*sensitivityY);
    }
}
