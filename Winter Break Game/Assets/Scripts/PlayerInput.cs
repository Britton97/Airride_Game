using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Sender _fireInput;
    [SerializeField] private float _speed = 5f;

    Vector2 movement;
    Vector3 worldMovement;
    Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        worldMovement.x = movement.x;
        worldMovement.z = movement.y;
        transform.Translate(worldMovement * _speed * Time.deltaTime, Space.World);
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnFire()
    {
        _fireInput.SendAction();
    }
}
