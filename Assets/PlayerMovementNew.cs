using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementNew : MonoBehaviour
{
    public PlayerInput movement;
    public PlayerInteract interaction;
    public float speed = 5f;
    public float gravity = 9.81f;

    private Vector2 movementInput;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        movementInput = movement.MovementVector;

        Vector3 m = new Vector3(movementInput.x, 0, movementInput.y);

        rigidbody.velocity = new Vector3(m.x * speed, rigidbody.velocity.y * gravity, m.z * speed) * Time.deltaTime;

        Vector3 r = new Vector3(movementInput.x, 0, movementInput.y) * 100f * Time.deltaTime;
        this.transform.LookAt(transform.position + r);
    }
}
