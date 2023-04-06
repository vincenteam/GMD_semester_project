using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMDProject;

public class CharacterMovement : MonoBehaviour, ICharacterMovement
{
    private CollisionDetector groundDetector;
    private Rigidbody rb;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float forwardAcceleration;
    [SerializeField] private float leftRightAcceleration;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        groundDetector = Tools.GetGoWithComponent<CollisionDetector>(gameObject.transform);
    }

    public void Jump()
    {
        if (groundDetector.Grounded)
        {
            rb.AddForce(jumpHeight * transform.up, ForceMode.Impulse);
            groundDetector.ForceCollisionOut();
        }
    }

    public void MoveRight()
    {
        MoveRightLeft(1);
    }
    
    public void MoveLeft()
    {
        MoveRightLeft(-1);
    }
 
    public void MoveForward()
    {
        MoveBackFront(1);
    }
    
    public void MoveBackward()
    {
        MoveBackFront(-1);
    }

    public void RotateX(float amount)
    {
        throw new NotImplementedException();
    }

    public void RotateY(float amount)
    {
        Quaternion q = Quaternion.Euler(0, amount*360*rotateSpeed*Time.deltaTime, 0); // timedeltatime should already be in amount
        rb.MoveRotation(transform.rotation * q);
    }

    public void RotateZ(float amount)
    {
        throw new NotImplementedException();
    }

    private void MoveBackFront(int direction)
    {
        Vector3 vel = transform.InverseTransformDirection(rb.velocity);

        float forwardPercent = Mathf.Abs(maxSpeed*direction - Mathf.Clamp(vel.z, -maxSpeed, maxSpeed))/ (maxSpeed*2);
        if(direction != 0)
        {
            rb.AddRelativeForce(forwardPercent * forwardAcceleration * new Vector3(0, 0, direction), ForceMode.Acceleration);
        }
    }

    private void MoveRightLeft(int direction)
    {
        Vector3 vel = transform.InverseTransformDirection(rb.velocity);
        
        float rglftPercent = Mathf.Abs(maxSpeed*direction - Mathf.Clamp(vel.x, -maxSpeed, maxSpeed))/ (maxSpeed*2);
        if (direction != 0)
        {
            rb.AddRelativeForce(rglftPercent * leftRightAcceleration * new Vector3(direction, 0, 0),
                ForceMode.Acceleration);
        }
    }
}
