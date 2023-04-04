using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using Math = UnityEngine.ProBuilder.Math;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private float jumpHeight = 1;
    [SerializeField] private float sensitivityY = 0.1f; 

    private Rigidbody rb;
    [SerializeField] private Transform head;
    [SerializeField] private CollisionDetector groundDetector;

    [SerializeField] private Alive lifeEvents;

    [SerializeField] private float leftRightSpeed = 1;
    [SerializeField] private float forwardSpeed = 1;
    [SerializeField] private float maxSpeed = 1;
    float yAccumulator; // this is a member variable, NOT a local!
 
    [SerializeField] float Snappiness = 10.0f;

    private float moveForward;
    private float moveRightLeft;
    private float mouseY;
    private float mouseX;
    private bool jumpInput;

    private bool deathInput;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        rb = gameObject.GetComponent<Rigidbody>();
        
        // rotation hell
        rb.inertiaTensorRotation = Quaternion.identity;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        
        float rglft = Input.GetAxis("RightLeft");
        if (rglft != 0)
        {
            moveRightLeft = rglft < 0 ? -1 : 1;    
        }
        else
        {
            moveRightLeft = 0;
        }
        
        float forward = Input.GetAxis("Forward");
        if (forward != 0)
        {
            moveForward = forward < 0 ? -1 : 1;    
        }
        else
        {
            moveForward = 0;
        }
        
        jumpInput = Input.GetButtonDown("Jump");

        deathInput = Input.GetButtonDown("Death");
    }
    
    void FixedUpdate()
    {
        if (deathInput)
        {
            print("f");
            this.enabled = false;
            lifeEvents.Die();
        }

        if (jumpInput && groundDetector.Grounded)
        {
            rb.AddForce(jumpHeight*transform.up, ForceMode.Impulse);
            groundDetector.ForceCollisionOut();
        }

        
        //movement
        Vector3 vel = transform.InverseTransformDirection(rb.velocity);

        float forward_percent = Mathf.Abs(maxSpeed*moveForward - Mathf.Clamp(vel.z, -maxSpeed, maxSpeed))/ (maxSpeed*2);
        if(moveForward != 0)
        {
            rb.AddRelativeForce(forward_percent * forwardSpeed * new Vector3(0, 0, moveForward), ForceMode.Acceleration);
        }

        
        float rglft_percent = Mathf.Abs(maxSpeed*moveRightLeft - Mathf.Clamp(vel.x, -maxSpeed, maxSpeed))/ (maxSpeed*2);
        if (moveRightLeft != 0)
        {
            rb.AddRelativeForce(rglft_percent * leftRightSpeed * new Vector3(moveRightLeft, 0, 0),
                ForceMode.Acceleration);
        }


        // rotation
        Quaternion q = Quaternion.Euler(0, mouseX*360*rotateSpeed*Time.deltaTime, 0);
        rb.MoveRotation(transform.rotation * q);


        yAccumulator = Mathf.Lerp( yAccumulator, mouseY, Snappiness * Time.deltaTime);
        Vector3 r = new Vector3(yAccumulator*360*-rotateSpeed*sensitivityY, 0, 0);
        
        if (head.localRotation.x*360 + r.x > 180)
        {
            head.localRotation = new Quaternion(0.5f, head.localRotation.y, head.localRotation.z, head.localRotation.w);
        }else if ( head.localRotation.x*360 + r.x < -180)
        {
            head.localRotation = new Quaternion(-0.5f, head.localRotation.y, head.localRotation.z, head.localRotation.w);
        }
        else
        {
            head.Rotate(r, Space.Self);
        }
    }
}
