using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int Jump = Animator.StringToHash("jump");

    private Animator animator;
    private static readonly int Land = Animator.StringToHash("land");

    void Awake()
    {
        PlayerInput playerInput = gameObject.GetComponent<PlayerInput>();

        if (playerInput is not null)
        {
            CharacterMovement charMove = gameObject.GetComponent<CharacterMovement>();
            if (charMove is not null)
            {
                playerInput.OnJump += charMove.Jump;
                playerInput.OnForward += charMove.MoveForward;
                playerInput.OnBackward+= charMove.MoveBackward;
                playerInput.OnRight += charMove.MoveRight;
                playerInput.OnLeft += charMove.MoveLeft;
                playerInput.OnRotateY += charMove.RotateY;
            }

            HeadMovement headMove = Tools.GetGoWithComponent<HeadMovement>(gameObject.transform);
            if (headMove is not null)
            {
                playerInput.OnRotateX += headMove.RotateX;
            }

            Alive lifeActions = gameObject.GetComponent<Alive>();
            if (lifeActions is not null)
            {
                playerInput.OnSuicide += lifeActions.Suicide;
            }

            animator = gameObject.GetComponent<Animator>();
            if (animator is not null && charMove is not null)
            {
                charMove.OnJump += delegate { animator.SetTrigger(Jump); animator.ResetTrigger(Land); };
            }
        }
    }

    private void Start()
    {
        CollisionDetector groundDetector = Tools.GetGoWithComponent<CollisionDetector>(gameObject.transform);
        if (groundDetector is not null && animator is not null)
        {
            groundDetector.OnLand += delegate { animator.SetTrigger(Land);};
        }
    }
}