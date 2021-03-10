using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerInputActions playerInput;
    [SerializeField] private float speed;

    Vector2 move;
    Vector2 rotate;

    private void Awake()
    {
        playerInput = new PlayerInputActions();

        // Collect movement input
        playerInput.Movement.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        playerInput.Movement.Move.canceled += ctx => move = Vector2.zero;

        // Collect rotation input
        playerInput.Movement.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        playerInput.Movement.Rotate.canceled += ctx => rotate = Vector2.zero;
    }

    private void Update()
    {
        Vector3 m = new Vector3(move.x, 0, move.y) * Time.deltaTime * speed;
        controller.Move(m);

        Vector3 r = new Vector3(rotate.x, 0, rotate.y) * 100f * Time.deltaTime;
        this.transform.LookAt(transform.position + r);
    }

    private void OnEnable()
    {
        playerInput.Movement.Enable();
    }

    private void OnDisable()
    {
        playerInput.Movement.Disable();
    }
}