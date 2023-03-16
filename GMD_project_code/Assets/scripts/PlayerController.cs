using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private float jumpHeight = 1;

    private Rigidbody rg;
    [SerializeField] private CollisionDetector groundDetector;

    [SerializeField] private float leftRightSpeed = 1;
    [SerializeField] private float forwardSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        rg = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Quaternion q = Quaternion.Euler(0, Input.GetAxis("Mouse X")*360*rotateSpeed*Time.deltaTime, 0);
        //rg.MoveRotation( transform.rotation*q);

        Vector3 targetVelocity = new Vector3(0, 0, 0);
        
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
            targetVelocity.x = moveRightLeft * forwardSpeed;
            //rg.AddForce(moveRightLeft * leftRightSpeed *Time.deltaTime * transform.right);
        }
        
        if (Input.GetButtonDown("Jump") && groundDetector.Grounded)
        {
            rg.AddForce(jumpHeight*transform.up, ForceMode.Impulse);
        }

        targetVelocity.y = rg.velocity.y;
        rg.velocity = targetVelocity;
        //rg.MovePosition(transform.position + move * Time.deltaTime);
    }
}
