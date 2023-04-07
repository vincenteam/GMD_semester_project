using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMDProject;

public class CharacterMovement : MonoBehaviour, ICharacterMovement
{
    private CollisionDetector _groundDetector;
    private Rigidbody _rb;

    [SerializeField] private float initialJumpHeight;
    [SerializeField] private float initialMaxSpeed;
    [SerializeField] private float initialRotateSpeed;
    [SerializeField] private float initialForwardAcceleration;
    [SerializeField] private float initialLeftRightAcceleration;

    [SerializeField] private float airControl;
    
    
    private float _jumpHeight;
    private float _maxSpeed;
    private float _rotateSpeed;
    private float _forwardAcceleration;
    private float _leftRightAcceleration;
    
    public delegate void ActionsDelegate();
    private ActionsDelegate _jump;
    public ActionsDelegate OnJump
    {
        get => _jump;
        set => _jump = value;
    }

    private void Awake()
    {
        _jumpHeight = initialJumpHeight;
        _maxSpeed = initialMaxSpeed;
        _rotateSpeed = initialRotateSpeed;
        _forwardAcceleration = initialForwardAcceleration;
        _leftRightAcceleration = initialLeftRightAcceleration;
        
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _groundDetector = Tools.GetGoWithComponent<CollisionDetector>(gameObject.transform);
        _groundDetector.OnLand += SwitchToGroundControl;
        _groundDetector.OnLeaveGround += SwitchToAirControl;
    }

    public void Jump()
    {
        if (_groundDetector.Grounded)
        {
            _rb.AddForce(_jumpHeight * transform.up, ForceMode.Impulse);
            _groundDetector.ForceCollisionOut();
            _jump();
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
        Quaternion q = Quaternion.Euler(0, amount*360*_rotateSpeed*Time.deltaTime, 0); // timedeltatime should already be in amount
        _rb.MoveRotation(transform.rotation * q);
    }

    public void RotateZ(float amount)
    {
        throw new NotImplementedException();
    }

    private void MoveBackFront(int direction)
    {
        Vector3 vel = transform.InverseTransformDirection(_rb.velocity);

        float forwardPercent = Mathf.Abs(_maxSpeed*direction - Mathf.Clamp(vel.z, -_maxSpeed, _maxSpeed))/ (_maxSpeed*2);
        if(direction != 0)
        {
            _rb.AddRelativeForce(forwardPercent * _forwardAcceleration * new Vector3(0, 0, direction), ForceMode.Acceleration);
        }
    }

    private void MoveRightLeft(int direction)
    {
        Vector3 vel = transform.InverseTransformDirection(_rb.velocity);
        
        float rglftPercent = Mathf.Abs(_maxSpeed*direction - Mathf.Clamp(vel.x, -_maxSpeed, _maxSpeed))/ (_maxSpeed*2);
        if (direction != 0)
        {
            _rb.AddRelativeForce(rglftPercent * _leftRightAcceleration * new Vector3(direction, 0, 0),
                ForceMode.Acceleration);
        }
    }

    private void SwitchToAirControl()
    {
        print("switch air");
        _maxSpeed = initialMaxSpeed * airControl;
        _forwardAcceleration = initialForwardAcceleration * airControl;
        _leftRightAcceleration = initialLeftRightAcceleration * airControl;
    }
    
    private void SwitchToGroundControl() // call major Tom
    {
        print("switch ground");
        _maxSpeed = initialMaxSpeed;
        _forwardAcceleration = initialForwardAcceleration;
        _leftRightAcceleration = initialLeftRightAcceleration;
    }
}
