using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private float jumpHeight = 1;

    private Rigidbody rb;
    [SerializeField] private CollisionDetector groundDetector;

    [SerializeField] private float leftRightSpeed = 1;
    [SerializeField] private float forwardSpeed = 1;
    [SerializeField] private float maxSpeed = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
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
        }
        
        /*Vector3 targetVelocity = new Vector3(0, 0, 0);
        
        float moveForward = Input.GetAxis("Forward");
        //Vector3 move = new Vector3(0, 0, 0);
        if(moveForward != 0 )
        {
            targetVelocity.z = moveForward * forwardSpeed;
            //move += moveForward * forwardSpeed * transform.forward;
            //rg.AddForce(moveForward * forwardSpeed  *Time.deltaTime * transform.forward);
            //rg.velocity = moveForward * forwardSpeed * Time.deltaTime * transform.forward;
        }
        
        float moveRightLeft = Input.GetAxis("RightLeft");
        if (moveRightLeft != 0)
        {
            targetVelocity.x = moveRightLeft * leftRightSpeed;
            //rg.AddForce(moveRightLeft * leftRightSpeed *Time.deltaTime * transform.right);
        }

        targetVelocity.y = rg.velocity.y;
        rg.velocity = targetVelocity;
        //rg.MovePosition(transform.position + move * Time.deltaTime);*/
        
        
        float moveForward = Input.GetAxis("Forward");
        if(moveForward != 0 )
        {
            rb.AddRelativeForce(new Vector3(0, 0, moveForward) * forwardSpeed);
        }
        
        float moveRightLeft = Input.GetAxis("RightLeft");
        if (moveRightLeft != 0)
        {
            rb.AddRelativeForce(new Vector3(moveRightLeft,0,0) * leftRightSpeed);
        }
        
        Quaternion q = Quaternion.Euler(0, Input.GetAxis("Mouse X")*360*rotateSpeed*Time.deltaTime, 0);
        rb.MoveRotation( transform.rotation*q);

        Vector3 vel = rb.velocity;
        rb.velocity = new Vector3(Mathf.Clamp(vel.x, -maxSpeed, maxSpeed), vel.y, Mathf.Clamp(vel.z, -maxSpeed, maxSpeed));
    }
}
