using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int Jump = Animator.StringToHash("jump");
    private static readonly int Land = Animator.StringToHash("land");
    private static readonly int MoveSpeed = Animator.StringToHash("moveSpeed");

    
    private Animator _animator;
    private PlayerInput _playerInput;
    private Rigidbody _rb;
    
    void Awake()
    {
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _animator = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody>();
        CharacterMovement charMove = gameObject.GetComponent<CharacterMovement>();
        Alive lifeActions = gameObject.GetComponent<Alive>();
        
        
        if (_playerInput is not null)
        {
            if (charMove is not null)
            {
                _playerInput.OnJump += charMove.Jump;
                _playerInput.OnForward += charMove.MoveForward;
                _playerInput.OnBackward+= charMove.MoveBackward;
                _playerInput.OnRight += charMove.MoveRight;
                _playerInput.OnLeft += charMove.MoveLeft;
                _playerInput.OnRotateY += charMove.RotateY;
            }

            
            if (lifeActions is not null)
            {
                _playerInput.OnSuicide += lifeActions.Suicide;
            }
        }
        
        if (_animator is not null && charMove is not null)
        {
            charMove.OnJump += delegate { _animator.SetTrigger(Jump); _animator.ResetTrigger(Land); };
        }
        
        HeadMovement headMove = Tools.GetGoWithComponent<HeadMovement>(gameObject.transform);
        if (headMove is not null && _playerInput is not null)
        {
            _playerInput.OnRotateX += headMove.RotateX;
        }
    }

    private void Start()
    {
        CollisionDetector groundDetector = Tools.GetGoWithComponent<CollisionDetector>(gameObject.transform);
        
        if (groundDetector is not null && _animator is not null)
        {
            groundDetector.OnLand += delegate { _animator.SetTrigger(Land);};
        }
    }

    private void FixedUpdate()
    {
        _animator.SetFloat(MoveSpeed, _rb.velocity.magnitude);
    }
}