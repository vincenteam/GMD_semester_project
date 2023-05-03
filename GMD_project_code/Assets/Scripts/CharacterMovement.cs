using System;
using UnityEngine;
using GMDProject;

public class CharacterMovement : MonoBehaviour, ICharacterMovement
{
    private GroundDetector _groundDetector;
    private Rigidbody _rb;

    [SerializeField] private float initialJumpHeight;
    [SerializeField] private float initialMaxSpeed;
    [SerializeField] private float initialRotateSpeed;
    [SerializeField] private float initialAcceleration;

    [SerializeField] private float airControl;

    private Vector3 _movement; // the final movement applied to rigidbody
    private int _movingRigLft;
    private int _movingForward;
    private bool _jumping;
    private float _combinedMaxSpeed; // max speed when moving diagonally

    private float _jumpHeight;
    private float _maxSpeed;
    private float _rotateSpeed;
    private float _acceleration; // maximum acceleration by second
    
    public delegate void ActionsDelegate();
    private ActionsDelegate _jump;
    public ActionsDelegate OnJump
    {
        get => _jump;
        set => _jump = value;
    }
    
    public delegate void MoveDelegate(float vel);
    private MoveDelegate _move;
    public MoveDelegate OnMove
    {
        get => _move;
        set => _move = value;
    }

    private void Awake()
    {
        _jumpHeight = initialJumpHeight;
        _maxSpeed = initialMaxSpeed;
        _rotateSpeed = initialRotateSpeed;
        _acceleration = initialAcceleration;
        
        _combinedMaxSpeed = Mathf.Cos(MathF.PI/4) * _maxSpeed;
        
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _groundDetector = gameObject.GetComponentInChildren<GroundDetector>();
        if (_groundDetector != null)
        {
            _groundDetector.OnLand += SwitchToGroundControl;
            _groundDetector.OnLeaveGround += SwitchToAirControl;   
        }
        else
        {
            print("Groundetector is needed to use CharacterMovement");
        }
    }

    public void Jump()
    {
        _jumping = true;
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
        _movingForward = direction;
    }

    private void MoveRightLeft(int direction)
    {
        _movingRigLft = direction;
    }

    private void SwitchToAirControl()
    {
        _maxSpeed = initialMaxSpeed * airControl;
        _acceleration = initialAcceleration * airControl;
        _combinedMaxSpeed = Mathf.Cos(MathF.PI/4) * _maxSpeed;
    }
    
    private void SwitchToGroundControl() // call major Tom
    {
        _maxSpeed = initialMaxSpeed;
        _acceleration = initialAcceleration;
        _combinedMaxSpeed = Mathf.Cos(MathF.PI/4) * _maxSpeed;
    }

    private void FixedUpdate()
    {
        if (_jumping && _groundDetector.Grounded)
        {
            Vector3 vel = _rb.velocity;
            vel.y = 0;
            _rb.velocity = vel;
            _rb.AddForce(_jumpHeight * transform.up, ForceMode.Impulse);
            _groundDetector.ForceCollisionOut(); // useless now ?
            //_jump();
        }
        _jumping = false;
        
        Vector3 force = new Vector3();

        bool forward = Convert.ToBoolean(_movingForward);
        bool rgtLft = Convert.ToBoolean(_movingRigLft);
        
        if (forward && rgtLft)
        {
            Vector3 vel = transform.InverseTransformDirection(_rb.velocity);
            float forwardPercent = Mathf.Abs(_combinedMaxSpeed*_movingForward - Mathf.Clamp(vel.z, -_combinedMaxSpeed, _combinedMaxSpeed))/ _combinedMaxSpeed;
            float rgtLftPercent = Mathf.Abs(_combinedMaxSpeed*_movingRigLft - Mathf.Clamp(vel.x, -_combinedMaxSpeed, _combinedMaxSpeed))/ _combinedMaxSpeed;

            // add directions
            force.z = forwardPercent * _combinedMaxSpeed * _movingForward;
            force.x = rgtLftPercent * _combinedMaxSpeed * _movingRigLft;
            
        }else if (forward)
        {
            Vector3 vel = transform.InverseTransformDirection(_rb.velocity);
            
            float forwardPercent = Mathf.Abs(_maxSpeed*_movingForward - Mathf.Clamp(vel.z, -_maxSpeed, _maxSpeed))/ _maxSpeed;
            force.z = forwardPercent * _maxSpeed * _movingForward;
        }else if (rgtLft)
        {
            Vector3 vel = transform.InverseTransformDirection(_rb.velocity);
            
            float rgtLftPercent = Mathf.Abs(_maxSpeed*_movingRigLft - Mathf.Clamp(vel.x, -_maxSpeed, _maxSpeed))/ _maxSpeed;
            force.x = rgtLftPercent * _maxSpeed * _movingRigLft;
        }

        if (forward || rgtLft)
        {
            Vector3.ClampMagnitude(force, _acceleration * Time.deltaTime);
            _rb.AddRelativeForce(force, ForceMode.VelocityChange);    
            
            _movingForward = 0;
            _movingRigLft = 0;
        }
    }
}
