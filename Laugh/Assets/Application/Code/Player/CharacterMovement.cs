using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum DirectionFacing
{
    Left, Right
}

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AudioSource walkAudioSource;

    [SerializeField]
    private float speed = 5;

    [Header("Rotations")]
    [SerializeField]
    private float flipSpeed = 0.5f;
    [SerializeField]
    private Transform rotatingSprite;

    private bool leftKeyPressed = false;
    private bool rightKeyPressed = false;
    private float finalAngleValue;

    public DirectionFacing Direction { private set; get; } = DirectionFacing.Left;

    void Start()
    {
        playerInput.actions["MoveLeft"].performed += CharacterMoveLeft_performed;
        playerInput.actions["MoveRight"].performed += CharacterMoveRight_performed;

        playerInput.actions["MoveLeft"].canceled += CharacterMoveLet_canceled;
        playerInput.actions["MoveRight"].canceled += CharacterMoveRight_canceled; ;
    }

    private void CharacterMoveRight_canceled(InputAction.CallbackContext obj)
    {
        rightKeyPressed = false;
    }

    private void CharacterMoveLet_canceled(InputAction.CallbackContext obj)
    {
        leftKeyPressed = false;
    }

    private void CharacterMoveRight_performed(InputAction.CallbackContext context)
    {
        SetDirectionFacing(DirectionFacing.Right);
        rightKeyPressed = true;
    }

    private void CharacterMoveLeft_performed(InputAction.CallbackContext obj)
    {
        SetDirectionFacing(DirectionFacing.Left);
        leftKeyPressed = true;
    }

    void FixedUpdate()
    {
        if (leftKeyPressed && !rightKeyPressed)
        {
            MovePlayer(-1);
            animator.SetBool("Moving", true);
            PlaySteps(true);
        }
        else if (rightKeyPressed && !leftKeyPressed)
        {
            MovePlayer(1);
            animator.SetBool("Moving", true);
            PlaySteps(true);
        }
        else
        {
            MovePlayer(0);
            animator.SetBool("Moving", false);
            PlaySteps(false);
            rotatingSprite.rotation = Quaternion.Euler(0, rotatingSprite.eulerAngles.y, 0);
        }
    }

    private void PlaySteps(bool play)
    {
        if (play && !walkAudioSource.isPlaying)
        {
            walkAudioSource.Play();
        }
        else if (!play && walkAudioSource.isPlaying)
        {
            walkAudioSource.Stop();
        }
    }

    private void MovePlayer(int direction)
    {
        var movement = new Vector3(direction * speed * Time.deltaTime, 0, 0);
        controller.Move(movement);
    }

    private void SetDirectionFacing(DirectionFacing directionFacing)
    {
        if (Direction != directionFacing)
        {
            StopAllCoroutines();

            rotatingSprite.rotation = Quaternion.Euler(0, finalAngleValue, 0);

            StartCoroutine("FlipCharater");
        }

        Direction = directionFacing;
    }

    private IEnumerator FlipCharater()
    {
        float newAngle = rotatingSprite.eulerAngles.y;
        finalAngleValue = 180 + newAngle;

        while (finalAngleValue != newAngle)
        {
            newAngle += flipSpeed * Time.deltaTime;

            if (newAngle > finalAngleValue) { newAngle = finalAngleValue; }

            rotatingSprite.rotation = Quaternion.Euler(0, newAngle, 0);

            yield return null;
        }
    }
}
