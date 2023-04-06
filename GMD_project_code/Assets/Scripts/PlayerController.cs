using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        }
    }
}