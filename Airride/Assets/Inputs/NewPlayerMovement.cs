using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class NewPlayerMovement : MonoBehaviourPun
{
    #region Private Fields
    private PlayerControls controls;
    private Vector2 movementInput;
    private Vector3 suggestedMove;
    private CharacterController characterController;
    private IEnumerator moveCoroutine;
    private IEnumerator aimCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate;
    [SerializeField] private bool isMoving;
    #endregion

    #region Player/Movement Fields
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnLimiter;
    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    [SerializeField] private float _velocity;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        controls = new PlayerControls();
        controls.Player.Enable();
        controls.Player.Move.started += MoveEntry;
        controls.Player.Move.canceled += MoveEntry;
    }

    void FixedUpdate()
    {
        //Debug.Log(characterController.isGrounded);
        if(!isMoving) {ApplyGravity();}
        //characterController.Move(new Vector3(0,_velocity,0));
    }

    #region Player Movement Methods

    private void MoveEntry(InputAction.CallbackContext context)
    {
        //movementInput = context.ReadValue<Vector2>();
        //Debug.Log(context.phase);
        if(context.started)
        {
            isMoving = true;
            moveCoroutine = MoveCoroutine(context);
            StartCoroutine(moveCoroutine);
        } 
        else 
        {
            isMoving = false;
            StopCoroutine(moveCoroutine);
        }
        //Vector3 worldMove = new Vector3(movementInput.x, 0, movementInput.y);
        //characterController.Move(worldMove * speed * Time.deltaTime);
    }

    private IEnumerator MoveCoroutine(InputAction.CallbackContext context)
    {
        while(true)
        {
            if(photonView.IsMine)
            {
                ApplyGravity();
                PlayerAim(context);
                PlayerMove(context);
            }
            yield return waitForFixedUpdate;
        }
            
    }

    private void PlayerMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Vector3 worldMove = new Vector3(movementInput.x, _velocity, movementInput.y);
        characterController.Move(worldMove * speed * Time.deltaTime);
    }

    public void PlayerAim(InputAction.CallbackContext context)
    {
        //gets the joystick input and converts it to vector3
        suggestedMove = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);

        //decides the turn strength. This makes it so the player does automatically face the joystick direction
        Vector3 limitedAngle = ((suggestedMove * (1 - turnLimiter * Time.deltaTime)) + (transform.forward * turnLimiter)).normalized;

        //sets the player forward to the limited angle
        Quaternion lookRotation = Quaternion.LookRotation(limitedAngle, Vector3.up);

        //Debug.Log($"limitedAngle: {limitedAngle}\r\nlookRotation: {lookRotation}");
        //sets player direction to lookRotation
        transform.rotation = lookRotation;
    }

    #endregion


    private void ApplyGravity()
    {
        if (characterController.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        if (!isMoving && !characterController.isGrounded)
        {
            Debug.Log("Called");
            characterController.Move(new Vector3(0,_velocity,0));
        }
    }
}
