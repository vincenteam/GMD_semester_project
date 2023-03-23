using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private float jumpHeight = 1;
    [SerializeField] private float sensitivityY = 0.1f; 

    private Rigidbody rb;
    [SerializeField] private Transform head;
    [SerializeField] private CollisionDetector groundDetector;

    [SerializeField] private float leftRightSpeed = 1;
    [SerializeField] private float forwardSpeed = 1;
    [SerializeField] private float maxSpeed = 1;
    float yAccumulator; // this is a member variable, NOT a local!
 
    [SerializeField] float Snappiness = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        rb = gameObject.GetComponent<Rigidbody>();
        
        // rotation hell
        gameObject.GetComponent<Rigidbody>();
        rb.inertiaTensorRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && groundDetector.Grounded)
        {
            rb.AddForce(jumpHeight*transform.up, ForceMode.Impulse);
            groundDetector.ForceCollisionOut();
        }

        Vector3 vel = transform.InverseTransformDirection(rb.velocity);
        float moveForward = Input.GetAxis("Forward");
        if(moveForward != 0 && vel.z < maxSpeed && vel.z > -maxSpeed)
        {
            rb.AddRelativeForce(new Vector3(0, 0, moveForward) * forwardSpeed);
        }
        
        float moveRightLeft = Input.GetAxis("RightLeft");
        if (moveRightLeft != 0 && vel.x < maxSpeed && vel.x > -maxSpeed)
        {
            rb.AddRelativeForce(new Vector3(moveRightLeft,0,0) * leftRightSpeed);
        }
        
        Quaternion q = Quaternion.Euler(0, Input.GetAxis("Mouse X")*360*rotateSpeed*Time.deltaTime, 0);
        rb.MoveRotation( transform.rotation*q);
        
        
        float inputY = Input.GetAxis("Mouse Y");
        yAccumulator = Mathf.Lerp( yAccumulator, inputY, Snappiness * Time.deltaTime);
        Vector3 r = new Vector3(yAccumulator*360*rotateSpeed*sensitivityY, 0, 0);
        
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
