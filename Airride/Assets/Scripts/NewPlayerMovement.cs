using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

namespace Com.MyCompany.MyGame
{
public class NewPlayerMovement : MonoBehaviourPun, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
    #region Private Fields
    private PlayerControls controls;
    private Vector2 movementInput;
    private Vector3 suggestedMove;
    [HideInInspector]
    public CharacterController characterController;
    private IEnumerator moveCoroutine;
    private IEnumerator aimCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate;
    private bool isMoving;
    private PlayerManager target;
    #endregion

    #region Player/Movement Fields
    public AnimationCurve movementCurve;
    [SerializeField] private float timeSinceStarted;
    [SerializeField] private Data_Float speed;
    [SerializeField] private Data_Float speedMultiplier; //this is for the animation curve
    private float animationCurveMultiplier;
    [SerializeField] private float turnLimiter;
    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;
    #endregion

    #region Set Target

    public void SetTarget(PlayerManager _target)
    {
        if (_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }

        target = _target;
    }

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

    void OnDisable()
    {
        controls.Player.Move.started -= MoveEntry;
        controls.Player.Move.canceled -= MoveEntry;
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
        if(target == null) {return;}
        if(target.IsFrozen) {return;}
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
        timeSinceStarted = 0;
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

        //test
        animationCurveMultiplier = movementCurve.Evaluate(timeSinceStarted);
        timeSinceStarted += Time.deltaTime;
        //test

        Vector3 worldMove = new Vector3(movementInput.x, _velocity, movementInput.y);
        characterController.Move(worldMove * speed.Value * speedMultiplier.Value * animationCurveMultiplier * Time.deltaTime);
    }

    public void PlayerAim(InputAction.CallbackContext context)
    {
        //gets the joystick input and converts it to vector3
        suggestedMove = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);

        //decides the turn strength. This makes it so the player does automatically face the joystick direction
        Vector3 limitedAngle = (suggestedMove * ((1 - turnLimiter) * Time.deltaTime) + (transform.forward * turnLimiter)).normalized;

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
            //Debug.Log("Called");
            characterController.Move(new Vector3(0,_velocity,0));
        }
    }
}
}