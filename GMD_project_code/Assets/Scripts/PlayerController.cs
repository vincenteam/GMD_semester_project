using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int Jump = Animator.StringToHash("jump");
    private static readonly int Land = Animator.StringToHash("land");
    private static readonly int MoveSpeed = Animator.StringToHash("moveSpeed");
    
    private SkinManager _skinManager;
    private PlayerInput _playerInput;
    private Rigidbody _rb;

    [SerializeField] private GameObject newSkin;

    void Awake()
    {
        _skinManager = gameObject.GetComponent<SkinManager>();
        
        _playerInput = gameObject.GetComponent<PlayerInput>();
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

            if (_skinManager != null)
            {
                _playerInput.OnChangeSkin += delegate {_skinManager.ChangeSkin(newSkin);};
            }
        }

        HeadMovement headMove = Tools.GetGoWithComponent<HeadMovement>(gameObject.transform);
        if (headMove is not null && _playerInput is not null)
        {
            _playerInput.OnRotateX += headMove.RotateX;
        }
    }

    private void Start()
    {
        GroundDetector groundDetector = gameObject.GetComponentInChildren<GroundDetector>();
        
        if (groundDetector is not null && _skinManager is not null)
        {
            groundDetector.OnLand += delegate { _skinManager.AnimatorInstance.SetTrigger(Land);};
            groundDetector.OnLeaveGround += delegate { 
                _skinManager.AnimatorInstance.SetTrigger(Jump);
                _skinManager.AnimatorInstance.ResetTrigger(Land);
            };
        }
    }

    private void FixedUpdate()
    {
        _skinManager.AnimatorInstance.SetFloat(MoveSpeed, _rb.velocity.magnitude);
    }
}