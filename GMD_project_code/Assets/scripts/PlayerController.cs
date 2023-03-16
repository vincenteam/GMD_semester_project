using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1;

    private Rigidbody rg;

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
        //transform.Rotate(0, Input.GetAxis("Mouse X")*360*rotateSpeed*Time.deltaTime, 0);
        Quaternion q = Quaternion.Euler(0, Input.GetAxis("Mouse X")*360*rotateSpeed*Time.deltaTime, 0);
        rg.MoveRotation( transform.rotation*q);
        
        float moveForward = Input.GetAxis("Forward");
        Vector3 move = new Vector3(0, 0, 0);
        if(moveForward != 0 )
        {
            move += moveForward * forwardSpeed * transform.forward;
        }
        
        float moveRightLeft = Input.GetAxis("RightLeft");
        if (moveRightLeft != 0)
        {
            move += moveRightLeft * leftRightSpeed * transform.right;
        }
        
        rg.MovePosition(transform.position + move * Time.deltaTime);
    }
}
